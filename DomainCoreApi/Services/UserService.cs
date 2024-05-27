using AutoMapper;
using Azure.Core;
using CoreLib.DTO.EchoCore.UserCore;
using CoreLib.DTO.RequestCore.FriendCore;
using CoreLib.DTO.RequestCore.RelationCore;
using CoreLib.DTO.RequestCore.UserCore;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ApplicationCore.Settings;
using CoreLib.Entities.EchoCore.FriendCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Repositorys;
using CoreLib.Interfaces.Services;
using DomainCoreApi.EFCORE;
using DomainCoreApi.Handlers;
using DomainCoreApi.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace DomainCoreApi.Services
{
    public class UserService : BaseEntityService<User, ulong>, IUserService
    {
        private readonly EchoDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IPasswordHandler _pwdHandler;
        private readonly IAccountService _accountService;
        private readonly IPushNotificationService notificationService;
        private readonly CreateUserHandler _createUserHandler = new();
        public UserService(EchoDbContext dbContext, IMapper mapper, IPushNotificationService notificationService, IUserRepository repository, IPasswordHandler pwdHandler, IAccountService accountService) : base(repository)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            _pwdHandler = pwdHandler;
            _accountService = accountService;
            this.notificationService = notificationService;
        }

        public async Task<bool> AcceptFriendRequestAsync(ulong senderId, ulong requestId)
        {
            try
            {
                Account acceptingAccount = new()
                {
                    Id = senderId,
                };
                var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                //verify acceptingacc is part of inc request.
                if (request != null && request.ReceiverId == senderId)
                {
                    Account senderAccount = new()
                    {
                        Id = request.SenderRequest.SenderId,
                    };
                    Friendship friendship = new()
                    {
                        Participants = new List<Account>(),
                    };
                    dbContext.Set<Friendship>().Attach(friendship);
                    friendship.Participants.Add(senderAccount);
                    friendship.Participants.Add(acceptingAccount);
                    var res = await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> AddUserConnectionAsync(ulong senderId, AddUserConnectionRequestDTO requestDTO)
        {
            try
            {
                Account senderAccount = new()
                {
                    Id = senderId,
                    Connections = new List<AccountConnection>()
                };
                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                AccountConnection connection = new()
                {
                    Name = requestDTO.Name,
                    Token = requestDTO.Token,
                    DisplayOnProfile = requestDTO.DisplayOnProfile,
                    ConnectionId = requestDTO.TypeId,
                };

                dbContext.Set<Account>().Attach(senderAccount);
                senderAccount.Connections.Add(connection);
                var res = await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> BlockUserAsync(ulong senderId, ulong userId)
        {
            try
            {
                Account senderAccount = new()
                {
                    Id = senderId,
                    BlockedAccounts = new List<AccountBlock>()
                };
                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                AccountBlock blockrelation = new()
                {
                    BlockedId = userId,
                    BlockerId = senderId,
                };

                dbContext.Set<Account>().Attach(senderAccount); //throws error if already blocked fyi
                senderAccount.BlockedAccounts.Add(blockrelation);
                var res = await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CancelFriendRequestAsync(ulong senderId, ulong requestId) //revoke //requests should share id so maybe no problem???
        {
            try
            {
                //verify the sender is sender
                var request = await dbContext.Set<OutgoingFriendRequest>().AsQueryable().FirstOrDefaultAsync(e => e.Id == requestId);
                if (request != null && request.SenderId == senderId)
                {
                    dbContext.Set<OutgoingFriendRequest>().Remove(request);
                    var res = await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> RegisterAsync(RegisterRequestDTO input)
        {
            try
            {
                Console.WriteLine(input);
                var data = await _createUserHandler.CreateHandler(input);
                var result = await _repository.AddAsync(data.Item1);
                data.Item2.UserId = result.Id;
                await _pwdHandler.CreatePassword(input.Password, data.Item1.Id);
                await _accountService.AddAsync(data.Item2);
            }
            catch (Exception e)
            {

                return false;
            }
                return true;
        }

        public async Task<bool> DeafenSelfAsync(ulong senderId)
        {
            try
            {
                VoiceSettings voiceSettings = new()
                {
                    Id = senderId,
                };


                dbContext.Set<VoiceSettings>().Attach(voiceSettings);
                voiceSettings.DeafenSelf = true;
                var res = await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeclineFriendRequestAsync(ulong senderId, ulong requestId)
        {
            try
            {
                //verify the sender is receiver
                var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                if (request != null && request.ReceiverId == senderId)
                {
                    dbContext.Set<OutgoingFriendRequest>().Remove(request.SenderRequest);
                    var res = await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public Task<bool> DeleteAccountAsync(ulong senderId, DeleteAccountRequestDTO requestDTO)
        {
            throw new NotImplementedException(); //idk not gonna implement maybe just soft delete or delete security credentials
        }

        public Task<bool> DisableAccountAsync(ulong senderId, DisableAccountRequestDTO requestDTO)
        {
            throw new NotImplementedException(); //not gonna implement maybe just enable some flag of a sort to show login is not enabled
        }

        public async Task<string> LoginAsync(LoginRequestDTO attempt)
        {
            var user = await _repository.GetSingleWithIncludeAsync(e => e.Email == attempt.Email);
            if (user is not null && await _pwdHandler.CheckPassword(attempt.Password, user.Id))
            {
                //send a jwt back to the website
                TokenHandler tokenHandler = new();
                return tokenHandler.CreateToken<Account>(user.Account);
            }

            throw new Exception("You suck at hacking bruv");
        }

        public async Task<bool> MuteSelfAsync(ulong senderId)
        {
            try
            {
                VoiceSettings voiceSettings = new()
                {
                    Id = senderId,
                };


                dbContext.Set<VoiceSettings>().Attach(voiceSettings);
                voiceSettings.MuteSelf = true;
                var res = await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> MuteUserAsync(ulong senderId, ulong userId, MuteRequestDTO requestDTO)
        {
            try
            {
                Account senderAccount = new()
                {
                    Id = senderId,
                    MutedVoices = new List<AccountMute>()
                };
                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                AccountMute muterelation = new()
                {
                    SubjectId = userId,
                    MuterId = senderId,
                };

                dbContext.Set<Account>().Attach(senderAccount); //throws error if already blocked fyi
                senderAccount.MutedVoices.Add(muterelation);
                var res = await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        

        public async Task<bool> RemoveFriendAsync(ulong senderId, ulong friendId)
        {
            //bad way to have friendships with this data or maybe make alternate key in friendship etc..
            try
            {
                Account acc = await dbContext.Set<Account>().AsQueryable().FirstOrDefaultAsync(e => e.Id == senderId);
                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                var removed = acc.Friendships.FirstOrDefault(e => e.Participants.Any(e=>e.Id==friendId));
                dbContext.Set<Friendship>().Remove(removed); //cannot make use of attach cause manytomany usingentity relation
                var res = await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> RemoveUserConnectionAsync(ulong senderId, ulong connectionId)
        {
            try
            {
                Account acc = await dbContext.Set<Account>().AsQueryable().Include(e=>e.Connections).FirstOrDefaultAsync(e => e.Id == senderId);
                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                var removed = acc.Connections.FirstOrDefault(e => e.Id == connectionId);
                acc.Connections.Remove(removed);
                var res = await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> SendFriendRequestAsync(ulong senderId, AddFriendRequestDTO requestDTO)
        {
            try
            {
                Account receiverAcc = await dbContext.Set<Account>().AsQueryable().FirstOrDefaultAsync(e=>e.Name==requestDTO.Name);
                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                IncomingFriendRequest request = new()
                {
                      ReceiverId=receiverAcc.Id,
                       SenderRequest = new()
                       {
                           SenderId=senderId
                       }
                };

                dbContext.Set<IncomingFriendRequest>().Add(request); //throws error if already blocked fyi
                var res = await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> SetCustomStatusAsync(ulong senderId, SetCustomStatusRequestDTO requestDTO) //check if this works??
        {
            try
            {
                Account acc = new()
                {
                    Id = senderId,
                    //something different than potential change //0 is always different than null and no id can have id 0 so its gucci
                };

                AccountCustomStatus status = new()
                {
                    Id= senderId,
                     CustomMessage= requestDTO.Content,
                     ExpirationTime = requestDTO.TimeExpires,
                };

                //efficient update
                //await dbContext.Set<AccountCustomStatus>().AsQueryable().Where(e=>e.Id==senderId)
                //    .ExecuteUpdateAsync(setters => 
                //    setters.SetProperty(b => b.CustomMessage, requestDTO.Content)
                //    .SetProperty(b=>b.ExpirationTime, requestDTO.TimeExpires)
                //    );
                dbContext.Set<Account>().Attach(acc);

                acc.CustomStatus = status;


                var res = await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> SetNicknameAsync(ulong senderId, ulong userId, SetNicknameUserRequestDTO requestDTO)
        {
            try
            {
                Account senderAccount = new()
                {
                    Id = senderId,
                    NicknamedAccounts = new List<AccountNickname>()
                };
                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                AccountNickname relation = new()
                {
                    SubjectId = userId,
                    AuthorId = senderId,
                    Nickname = requestDTO.Nickname,
                };

                dbContext.Set<Account>().Attach(senderAccount); //throws error if already noted fyi
                senderAccount.NicknamedAccounts.Add(relation);
                var res = await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> SetNoteAsync(ulong senderId, ulong userId, SetNoteUserRequestDTO requestDTO)
        {
            try
            {
                Account senderAccount = new()
                {
                    Id = senderId,
                    NotedAccounts = new List<AccountNote>()
                };
                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                AccountNote relation = new()
                {
                    SubjectId = userId,
                    AuthorId = senderId,
                    Note = requestDTO.Note,
                };

                dbContext.Set<Account>().Attach(senderAccount); //throws error if already noted fyi
                senderAccount.NotedAccounts.Add(relation);
                var res = await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> SetPhoneNumberAsync(ulong senderId, EditPhoneNumberRequestDTO requestDTO)
        {
            try
            {
                //User sender = new()
                //{
                //    Id = senderId,
                //};
                var senderAcc = await dbContext.Set<Account>().AsQueryable().Include(e => e.User).FirstOrDefaultAsync(x => x.Id == senderId);
                if (senderAcc != null)
                {
                    dbContext.Set<User>().Attach(senderAcc.User);
                    senderAcc.User.PhoneNumber = requestDTO.NewPhoneNumber;
                    var res = await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> SetStatusAsync(ulong senderId, SetStatusRequestDTO requestDTO)
        {
            try
            {
                Account senderAccount = new()
                {
                    Id = senderId,
                };
                //var senderAcc = await dbContext.Set<Account>().AsQueryable().Include(e => e.User).FirstOrDefaultAsync(x => x.Id == senderId);
                //if (senderAcc != null)
                //{
                dbContext.Set<Account>().Attach(senderAccount);
                senderAccount.ActivityStatusId = requestDTO.Id;
                var res = await dbContext.SaveChangesAsync();
                //}
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> SetUserVolumeAsync(ulong senderId, ulong userId, SetUserVolumeRequestDTO requestDTO)
        {
            throw new NotImplementedException(); //no idea right now where to set this user volume as entity seems to have disappeared?=???
        }

        public async Task<bool> UpdatePasswordAsync(ulong id, string password)
        {
            try
            {
                var result = await _pwdHandler.UpdatePassword(password, id);
                return result is not null;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<bool> ForgotPasswordAsync(string email, string username)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SendFriendRequestAsync(ulong senderId, ulong receiverId)
        {
            try
            {
                //Account receiverAcc = await dbContext.Set<Account>().AsQueryable().FirstOrDefaultAsync(e => e.Id == receiverId);
                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                IncomingFriendRequest request = new()
                {
                    ReceiverId = receiverId,
                    SenderRequest = new()
                    {
                        SenderId = senderId
                    }
                };
                dbContext.Set<IncomingFriendRequest>().Add(request); //throws error if already blocked fyi
                var res = await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateUserConnectionAsync(ulong senderId, ulong connectionId, UpdateUserConnectionRequestDTO requestDTO)
        {
            try
            {
                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                AccountConnection connection = new()
                {
                    Id=connectionId,
                    
                };

                dbContext.Set<AccountConnection>().Attach(connection);
                connection.DisplayOnProfile = requestDTO.DisplayOnProfile;
                var res = await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
       
        public async Task<UserFullDTO> LoadUserSessionDataAsync(ulong senderId) //perhaps split this into multiple sections that get called upon loading application
        {
            var senderAcc = dbContext.Set<Account>()//.AsQueryable() //probably make this queryable and make the joins reused query appends or make compiled query
                
                //chat
                .Include(x => x.ChatMessageTrackers) //only users own trackers
                .Include(x => x.Chats).ThenInclude(e=>e.Subject).ThenInclude(e=>e.Messages).ThenInclude(e=>e.Attachments) //auto includes parent and children
                .Include(x => x.Chats).ThenInclude(e=>e.Subject).ThenInclude(e=>e.Participants).ThenInclude(e=>e.Participant).ThenInclude(e=>e.Profile).ThenInclude(e=>e.Account).ThenInclude(e=>e.ActivityStatus)
                .Include(x => x.Chats).ThenInclude(e=>e.Subject).ThenInclude(e=>e.Invites)
                .Include(x => x.Chats).ThenInclude(e=>e.Subject).ThenInclude(e=>e.Pinboard).ThenInclude(e=>e.PinnedMessages) //auto connects messages

                //self
                //.Include(x => x.ActivityStatus) //probably not needed
                .Include(x => x.Connections).ThenInclude(e=>e.Connection)
                .Include(x => x.CustomStatus)
                .Include(x => x.Sessions)
                 .Include(x => x.User).ThenInclude(e => e.SecurityCredentials)
                .Include(x => x.Violations).ThenInclude(e=>e.Appeal) //users violations and if they appealed
                //.Include(x => x.Profile) //probably dont need this cause this account gets included from chat or server

                .Include(x => x.Roles).ThenInclude(e=>e.Permissions)
                .Include(x => x.Friendships).ThenInclude(e=>e.Participants)

                .Include(x => x.IncomingFriendRequests).ThenInclude(e=>e.SenderRequest).ThenInclude(e=>e.Sender).ThenInclude(e=>e.Profile) //dont  actually know if i need to include profile or just sender handle name
                .Include(x => x.IncomingFriendRequests).ThenInclude(e=>e.SenderRequest).ThenInclude(e=>e.Sender).ThenInclude(e=>e.ActivityStatus) //dont  actually know if i need to include profile or just sender handle name
                .Include(x => x.OutgoingFriendRequests).ThenInclude(e => e.ReceiverRequest).ThenInclude(e => e.Receiver).ThenInclude(e => e.Profile)
                .Include(x => x.OutgoingFriendRequests).ThenInclude(e => e.ReceiverRequest).ThenInclude(e => e.Receiver).ThenInclude(e => e.ActivityStatus)
               
                //dont need all this report stuff
                //.Include(x => x.IssuedViolations).ThenInclude(e=>e.Appeal)
                //.Include(x => x.IssuedViolations).ThenInclude(e=>e.ConsumedCustomStatusReports)
                //.Include(x => x.IssuedViolations).ThenInclude(e=>e.ConsumedMessageReports)
                //.Include(x => x.IssuedViolations).ThenInclude(e=>e.ConsumedProfileReports)
                //.Include(x => x.CustomStatusReports)
                //.Include(x => x.MessageReports)
                //.Include(x => x.ReportedCustomStatuses)
                //.Include(x => x.ReportedMessages)
                //.Include(x => x.ReportedProfiles)
                //.Include(x => x.ProfileReports)
                //.Include(x => x.ReviewedAppeals)

                //dont need to include relations subject / profile if i am not part of same chat/server as them unless specific cases like blocked or suggestion
                .Include(x => x.FriendSuggestions).ThenInclude(e=>e.Suggestion).ThenInclude(e=>e.Profile)
                .Include(x => x.FriendSuggestions).ThenInclude(e=>e.Suggestion).ThenInclude(e=>e.ActivityStatus)
                .Include(x => x.BlockedAccounts).ThenInclude(e=>e.Blocked).ThenInclude(e=>e.Profile) //dont know about profile probably just need accountname handle
                .Include(x => x.MutedChats)
                .Include(x => x.MutedServers)
                .Include(x => x.MutedSoundboards)
                .Include(x => x.MutedTextChannels)
                .Include(x => x.MutedVoiceChannels)
                .Include(x => x.MutedVoices)
                .Include(x => x.NicknamedAccounts)
                .Include(x => x.NotedAccounts)
                .Include(x => x.Folders) //dont need to include server cause servers get included from serverprofile

                .Include(x => x.Settings).ThenInclude(e => e.AccessibilitySettings)
                .Include(x => x.Settings).ThenInclude(e => e.ActivitySettings)
                .Include(x => x.Settings).ThenInclude(e => e.AdvancedSettings)
                .Include(x => x.Settings).ThenInclude(e => e.AppearanceSettings).ThenInclude(e => e.Theme)
                .Include(x => x.Settings).ThenInclude(e => e.BillingInformation).ThenInclude(e => e.PaymentMethods).ThenInclude(e => e.Type)
                .Include(x => x.Settings).ThenInclude(e => e.BillingInformation).ThenInclude(e => e.Subscriptions)
                .Include(x => x.Settings).ThenInclude(e => e.ChatSettings)
                .Include(x => x.Settings).ThenInclude(e => e.FriendRequestSettings)
                .Include(x => x.Settings).ThenInclude(e => e.GameOverlaySettings)
                .Include(x => x.Settings).ThenInclude(e => e.KeybindSettings).ThenInclude(e => e.Keybinds).ThenInclude(e => e.ApplicationKeybind)
                .Include(x => x.Settings).ThenInclude(e => e.Language)
                .Include(x => x.Settings).ThenInclude(e => e.NotificationSettings)
                .Include(x => x.Settings).ThenInclude(e => e.PrivacySettings)
                .Include(x => x.Settings).ThenInclude(e => e.SoundboardSettings)
                .Include(x => x.Settings).ThenInclude(e => e.StreamerModeSettings)
                .Include(x => x.Settings).ThenInclude(e => e.VideoSettings)
                .Include(x => x.Settings).ThenInclude(e => e.VoiceSettings)
                .Include(x => x.Settings).ThenInclude(e => e.WindowSettings)

                //server
                .Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.AuditLogs).ThenInclude(e=>e.Account)
                .Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.BanList).ThenInclude(e=>e.Account)
                .Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.ChannelCategories).ThenInclude(e=>e.RolePermissions)
                .Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.ChannelCategoryMemberPermissions)
                .Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.ChannelCategoryMemberSettings)
                .Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.Emotes).ThenInclude(e=>e.Uploader)
                .Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.Events).ThenInclude(e=>e.Creator)
                .Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.Invites).ThenInclude(e=>e.Inviter)
                .Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.Members).ThenInclude(e=>e.Account).ThenInclude(e=>e.Profile)
                .Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.Members).ThenInclude(e=>e.Account).ThenInclude(e=>e.ActivityStatus)
                .Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.Roles).ThenInclude(e=>e.Permissions).ThenInclude(e=>e.Permission)
                .Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.Roles).ThenInclude(e=>e.Recipients)
                //.Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.Roles).ThenInclude(e=>e.ChannelCategoryRolePermissions)
                //.Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.Roles).ThenInclude(e=>e.TextChannelRolePermissions)
                //.Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.Roles).ThenInclude(e=>e.VoiceChannelRolePermissions)
                .Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.Roles).ThenInclude(e=>e.ChannelCategoryRoles).ThenInclude(e=>e.Permissions)
                .Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.Roles).ThenInclude(e=>e.TextChannelRoles).ThenInclude(e => e.Permissions)
                .Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.Roles).ThenInclude(e=>e.VoiceChannelRoles).ThenInclude(e => e.Permissions)
                .Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.Settings).ThenInclude(e=>e.Region)
                .Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.SoundboardSounds).ThenInclude(e=>e.Uploader).ThenInclude(e=>e.Profile)
                //txtchannel
                .Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.TextChannels).ThenInclude(e=>e.Messages).ThenInclude(e=>e.Author)
                .Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.TextChannels).ThenInclude(e=>e.Pinboard).ThenInclude(e=>e.PinnedMessages)
                .Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.TextChannelMemberPermissions)
                .Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.TextChannelMemberSettings)
                .Include(x => x.TextChannelMessageTrackers) //users own textchannelmessagetrackers
                //voicechannel
                .Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.VoiceChannels).ThenInclude(e=>e.VoiceInvites)
                .Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.VoiceChannels).ThenInclude(e=>e.Region)
                .Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.VoiceChannelMemberPermissions)
                .Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.VoiceChannelMemberSettings)

                .AsSplitQuery().FirstOrDefaultAsync(e=>e.Id == senderId);

            return mapper.Map<UserFullDTO>(senderAcc);
        }
    }
}
