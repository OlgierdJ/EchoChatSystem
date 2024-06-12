using AutoMapper;
using CoreLib.DTO.RequestCore.InviteCore;
using CoreLib.DTO.RequestCore.RelationCore;
using CoreLib.DTO.RequestCore.ServerCore;
using CoreLib.DTO.RequestCore.ServerCore.EmoteCore;
using CoreLib.DTO.RequestCore.ServerCore.Role;
using CoreLib.DTO.RequestCore.ServerCore.Server;
using CoreLib.DTO.RequestCore.ServerCore.SoundboardSound;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.VoiceChannel;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.SettingsCore;
using CoreLib.Entities.Enums;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Services;
using DomainCoreApi.EFCORE;
using Microsoft.EntityFrameworkCore;

namespace DomainCoreApi.Services;

public class ServerService : IServerService
{
    private readonly EchoDbContext context;
    private readonly IMapper mapper;
    private readonly IPushNotificationService notificationService;
    private readonly IServerTextChannelService textChannelService;

    public ServerService(EchoDbContext context, IMapper mapper, IPushNotificationService notificationService, IServerTextChannelService textChannelService)
    {
        this.context = context;
        this.mapper = mapper;
        this.notificationService = notificationService;
        this.textChannelService = textChannelService;
    }

    public async Task<bool> ConsumeInvite(ulong senderId, ulong serverid, string inviteCode)
    {
        try
        {
            //verify sender is member
            var serverInvite = await context.Set<ServerInvite>().FirstOrDefaultAsync(e => e.InviteCode == inviteCode);
            var serverVoiceInvite = await context.Set<ServerInvite>().FirstOrDefaultAsync(e => e.InviteCode == inviteCode);
            var server = await context.Set<Server>()
                .Include(e => e.Members.Where(t => t.AccountId == senderId).Take(1))
                .Include(e => e.Invites.Where(e => e.InviteCode == inviteCode).Take(1))
                //.Include(e => e.VoiceInvites.Where(e => e.InviteCode == inviteCode).Take(1))
                .Include(e => e.Settings).ThenInclude(e => e.SystemMessagesChannel).ThenInclude(e => e.Messages.OrderByDescending(e => e.TimeSent).Take(1))
                .Include(e => e.TextChannels).ThenInclude(e => e.Messages.OrderByDescending(e => e.TimeSent).Take(1))
                .Include(e => e.VoiceChannels)
                .Include(e => e.Roles)
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == serverid);
            var sender = await context.Set<Account>()
                .Include(e => e.Profile)
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == senderId);
            if (sender == null || server == null || !server.Invites.Any())
            {
                //cant process nonexistant sender, server or nonexistant invite within server. therefore go away plebian
                return false;
            }

            //if already member cant consume invite
            var isMember = server.Members.Any(); //already filtered from loading.
            if (isMember)
            {
                return false;
            }
            var defaultRole = server.Roles.OrderBy(e => e.Importance).First(); //probably role will always be present such as member, everyone or servermember or such.
            ServerProfile participancy = new()
            {
                AccountId = senderId,
                Nickname = sender.Profile.DisplayName,
                FolderId = null,
                Roles = new List<ServerProfileServerRole>()
                  {
                      new()
                      {
                           AccountId=senderId,
                            ServerId = serverid,
                             RoleId = defaultRole.Id,
                      }
                  },
                ImageIconURL = sender.Profile.AvatarFileURL
                //ServerId = invite.SubjectId,
                //temp membership is not supported yet
            };

            //the invite has to be not expired, and the inviter cant use their own invite.
            //also if the totaluses is not 0 which means unlimited then the validation should also take times used in regard.
            var invite = server.Invites.FirstOrDefault(); //already filtered from loading
            var notExpired = invite.ExpirationTime > DateTime.Now;
            var notIssuedBySender = invite.InviterId != senderId;
            var canBeUsedStill = invite.TotalUses == 0 ? true : invite.TotalUses > invite.TimesUsed;
            var inviteValid = notExpired && notIssuedBySender && canBeUsedStill;

            if (!inviteValid) //dont allow people to use invalid invite to join
            {
                return false;
            }

            if (invite.VoiceChannelId != null)
            {
                participancy.KickFromServerOnVoiceLeave = invite.GuestInvite;
                participancy.JoinMethod = invite.GuestInvite ? "Temporary_VoiceInvite" : "VoiceInvite";
                //prompt client join voice here from publisher
            }
            else if (invite.TextChannelId != null)
            {
                participancy.JoinMethod = "Invite";
                ChatAccountMessageTracker tracker = new()
                {
                    OwnerId = senderId,
                    CoOwnerId = invite.SubjectId,
                    SubjectId = server.TextChannels.FirstOrDefault(e => e.Id == invite.TextChannelId)?.Id // set channel if any set on invite
                };
                context.Set<ChatAccountMessageTracker>().Add(tracker);
                //prompt client navigate textchannel here from publisher
            }
            else
            {
                //if here that means no channel is specified and there probably isnt any channel in the server so just allow passage
                participancy.JoinMethod = "Invite";
            }

            invite.TimesUsed += 1; //maybe make seperate account invite use entity table and once in a while empty and add count to invite such that no concurrency issues will arise

            context.Set<ServerProfile>().Add(participancy);
            //publish inviteused event here maybe
            //publish profilecreated event here maybe
            var res = await context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> CreateEmote(ulong senderId, ulong serverid, CreateEmoteRequestDTO requestDTO)
    {
        try
        {
            //verify sender is member
            var server = await context.Set<Server>()
                //annoys the fk out of me that you cant just include members.firstordefault where some condition on thenincludes
                .Include(e => e.Members.Where(e => e.AccountId == senderId).Take(1)) // filter members from db 
                .ThenInclude(e => e.Roles) //include sender role grants and the role as well as their permissions to see if they have permission to change server image
                .ThenInclude(e => e.Role)
                .ThenInclude(e => e.Permissions) //you dont actually need to pull all permissions if you know the ids of the reused serverpermissions.
                .ThenInclude(e => e.Permission)
                .Include(e => e.Emotes.Where(e => e.Name == requestDTO.Name))
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == serverid);
            if (server == null || server.Emotes.Any())
            {
                //cant process nonexistant server or add emote with existing name
                return false;
            }
            var sender = server.Members.FirstOrDefault(); //if any then sender is here since already filtered // else null
            var isMember = sender != null;
            if (!isMember)
            {
                return false;
            }
            var allowedPermissions = new string[] { "create expressions", "administrator" };
            var HasPermission = sender.IsOwner; //if the account is serverowner then allow and skip role permission processing
            HasPermission = HasPermission || FindHierarchalPermissionFromRoles(sender, allowedPermissions); //keep true if true else assign from role permission state
            if (!HasPermission) //if they still dont have permission
            {
                return false;
            }

            var Emote = new ServerEmote()
            {
                UploaderId = senderId,
                ServerId = serverid,
                Name = requestDTO.Name,
                ImageUrl = requestDTO.Name,
            };

            await context.Set<ServerEmote>().AddAsync(Emote);

            await context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> CreateEvent(ulong senderId, ulong serverid, CreateEventRequestDTO requestDTO)
    {
        try
        {
            //verify sender is member
            var server = await context.Set<Server>()
                //annoys the fk out of me that you cant just include members.firstordefault where some condition on thenincludes
                .Include(e => e.Members.Where(e => e.AccountId == senderId).Take(1)) // filter members from db 
                .ThenInclude(e => e.Roles) //include sender role grants and the role as well as their permissions to see if they have permission to change server image
                .ThenInclude(e => e.Role)
                .ThenInclude(e => e.Permissions) //you dont actually need to pull all permissions if you know the ids of the reused serverpermissions.
                .ThenInclude(e => e.Permission)
                .Include(e => e.Roles) //will presumably always have one role which everyone is granted if not then servercreation went wrong
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == serverid);
            if (server == null)
            {
                //cant process nonexistant server
                return false;
            }
            var sender = server.Members.FirstOrDefault(); //if any then sender is here since already filtered // else null
            var isMember = sender != null;
            if (!isMember)
            {
                return false;
            }
            var allowedPermissions = new string[] { "create event", "administrator" };
            var HasPermission = sender.IsOwner; //if the account is serverowner then allow and skip role permission processing
            HasPermission = HasPermission || FindHierarchalPermissionFromRoles(sender, allowedPermissions); //keep true if true else assign from role permission state
            if (!HasPermission) //if they still dont have permission
            {
                return false;
            }

            var newEvent = new ServerEvent()
            {
                CreatorId = senderId,
                ServerId = serverid,
                Description = requestDTO.Description,
                EndTime = requestDTO.EndTime,
                ImageFileUrl = requestDTO.ImageFileUrl,
                EventFrequency = requestDTO.EventFrequency,
                Location = requestDTO.Location,
                StartTime = requestDTO.StartTime,
                Topic = requestDTO.Topic,
            };
            await context.Set<ServerEvent>().AddAsync(newEvent);


            await context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> CreateInvite(ulong senderId, ulong serverid, CreateInviteRequestDTO requestDTO)
    {
        try
        {
            //verify sender is member
            var server = await context.Set<Server>()
                //annoys the fk out of me that you cant just include members.firstordefault where some condition on thenincludes
                .Include(e => e.Members.Where(e => e.AccountId == senderId).Take(1)) // filter members from db 
                .ThenInclude(e => e.Roles) //include sender role grants and the role as well as their permissions to see if they have permission to change server image
                .ThenInclude(e => e.Role)
                .ThenInclude(e => e.Permissions) //you dont actually need to pull all permissions if you know the ids of the reused serverpermissions.
                .ThenInclude(e => e.Permission)
                .Include(e => e.Roles) //will presumably always have one role which everyone is granted if not then servercreation went wrong
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == serverid);
            if (server == null)
            {
                //cant process nonexistant server
                return false;
            }
            var sender = server.Members.FirstOrDefault(); //if any then sender is here since already filtered // else null
            var isMember = sender != null;
            if (!isMember)
            {
                return false;
            }
            var allowedPermissions = new string[] { "create invite", "administrator" };
            var HasPermission = sender.IsOwner; //if the account is serverowner then allow and skip role permission processing
            HasPermission = HasPermission || FindHierarchalPermissionFromRoles(sender, allowedPermissions); //keep true if true else assign from role permission state
            if (!HasPermission) //if they still dont have permission
            {
                return false;
            }
            var invite = new ServerInvite()
            {
                ExpirationTime = requestDTO.TimeExpires,
                InviterId = senderId,
                SubjectId = serverid,
                TotalUses = requestDTO.TotalUses,
            };

            if (requestDTO.Type == InviteType.VoiceChat)
            {
                //var ChannelId = requestDTO.ChannelId.GetValueOrDefault();
                //if (ChannelId == 0)
                //{
                //    return false; //invalid invite channel for voice invite
                //}
                //var invite = new ServerVoiceInvite()
                //{
                //    ExpirationTime = requestDTO.TimeExpires,
                //    InviterId = senderId,
                //    SubjectId = serverid,
                //    TotalUses = requestDTO.TotalUses,
                //    GuestInvite = requestDTO.TemporaryMembership,
                //    ChannelId = ChannelId,
                //};
                //await context.Set<ServerVoiceInvite>().AddAsync(invite);
                invite.GuestInvite = requestDTO.TemporaryMembership;
                invite.VoiceChannelId = requestDTO.ChannelId;
            }
            else
            {
                invite.GuestInvite = requestDTO.TemporaryMembership;
                invite.TextChannelId = requestDTO.ChannelId;
            }
            await context.Set<ServerInvite>().AddAsync(invite);

            await context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> CreateRole(ulong senderId, ulong serverid, CreateRoleRequestDTO requestDTO)
    {
        try
        {
            //verify sender is member
            var server = await context.Set<Server>()
                //annoys the fk out of me that you cant just include members.firstordefault where some condition on thenincludes
                .Include(e => e.Members.Where(e => e.AccountId == senderId).Take(1)) // filter members from db 
                .ThenInclude(e => e.Roles) //include sender role grants and the role as well as their permissions to see if they have permission to change server image
                .ThenInclude(e => e.Role)
                .ThenInclude(e => e.Permissions) //you dont actually need to pull all permissions if you know the ids of the reused serverpermissions.
                .ThenInclude(e => e.Permission)
                .Include(e => e.Roles) //will presumably always have one role which everyone is granted if not then servercreation went wrong
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == serverid);
            if (server == null)
            {
                //cant process nonexistant server
                return false;
            }
            var sender = server.Members.FirstOrDefault(); //if any then sender is here since already filtered // else null
            var isMember = sender != null;
            if (!isMember)
            {
                return false;
            }
            var allowedPermissions = new string[] { "manage roles", "administrator" };
            var HasPermission = sender.IsOwner; //if the account is serverowner then allow and skip role permission processing
            HasPermission = HasPermission || FindHierarchalPermissionFromRoles(sender, allowedPermissions); //keep true if true else assign from role permission state
            if (!HasPermission) //if they still dont have permission
            {
                return false;
            }

            var leastImportantRole = server.Roles.OrderBy(e => e.Importance).First(); //presumably "everyone" or "servermember" role

            //create defaulted role with request name. (the client can customize it after via edit)
            var role = new ServerRole()
            {
                Name = requestDTO.Name,
                Importance = leastImportantRole.Importance + 1, //grant more importance to a new role than lowest role.
                AllowAnyoneToMention = true,
                DisplaySeperatelyFromOnlineMembers = false,
                Colour = leastImportantRole.Colour,
                IconURL = leastImportantRole.IconURL,
                IsAdmin = false,
                OwnerId = serverid,
                //dont grant permissions as client will have to find permissions which have been set and compare to permissions from all allowed permissions within the context of the view.
                //Permissions = , 
                //(makes it so we dont have to store unset permissions in db)
            };

            await context.Set<ServerRole>().AddAsync(role);

            await context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> CreateServer(ulong senderId, CreateServerRequestDTO requestDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CreateSoundboardSound(ulong senderId, ulong serverid, CreateSoundboardSoundRequestDTO requestDTO)
    {
        try
        {
            //verify sender is member
            var server = await context.Set<Server>()
                //annoys the fk out of me that you cant just include members.firstordefault where some condition on thenincludes
                .Include(e => e.Members.Where(e => e.AccountId == senderId).Take(1)) // filter members from db 
                .ThenInclude(e => e.Roles) //include sender role grants and the role as well as their permissions to see if they have permission to change server image
                .ThenInclude(e => e.Role)
                .ThenInclude(e => e.Permissions) //you dont actually need to pull all permissions if you know the ids of the reused serverpermissions.
                .ThenInclude(e => e.Permission)
                //.Include(e => e.Emotes.Where(e => e.Id == emoteId))
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == serverid);
            if (server == null)
            {
                //cant process nonexistant server
                return false;
            }
            var sender = server.Members.FirstOrDefault(); //if any then sender is here since already filtered // else null
            var isMember = sender != null;
            if (!isMember)
            {
                return false;
            }
            var allowedPermissions = new string[] { "create expressions", "administrator" };
            var HasPermission = sender.IsOwner; //if the account is serverowner then allow and skip role permission processing
            HasPermission = HasPermission || FindHierarchalPermissionFromRoles(sender, allowedPermissions); //keep true if true else assign from role permission state
            if (!HasPermission) //if they still dont have permission
            {
                return false;
            }

            var sound = new ServerSoundboardSound()
            {
                UploaderId = senderId,
                ServerId = serverid,
                Name = requestDTO.Name,
                AssociatedEmoteId = requestDTO.EmoteId,
                SoundFileUrl = requestDTO.SoundFileURL,
            };

            await context.Set<ServerSoundboardSound>().AddAsync(sound);

            await context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> DeleteEmote(ulong senderId, ulong serverid, ulong emoteId)
    {
        try
        {
            //verify sender is member
            var server = await context.Set<Server>()
                //annoys the fk out of me that you cant just include members.firstordefault where some condition on thenincludes
                .Include(e => e.Members.Where(e => e.AccountId == senderId).Take(1)) // filter members from db 
                .ThenInclude(e => e.Roles) //include sender role grants and the role as well as their permissions to see if they have permission to change server image
                .ThenInclude(e => e.Role)
                .ThenInclude(e => e.Permissions) //you dont actually need to pull all permissions if you know the ids of the reused serverpermissions.
                .ThenInclude(e => e.Permission)
                .Include(e => e.Emotes.Where(e => e.Id == emoteId).Take(1))
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == serverid);
            if (server == null || !server.Emotes.Any())
            {
                //cant process nonexistant server or emote
                return false;
            }
            var sender = server.Members.FirstOrDefault(); //if any then sender is here since already filtered // else null
            var specifiedEmote = server.Emotes.FirstOrDefault(); //if any then sender is here since already filtered // else null
            var isMember = sender != null;
            if (!isMember)
            {
                return false;
            }
            var allowedPermissions = new string[] { "manage expressions", "administrator" };
            var HasPermission = sender.IsOwner; //if the account is serverowner then allow and skip role permission processing
            HasPermission = HasPermission || FindHierarchalPermissionFromRoles(sender, allowedPermissions); //keep true if true else assign from role permission state
            if (!HasPermission) //if they still dont have permission
            {
                return false;
            }

            context.Set<ServerEmote>().Remove(specifiedEmote);

            await context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> DeleteEvent(ulong senderId, ulong serverid, ulong eventId)
    {
        try
        {
            //verify sender is member
            var server = await context.Set<Server>()
                //annoys the fk out of me that you cant just include members.firstordefault where some condition on thenincludes
                .Include(e => e.Members.Where(e => e.AccountId == senderId).Take(1)) // filter members from db 
                .ThenInclude(e => e.Roles) //include sender role grants and the role as well as their permissions to see if they have permission to change server image
                .ThenInclude(e => e.Role)
                .ThenInclude(e => e.Permissions)//you dont actually need to pull all permissions if you know the ids of the reused serverpermissions.
            .ThenInclude(e => e.Permission)
                .Include(e => e.Events.Where(e => e.Id == eventId).Take(1))
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == serverid);
            if (server == null || !server.Events.Any())
            {
                //cant process nonexistant server or event
                return false;
            }
            var sender = server.Members.FirstOrDefault(); //if any then sender is here since already filtered // else null
            var specifiedEvent = server.Events.FirstOrDefault(); //if any then sender is here since already filtered // else null
            var isMember = sender != null;
            if (!isMember)
            {
                return false;
            }
            var allowedPermissions = new string[] { "manage events", "administrator" };
            var HasPermission = sender.IsOwner; //if the account is serverowner then allow and skip role permission processing
            HasPermission = HasPermission || FindHierarchalPermissionFromRoles(sender, allowedPermissions); //keep true if true else assign from role permission state
            if (!HasPermission) //if they still dont have permission
            {
                return false;
            }

            context.Set<ServerEvent>().Remove(specifiedEvent);

            await context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> DeleteRole(ulong senderId, ulong serverid, ulong roleId)
    {
        try
        {
            //verify sender is member
            var server = await context.Set<Server>()
                //annoys the fk out of me that you cant just include members.firstordefault where some condition on thenincludes
                .Include(e => e.Members.Where(e => e.AccountId == senderId).Take(1)) // filter members from db 
                .ThenInclude(e => e.Roles) //include sender role grants and the role as well as their permissions to see if they have permission to change server image
                .ThenInclude(e => e.Role)
                .ThenInclude(e => e.Permissions) //you dont actually need to pull all permissions if you know the ids of the reused serverpermissions.
                .ThenInclude(e => e.Permission)
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == serverid);
            var specifiedRole = server.Roles.FirstOrDefault(e => e.Id == roleId); //role if present
            if (server == null || specifiedRole == null)
            {
                //cant process nonexistant server or role
                return false;
            }
            var sender = server.Members.FirstOrDefault(); //if any then sender is here since already filtered // else null
            var isMember = sender != null;
            if (!isMember)
            {
                return false;
            }

            var senderTopRole = sender.Roles.Select(e => e.Role).OrderBy(e => e.Importance).First(); //sender will have a role of some sorts probably
            if (!sender.IsOwner && specifiedRole.Importance >= senderTopRole.Importance) //if sender is not owner then they are never allowed to process a role that has higher or equal status to their own toprole.
            {
                return false;
            }

            var allowedPermissions = new string[] { "manage roles", "administrator" };
            var HasPermission = sender.IsOwner; //if the account is serverowner then allow and skip role permission processing
            HasPermission = HasPermission || FindHierarchalPermissionFromRoles(sender, allowedPermissions); //keep true if true else assign from role permission state
            if (!HasPermission) //if they still dont have permission
            {
                return false;
            }

            context.Set<ServerRole>().Remove(specifiedRole);

            await context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> DeleteServer(ulong senderId, ulong serverId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteSoundboardSound(ulong senderId, ulong serverid, ulong soundId)
    {
        try
        {
            //verify sender is member
            var server = await context.Set<Server>()
                //annoys the fk out of me that you cant just include members.firstordefault where some condition on thenincludes
                .Include(e => e.Members.Where(e => e.AccountId == senderId).Take(1)) // filter members from db 
                .ThenInclude(e => e.Roles) //include sender role grants and the role as well as their permissions to see if they have permission to change server image
                .ThenInclude(e => e.Role)
                .ThenInclude(e => e.Permissions) //you dont actually need to pull all permissions if you know the ids of the reused serverpermissions.
                .ThenInclude(e => e.Permission)
                .Include(e => e.SoundboardSounds.Where(e => e.Id == soundId))
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == serverid);
            if (server == null || !server.SoundboardSounds.Any())
            {
                //cant process nonexistant server or soundboard sound
                return false;
            }
            var sender = server.Members.FirstOrDefault(); //if any then sender is here since already filtered // else null
            var specifiedSound = server.SoundboardSounds.FirstOrDefault(); //if any then sound is here since already filtered
            var isMember = sender != null;
            if (!isMember)
            {
                return false;
            }
            var allowedPermissions = new string[] { "manage expressions", "administrator" };
            var HasPermission = sender.IsOwner; //if the account is serverowner then allow and skip role permission processing
            HasPermission = HasPermission || FindHierarchalPermissionFromRoles(sender, allowedPermissions); //keep true if true else assign from role permission state
            if (!HasPermission) //if they still dont have permission
            {
                return false;
            }

            context.Set<ServerSoundboardSound>().Remove(specifiedSound);

            await context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> EditEmote(ulong senderId, ulong serverid, ulong emoteId, EditEmoteRequestDTO requestDTO)
    {
        try
        {
            //verify sender is member
            var server = await context.Set<Server>()
                //annoys the fk out of me that you cant just include members.firstordefault where some condition on thenincludes
                .Include(e => e.Members.Where(e => e.AccountId == senderId).Take(1)) // filter members from db 
                .ThenInclude(e => e.Roles) //include sender role grants and the role as well as their permissions to see if they have permission to change server image
                .ThenInclude(e => e.Role)
                .ThenInclude(e => e.Permissions) //you dont actually need to pull all permissions if you know the ids of the reused serverpermissions.
                .ThenInclude(e => e.Permission)
                .Include(e => e.Emotes.Where(e => e.Id == emoteId))
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == serverid);
            if (server == null || !server.Emotes.Any())
            {
                //cant process nonexistant server or emote
                return false;
            }
            var sender = server.Members.FirstOrDefault(); //if any then sender is here since already filtered // else null
            var specifiedEmote = server.Emotes.FirstOrDefault(); //if any then sender is here since already filtered // else null
            var isMember = sender != null;
            if (!isMember)
            {
                return false;
            }
            var allowedPermissions = new string[] { "manage expressions", "administrator" };
            var HasPermission = sender.IsOwner; //if the account is serverowner then allow and skip role permission processing
            HasPermission = HasPermission || FindHierarchalPermissionFromRoles(sender, allowedPermissions); //keep true if true else assign from role permission state
            if (!HasPermission) //if they still dont have permission
            {
                return false;
            }

            specifiedEmote.Name = requestDTO.Name;

            await context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> EditEvent(ulong senderId, ulong serverid, ulong eventId, EditEventRequestDTO requestDTO)
    {
        try
        {
            //verify sender is member
            var server = await context.Set<Server>()
                //annoys the fk out of me that you cant just include members.firstordefault where some condition on thenincludes
                .Include(e => e.Members.Where(e => e.AccountId == senderId).Take(1)) // filter members from db 
                .ThenInclude(e => e.Roles) //include sender role grants and the role as well as their permissions to see if they have permission to change server image
                .ThenInclude(e => e.Role)
                .ThenInclude(e => e.Permissions)//you dont actually need to pull all permissions if you know the ids of the reused serverpermissions.
            .ThenInclude(e => e.Permission)
                .Include(e => e.Events.Where(e => e.Id == eventId))
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == serverid);
            if (server == null || !server.Events.Any())
            {
                //cant process nonexistant server or event
                return false;
            }
            var sender = server.Members.FirstOrDefault(); //if any then sender is here since already filtered // else null
            var specifiedEvent = server.Events.FirstOrDefault(); //if any then sender is here since already filtered // else null
            var isMember = sender != null;
            if (!isMember)
            {
                return false;
            }
            var allowedPermissions = new string[] { "manage events", "administrator" };
            var HasPermission = sender.IsOwner; //if the account is serverowner then allow and skip role permission processing
            HasPermission = HasPermission || FindHierarchalPermissionFromRoles(sender, allowedPermissions); //keep true if true else assign from role permission state
            if (!HasPermission) //if they still dont have permission
            {
                return false;
            }

            specifiedEvent.StartTime = requestDTO.TimeStarts;
            specifiedEvent.EndTime = requestDTO.TimeEnds;

            await context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> EditRole(ulong senderId, ulong serverid, ulong roleId, EditRoleRequestDTO requestDTO)
    {
        try
        {
            //verify sender is member
            var server = await context.Set<Server>()
               .Include(e => e.Members.Where(e => e.AccountId == senderId).Take(1))
               .ThenInclude(e => e.Roles) //filter participants from db
               .Include(e => e.Roles) //include all roles
               .ThenInclude(e => e.Permissions)//you dont actually need to pull all permissions if you know the ids of the reused serverpermissions.
               .ThenInclude(e => e.Permission)
               .Include(e => e.Settings)
               .AsSplitQuery()
               .FirstOrDefaultAsync(e => e.Id == serverid);
            if (server == null || !server.Roles.Any(e => e.Id == roleId))
            {
                //cant process nonexistant server or role.
                return false;
            }
            var sender = server.Members.FirstOrDefault();
            var isMember = sender != null; //if any then sender is here since already filtered
            if (!isMember)
            {
                return false;
            }

            //sender will have a role of some sorts probably
            var senderTopRole = sender.Roles.Select(e => e.Role).OrderBy(e => e.Importance).First();
            var editedRole = server.Roles.FirstOrDefault(e => e.Id == roleId);
            if (!sender.IsOwner && editedRole.Importance >= senderTopRole.Importance)
            //if sender is not owner then they are never allowed to process
            //a role that has higher or equal status to their own toprole.
            {
                return false;
            }

            //do permission check if has permission.
            var allowedPermissions = new string[] { "manage roles", "administrator" };
            //if the account is serverowner then allow
            //and skip role permission processing
            var HasPermission = sender.IsOwner;
            //keep true if true else assign from role permission state
            HasPermission = HasPermission || FindHierarchalPermissionFromRoles(sender, allowedPermissions);
            if (!HasPermission) //if they still dont have permission
            {
                return false;
            }

            editedRole.Name = requestDTO.Name;
            editedRole.Importance = requestDTO.Importance;
            editedRole.IconURL = requestDTO.ImageURL;
            editedRole.DisplaySeperatelyFromOnlineMembers = requestDTO.DisplaySeperatelyFromOnline;
            editedRole.AllowAnyoneToMention = requestDTO.AllowAnyoneMention;
            editedRole.Colour = requestDTO.Colour;

            var res = await context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> EditServer(ulong senderId, ulong serverid, EditServerRequestDTO requestDTO)
    {
        try
        {
            //verify sender is member
            var server = await context.Set<Server>()
                .Include(e => e.Members.Where(e => e.AccountId == senderId).Take(1))
                .ThenInclude(e => e.Roles) //filter participants from db
                .ThenInclude(e => e.Role)
                .ThenInclude(e => e.Permissions)//you dont actually need to pull all permissions if you know the ids of the reused serverpermissions.
                .ThenInclude(e => e.Permission)
                .Include(e => e.Settings)
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == serverid);
            if (server == null)
            {
                //cant process nonexistant server
                return false;
            }
            var sender = server.Members.FirstOrDefault();
            var isMember = sender != null; //if any then sender is here since already filtered
            if (!isMember)
            {
                return false;
            }

            var allowedPermissions = new string[] { "manage server", "administrator" };
            var HasPermission = sender.IsOwner; //if the account is serverowner then allow and skip role permission processing
            HasPermission = HasPermission || FindHierarchalPermissionFromRoles(sender, allowedPermissions); //keep true if true else assign from role permission state
            if (!HasPermission) //if they still dont have permission
            {
                return false;
            }

            server.Settings.RegionId = requestDTO.RegionId;
            server.Settings.InactiveChannelId = requestDTO.InactiveChannelId;
            server.Settings.SystemMessagesChannelId = requestDTO.SystemMessagesChannelId;
            server.Settings.ServerImageUrl = requestDTO.ServerImageURL;
            server.Name = requestDTO.Name;
            server.Settings.SendRandomWelcomeMessageWhenSomeoneJoins = requestDTO.SendRandomWelcomeMessageWhenSomeoneJoins;
            server.Settings.PromptMembersToReplyWithASticker = requestDTO.PromptMembersToReplyWithASticker;
            server.Settings.SendHelpfulTipsForServerSetup = requestDTO.SendHelpfulTipsForServerSetup;
            server.Settings.ShowMembersInChannelList = requestDTO.ShowMembersInChannelList;
            server.Settings.Require2FAForModeratorActions = requestDTO.Require2FAForModeratorActions;
            server.Settings.DefaultNotificationSettingsMode = requestDTO.DefaultNotificationSettingsMode;
            server.Settings.VerificationLevelMode = requestDTO.VerificationLevelMode;
            server.Settings.ExplicitImageFilterMode = requestDTO.ExplicitImageFilterMode;

            var res = await context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> EditSoundboardSound(ulong senderId, ulong serverid, ulong soundId, EditSoundboardSoundRequestDTO requestDTO)
    {
        try
        {
            //verify sender is member
            var server = await context.Set<Server>()
                //annoys the fk out of me that you cant just include members.firstordefault where some condition on thenincludes
                .Include(e => e.Members.Where(e => e.AccountId == senderId).Take(1)) // filter members from db 
                .ThenInclude(e => e.Roles) //include sender role grants and the role as well as their permissions to see if they have permission to change server image
                .ThenInclude(e => e.Role)
                .ThenInclude(e => e.Permissions)//you dont actually need to pull all permissions if you know the ids of the reused serverpermissions.
                .ThenInclude(e => e.Permission)
                .Include(e => e.SoundboardSounds.Where(e => e.Id == soundId))
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == serverid);
            if (server == null || !server.SoundboardSounds.Any())
            {
                //cant process nonexistant server or sound
                return false;
            }
            var sender = server.Members.FirstOrDefault(); //if any then sender is here since already filtered // else null
            var specifiedSound = server.SoundboardSounds.FirstOrDefault(); //if any then sound is here since already filtered // else null
            var isMember = sender != null;
            if (!isMember)
            {
                return false;
            }
            var allowedPermissions = new string[] { "manage expressions", "administrator" };
            var HasPermission = sender.IsOwner; //if the account is serverowner then allow and skip role permission processing
            HasPermission = HasPermission || FindHierarchalPermissionFromRoles(sender, allowedPermissions); //keep true if true else assign from role permission state
            if (!HasPermission) //if they still dont have permission
            {
                return false;
            }

            specifiedSound.Name = requestDTO.Name;
            specifiedSound.AssociatedEmoteId = requestDTO.EmoteId;
            specifiedSound.Volume = requestDTO.Volume;

            await context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> JoinEvent(ulong senderId, ulong serverid, ulong eventId)
    {
        try
        {
            //verify sender is member
            var server = await context.Set<Server>()
                //annoys the fk out of me that you cant just include members.firstordefault where some condition on thenincludes
                .Include(e => e.Members.Where(e => e.AccountId == senderId).Take(1)) // filter members from db 
                .ThenInclude(e => e.Roles) //include sender role grants and the role as well as their permissions to see if they have permission to change server image
                .ThenInclude(e => e.Role)
                .ThenInclude(e => e.Permissions)//you dont actually need to pull all permissions if you know the ids of the reused serverpermissions.
                .ThenInclude(e => e.Permission)
                .Include(e => e.Events.Where(e => e.Id == eventId)).ThenInclude(e => e.Participants.Where(e => e.AccountId == senderId))
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == serverid);
            if (server == null || !server.Events.Any())
            {
                //cant process nonexistant server or event
                return false;
            }
            var sender = server.Members.FirstOrDefault(); //if any then sender is here since already filtered // else null
            var isMember = sender != null;
            if (!isMember)
            {
                return false;
            }

            var participancy = new ServerEventParticipancy()
            {
                AccountId = senderId,
                ServerId = serverid,
                EventId = eventId,
            };

            await context.Set<ServerEventParticipancy>().AddAsync(participancy);

            await context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> LeaveEvent(ulong senderId, ulong serverid, ulong eventId)
    {
        try
        {
            //verify sender is member
            var server = await context.Set<Server>()
                //annoys the fk out of me that you cant just include members.firstordefault where some condition on thenincludes
                .Include(e => e.Members.Where(e => e.AccountId == senderId).Take(1)) // filter members from db 
                .ThenInclude(e => e.Roles) //include sender role grants and the role as well as their permissions to see if they have permission to change server image
                .ThenInclude(e => e.Role)
                .ThenInclude(e => e.Permissions)//you dont actually need to pull all permissions if you know the ids of the reused serverpermissions.
                .ThenInclude(e => e.Permission)
                .Include(e => e.Events.Where(e => e.Id == eventId)).ThenInclude(e => e.Participants.Where(e => e.AccountId == senderId))
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == serverid);
            if (server == null || !server.Events.Any())
            {
                //cant process nonexistant server or event
                return false;
            }
            var sender = server.Members.FirstOrDefault(); //if any then sender is here since already filtered // else null
            var isMember = sender != null;
            if (!isMember)
            {
                return false;
            }

            var participancy = new ServerEventParticipancy()
            {
                AccountId = senderId,
                ServerId = serverid,
                EventId = eventId,
            };

            context.Set<ServerEventParticipancy>().Remove(participancy);

            await context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> LeaveServer(ulong senderId, ulong serverId)
    {
        try
        {
            //verify sender is member
            var serverProfile = await context.Set<ServerProfile>()
                .Include(e => e.VoiceChannelMemberPermissions)
                .Include(e => e.VoiceChannelMemberSettings)
                .Include(e => e.TextChannelMemberPermissions)
                .Include(e => e.TextChannelMemberSettings)
                .Include(e => e.CategoryMemberPermissions)
                .Include(e => e.CategoryMemberSettings)
                .Include(e => e.Roles)
                .Include(e => e.Account) //include account to get easy access to loaded message trackers
                .Include(e => e.Server).ThenInclude(e => e.TextChannels).ThenInclude(e => e.MessageTrackers.Where(e => e.OwnerId == senderId))
                .Include(e => e.Server).ThenInclude(e => e.TextChannels).ThenInclude(e => e.Muters.Where(e => e.MuterId == senderId))
                .Include(e => e.Server).ThenInclude(e => e.VoiceChannels).ThenInclude(e => e.Muters.Where(e => e.MuterId == senderId))
                .Include(e => e.Server).ThenInclude(e => e.Muters)
                //.Include(e=>e.Server).ThenInclude(e=>e.Invites)
                //.Include(e=>e.Server).ThenInclude(e=>e.voice).ThenInclude(e=>e.Muters.Where(e=>e.MuterId==senderId))
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.ServerId == senderId && e.AccountId == senderId);
            var isMember = serverProfile != null;
            if (!isMember)
            {
                //cant process if not member.
                return false;
            }
            if (serverProfile.IsOwner)
            {
                //cant leave if serverowner then must transfer ownership or delete server instead
                return false;
            }

            //cleanup relations for sender account. (wait with invite cause we dont know how to invalidate it yet.)
            //role stuff
            context.Set<ServerProfileServerRole>().RemoveRange(serverProfile.Roles); //only senders rolegrants will be included in context here.
            context.Set<ServerChannelCategoryMemberPermission>().RemoveRange(serverProfile.CategoryMemberPermissions); //only senders member permissions will be included in context here.
            context.Set<ServerChannelCategoryMemberSettings>().RemoveRange(serverProfile.CategoryMemberSettings); //only senders member settings will be included in context here.
            context.Set<ServerTextChannelMemberPermission>().RemoveRange(serverProfile.TextChannelMemberPermissions); //only senders member permissions will be included in context here.
            context.Set<ServerTextChannelMemberSettings>().RemoveRange(serverProfile.TextChannelMemberSettings); //only senders member settings will be included in context here.
            context.Set<ServerVoiceChannelMemberPermission>().RemoveRange(serverProfile.VoiceChannelMemberPermissions); //only senders member permissions will be included in context here.
            context.Set<ServerVoiceChannelMemberSettings>().RemoveRange(serverProfile.VoiceChannelMemberSettings); //only senders member settings will be included in context here.

            //channel stuff
            context.Set<AccountServerTextChannelMessageTracker>().RemoveRange(serverProfile.Account.TextChannelMessageTrackers); //only senders message trackers will be included in context here.
            context.Set<AccountServerVoiceChannelMute>().RemoveRange(serverProfile.Server.VoiceChannels.SelectMany(e => e.Muters)); //only senders mutes will be included in context
            context.Set<AccountServerVoiceChannelMute>().RemoveRange(serverProfile.Server.VoiceChannels.SelectMany(e => e.Muters)); //only senders mutes will be included in context
            context.Set<AccountServerTextChannelMute>().RemoveRange(serverProfile.Server.TextChannels.SelectMany(e => e.Muters)); //only senders mutes will be included in context

            //global stuff
            context.Set<AccountServerMute>().Remove(serverProfile.Server.Muters.FirstOrDefault()); //only senders mutes will be included in context 
            //context.Set<ServerVoiceMute>().RemoveRange(serverProfile.Server.MutedVoices); //only senders mutes will be included in context //excluded for now
            //context.Set<ServerDeafen>().RemoveRange(serverProfile.Server.Deafened); //only senders mutes will be included in context //excluded for now
            //context.Set<ServerInvite>().RemoveRange(chat.Invites); //maybe wait with this one
            //context.Set<ServerVoiceInvite>().RemoveRange(chat.Invites); //maybe wait with this one
            context.Set<ServerProfile>().Remove(serverProfile);

            await context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> MarkAsRead(ulong senderId, ulong serverId)
    {
        try
        {
            //verify sender is member
            var channels = await context.Set<ServerTextChannel>()
                .Include(e => e.Messages.OrderByDescending(m => m.TimeSent).Take(1)) //get newest message
                .Include(e => e.MessageTrackers.Where(t => t.OwnerId == senderId).Take(1)) //get users tracking relation
                .Include(e => e.Owner)
                .ThenInclude(e => e.Members.Where(t => t.AccountId == senderId).Take(1)) //get users participation
                .AsSplitQuery()
                .Where(e => e.Id == serverId)
                .ToListAsync();
            if (channels == null || !channels.Any())
            {
                //cant process non existant channels.
                return false;
            }
            var isMember = channels.First().Owner.Members.Any(); //if any then sender is here since already filtered
            if (!isMember)
            {
                return false;
            }
            foreach (var channel in channels) // loop over channels create trackers if not present and assign newest message to be tracked if any
            {
                var tracker = channel.MessageTrackers.FirstOrDefault();
                if (tracker == null)
                {
                    tracker = new()
                    {
                        CoOwnerId = channel.Id,
                        OwnerId = senderId,
                    };
                    await context.Set<AccountServerTextChannelMessageTracker>().AddAsync(tracker);
                }
                var newestMsgId = channel.Messages.FirstOrDefault()?.Id; //if no messages assign null or if messages assign id
                tracker.SubjectId = newestMsgId;
            }

            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> MuteServer(ulong senderId, ulong serverId, MuteRequestDTO requestDTO)
    {
        try
        {
            var isMember = context.Set<ServerProfile>().Any(o => o.AccountId == senderId && o.ServerId == serverId);
            if (!isMember)
            {
                return false;
            }

            AccountServerMute relation = new()
            {
                MuterId = senderId,
                SubjectId = serverId,
            };

            await context.Set<AccountServerMute>().AddAsync(relation); //throws error if already muted fyi

            var res = await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveInvite(ulong senderId, ulong serverid, string inviteCode)
    {
        try
        {
            //verify sender is member
            var server = await context.Set<Server>()
                //annoys the fk out of me that you cant just include members.firstordefault where some condition on thenincludes
                .Include(e => e.Members.Where(e => e.AccountId == senderId).Take(1)) // filter members from db 
                .ThenInclude(e => e.Roles) //include sender role grants and the role as well as their permissions to see if they have permission to change server image
                .ThenInclude(e => e.Role)
                .ThenInclude(e => e.Permissions)//you dont actually need to pull all permissions if you know the ids of the reused serverpermissions.
                .ThenInclude(e => e.Permission)
                .Include(e => e.Invites.Where(e => e.InviteCode == inviteCode).Take(1))
                //.Include(e => e.VoiceInvites.Where(e => e.InviteCode == inviteCode).Take(1))
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == serverid);
            if (server == null || !server.Invites.Any())
            {
                //cant process nonexistant server or invites
                return false;
            }
            var sender = server.Members.FirstOrDefault(); //if any then sender is here since already filtered // else null
            var isMember = sender != null;
            if (!isMember)
            {
                return false;
            }

            var allowedPermissions = new string[] { "manage server", "administrator" };
            var HasPermission = sender.IsOwner; //if the account is serverowner then allow and skip role permission processing
            HasPermission = HasPermission || FindHierarchalPermissionFromRoles(sender, allowedPermissions); //keep true if true else assign from role permission state
            if (!HasPermission) //if they still dont have permission
            {
                return false;
            }

            var specifiedInvite = server.Invites.First(); //if any then sender is here since already filtered // else null
            context.Set<ServerInvite>().Remove(specifiedInvite);
            //if (type==InviteType.Server)
            //{
            //}
            //else
            //{
            //    var specifiedInvite = server.VoiceInvites.First(); //if any then sender is here since already filtered // else null
            //    context.Set<ServerVoiceInvite>().Remove(specifiedInvite);
            //}

            await context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> SetRolePermission(ulong senderId, ulong serverid, ulong roleId, SetPermissionStateRequestDTO requestDTO)
    {
        try
        {
            //verify sender is member
            var server = await context.Set<Server>()
               .Include(e => e.Members.Where(e => e.AccountId == senderId).Take(1))
               .ThenInclude(e => e.Roles) //filter participants from db
               .Include(e => e.Roles) //include all roles
               .ThenInclude(e => e.Permissions) //you dont actually need to pull all permissions if you know the ids of the reused serverpermissions.
               .ThenInclude(e => e.Permission)
               //.Include(e => e.Settings)
               .AsSplitQuery()
               .FirstOrDefaultAsync(e => e.Id == serverid);
            if (server == null || !server.Roles.Any(e => e.Id == roleId))
            {
                //cant process nonexistant server or role.
                return false;
            }
            var sender = server.Members.FirstOrDefault();
            var isMember = sender != null; //if any then sender is here since already filtered
            if (!isMember)
            {
                return false;
            }

            var senderTopRole = sender.Roles.Select(e => e.Role).OrderBy(e => e.Importance).First(); //sender will have a role of some sorts probably
            var editedRole = server.Roles.FirstOrDefault(e => e.Id == roleId);
            if (!sender.IsOwner && editedRole.Importance >= senderTopRole.Importance) //if sender is not owner then they are never allowed to process a role that has higher or equal status to their own toprole.
            {
                return false;
            }

            //do permission check if has permission.
            var allowedPermissions = new string[] { "manage roles", "administrator" };
            var HasPermission = sender.IsOwner; //if the account is serverowner then allow and skip role permission processing
            HasPermission = HasPermission || FindHierarchalPermissionFromRoles(sender, allowedPermissions); //keep true if true else assign from role permission state
            if (!HasPermission) //if they still dont have permission
            {
                return false;
            }

            var newPerm = new ServerRolePermission()
            {
                PermissionId = requestDTO.PermissionId,
                State = requestDTO.State,
                RoleId = roleId
            };
            var existingPerm = editedRole.Permissions.FirstOrDefault(e => e.PermissionId == newPerm.PermissionId);//find ones to update
            if (existingPerm != null)
            {
                existingPerm.State = newPerm.State;
            }
            else
            {
                //add if not exists
                await context.Set<ServerRolePermission>().AddAsync(newPerm);
            }

            //removal of perms with null state will be handled by some background service at regular intervals

            var res = await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> SetRolePermissions(ulong senderId, ulong serverid, ulong roleId, SetMultiplePermissionStateRequestDTO requestDTO)
    {
        try
        {
            //verify sender is member
            var server = await context.Set<Server>()
               .Include(e => e.Members.Where(e => e.AccountId == senderId).Take(1))
               .ThenInclude(e => e.Roles) //filter participants from db
               .Include(e => e.Roles) //include all roles
               .ThenInclude(e => e.Permissions) //you dont actually need to pull all permissions if you know the ids of the reused serverpermissions.
               .ThenInclude(e => e.Permission)
               //.Include(e => e.Settings)
               .AsSplitQuery()
               .FirstOrDefaultAsync(e => e.Id == serverid);
            if (server == null || !server.Roles.Any(e => e.Id == roleId))
            {
                //cant process nonexistant server or role.
                return false;
            }
            var sender = server.Members.FirstOrDefault();
            var isMember = sender != null; //if any then sender is here since already filtered
            if (!isMember)
            {
                return false;
            }

            var senderTopRole = sender.Roles.Select(e => e.Role).OrderBy(e => e.Importance).First(); //sender will have a role of some sorts probably
            var editedRole = server.Roles.FirstOrDefault(e => e.Id == roleId);
            if (!sender.IsOwner && editedRole.Importance >= senderTopRole.Importance) //if sender is not owner then they are never allowed to process a role that has higher or equal status to their own toprole.
            {
                return false;
            }

            //do permission check if has permission.
            var allowedPermissions = new string[] { "manage roles", "administrator" };
            var HasPermission = sender.IsOwner; //if the account is serverowner then allow and skip role permission processing
            HasPermission = HasPermission || FindHierarchalPermissionFromRoles(sender, allowedPermissions); //keep true if true else assign from role permission state
            if (!HasPermission) //if they still dont have permission
            {
                return false;
            }

            var newPerms = requestDTO.Permissions.Select(e => new ServerRolePermission()
            {
                PermissionId = e.Item1,
                State = e.Item2,
                RoleId = roleId
            });
            var newPermIds = newPerms.Select(e => e.PermissionId);
            var existingPerms = editedRole.Permissions.Where(e => newPermIds.Contains(e.PermissionId));//find ones to update
            foreach (var existingPermission in existingPerms)
            {
                existingPermission.State = newPerms.First(e => e.PermissionId == existingPermission.PermissionId).State;
            }
            //find ones to add
            var addedPerms = newPerms.Where(e => !newPermIds.Contains(e.PermissionId));
            await context.Set<ServerRolePermission>().AddRangeAsync(addedPerms);

            //removal of perms with null state will be handled by some background service at regular intervals

            var res = await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// finds global hierarchal permission from permission name string, 
    /// </summary>
    /// <param name="sender">must contain roles and their permissions mapped via navigation</param>
    /// <param name="permissionName">example: "manage server" (casing doesnt matter as it will be formatted in method)</param>
    /// <returns>authorative state. (top role permission state which is not null)</returns>
    private bool FindHierarchalPermissionFromRoles(ServerProfile sender, string permissionName)
    {
        //find set of permissions from scope
        var Roles = sender.Roles.Select(e => e.Role).OrderBy(e => e.Importance); //find roles order by importance
        var formattedPermissionName = permissionName.ToLower();
        //since permission scope can only be serverglobal we can just bruteforce
        ServerRolePermission serverPermission = null;
        foreach (var role in Roles) //loop through roles with highest importance (meaning their set permissions overrides lower importance role grants)
        {
            //find specific permission that would allow this action
            var permission = role.Permissions.FirstOrDefault(e => e.Permission.Name.ToLower() == formattedPermissionName && e.State != null);
            if (permission == null)
            {
                //if permission is null that means the current role does not have allow or disallow set meaning we should check the next role
                continue;
            }
            //if a role has been found then assign the role and return.
            serverPermission = permission;
            break;
        }
        return serverPermission.State.GetValueOrDefault(); //if the state of the permission which has been found is true 
    }

    /// <summary>
    /// finds global hierarchal permission from permission name string enumerable
    /// </summary>
    /// <param name="sender">must contain roles and their permissions mapped via navigation</param>
    /// <param name="permissionNames">example: ["manage server", "administrator"] (casing doesnt matter as it will be formatted in method)</param>
    /// <returns>authorative state. (top role permission state which is not null)</returns>
    private bool FindHierarchalPermissionFromRoles(ServerProfile sender, IEnumerable<string> permissionNames)
    {
        //find set of permissions from scope
        //find roles order by hierarchy
        var Roles = sender.Roles.Select(e => e.Role)
            .OrderBy(e => e.Importance);

        var formattedPermissionNames = permissionNames.Select(
            name => name.ToLower());

        //since permission scope can only be serverglobal we can just bruteforce
        ServerRolePermission serverPermission = new() { State = false };
        foreach (var role in Roles) //loop through roles with highest importance
                                    //(meaning their set permissions overrides lower importance role grants)
        {
            //find specific permission that would allow this action
            var permission = role.Permissions
                .FirstOrDefault(e => formattedPermissionNames
                .Contains(e.Permission.Name
                .ToLower()) && e.State != null);
            if (permission == null)
            {
                //if permission is null that means the current role does not have allow
                //or disallow set meaning we should check the next role
                continue;
            }
            //if a role has been found then assign the role and return.
            serverPermission = permission;
            break;
        }
        return serverPermission.State.GetValueOrDefault();
    }

    /// <summary>
    /// finds global hierarchal permission from permission name string enumerable
    /// </summary>
    /// <param name="sender">must contain roles and their permissions mapped via navigation</param>
    /// <param name="permissionNames">example: ["manage server", "administrator"] (casing doesnt matter as it will be formatted in method)</param>
    /// <returns>authorative state. (top role permission state which is not null)</returns>
    private bool FindScopedPermissionFromMember(ServerProfile sender, IEnumerable<string> permissionNames)
    {
        var formattedPermissionNames = permissionNames.Select(
            name => name.ToLower());
        //find set permissions for all scopes for the specific user
        //concat list and find relevant unique instances
        var setMemberPermissions = sender.CategoryMemberPermissions.Select(e => e.Permission)
            .Concat(sender.VoiceChannelMemberPermissions.Select(e => e.Permission))
            .Concat(sender.TextChannelMemberPermissions.Select(e => e.Permission))
            .DistinctBy(perm => perm.Id) //filter duplicates
            .Where(perm => formattedPermissionNames
            .Contains(perm.Name //find relevant
            .ToLower() //normalize
            ));

        if (!setMemberPermissions.Any())
        {
            return false;
        }
        //check if any relevant permissions are set to true
        var result = setMemberPermissions.Any(setPermission =>
        {
            bool? res = null;
            //check voicechat, or textchannelscope
            var textChanScope = setPermission.TextChannelMemberPermissions.FirstOrDefault();
            var voicChanScope = setPermission.VoiceChannelMemberPermissions.FirstOrDefault();
            //if permission is set assign value to result and break
            if (textChanScope != null && textChanScope.State != null
            || voicChanScope != null && voicChanScope.State != null)
            {
                //only overrides result if null
                res = res ?? textChanScope.State;
                res = res ?? voicChanScope.State;
            }
            //else check categoryscope
            var catScope = setPermission.CategoryMemberPermissions.FirstOrDefault();
            //if permission is set assign value to result and break
            if (catScope != null && catScope.State != null)
            {
                res = res ?? catScope.State;
            }
            return res.GetValueOrDefault();
        });
        return result;
    }

    public async Task<bool> SetServerImage(ulong senderId, ulong serverid, SetImageRequestDTO requestDTO)
    {
        try
        {
            //verify sender is member
            var server = await context.Set<Server>()
                .Include(e => e.Settings) // need settings object as this is where the image resides
                                          //annoys the fk out of me that you cant just include members.firstordefault where some condition on thenincludes
                .Include(e => e.Members.Where(e => e.AccountId == senderId).Take(1)) // filter members from db 
                .ThenInclude(e => e.Roles) //include sender role grants and the role as well as their permissions to see if they have permission to change server image
                .ThenInclude(e => e.Role)
                .ThenInclude(e => e.Permissions) //you dont actually need to pull all permissions if you know the ids of the reused serverpermissions.
                .ThenInclude(e => e.Permission)
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == serverid);
            if (server == null)
            {
                //cant process nonexistant server
                return false;
            }
            var sender = server.Members.FirstOrDefault(); //if any then sender is here since already filtered // else null
            var isMember = sender != null;
            if (!isMember)
            {
                return false;
            }
            var allowedPermissions = new string[] { "manage server", "administrator" };
            var HasPermission = sender.IsOwner; //if the account is serverowner then allow and skip role permission processing
            HasPermission = HasPermission || FindHierarchalPermissionFromRoles(sender, allowedPermissions); //keep true if true else assign from role permission state
            if (!HasPermission) //if they still dont have permission they cant change image
            {
                return false;
            }

            server.Settings.ServerImageUrl = requestDTO.ImageFileURL;

            await context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> StartEvent(ulong senderId, ulong serverid, ulong eventId)
    {
        try
        {
            //verify sender is member
            var server = await context.Set<Server>()
                //annoys the fk out of me that you cant just include members.firstordefault where some condition on thenincludes
                .Include(e => e.Members.Where(e => e.AccountId == senderId).Take(1)) // filter members from db 
                .ThenInclude(e => e.Roles) //include sender role grants and the role as well as their permissions to see if they have permission to change server image
                .ThenInclude(e => e.Role)
                .ThenInclude(e => e.Permissions) //you dont actually need to pull all permissions if you know the ids of the reused serverpermissions.
                .ThenInclude(e => e.Permission)
                .Include(e => e.Events.Where(e => e.Id == eventId))
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == serverid);
            if (server == null || !server.Events.Any())
            {
                //cant process nonexistant chat
                return false;
            }
            var sender = server.Members.FirstOrDefault(); //if any then sender is here since already filtered // else null
            var specifiedEvent = server.Events.FirstOrDefault(); //if any then sender is here since already filtered // else null
            var isMember = sender != null;
            if (!isMember)
            {
                return false;
            }
            var allowedPermissions = new string[] { "manage events", "administrator" };
            var HasPermission = sender.IsOwner; //if the account is serverowner then allow and skip role permission processing
            HasPermission = HasPermission || FindHierarchalPermissionFromRoles(sender, allowedPermissions); //keep true if true else assign from role permission state
            if (!HasPermission) //if they still dont have permission
            {
                return false;
            }

            specifiedEvent.StartTime = DateTime.Now;

            await context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> UnmuteServer(ulong senderId, ulong serverId)
    {
        try
        {
            //verify sender is member //doesnt matter here
            var isMember = await context.Set<ServerProfile>().AnyAsync(o => o.AccountId == senderId && o.ServerId == serverId);
            if (!isMember) //muting an account only mutes the volume so you should be able to mute direct message chats and chats both
            {
                return false;
            }
            AccountServerMute relation = new()
            {
                SubjectId = serverId,
                MuterId = senderId,
            };
            context.Set<AccountServerMute>().Remove(relation); //throws error if not muted

            var res = await context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }
}
