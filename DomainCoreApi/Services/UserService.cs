using AutoMapper;
using Azure.Core;
using CoreLib.DTO.EchoCore.ServerCore;
using CoreLib.DTO.EchoCore.UserCore;
using CoreLib.DTO.RequestCore.FriendCore;
using CoreLib.DTO.RequestCore.MessageCore;
using CoreLib.DTO.RequestCore.RelationCore;
using CoreLib.DTO.RequestCore.UserCore;
using CoreLib.Entities.EchoCore;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ApplicationCore;
using CoreLib.Entities.EchoCore.ApplicationCore.Settings;
using CoreLib.Entities.EchoCore.ApplicationCore.SettingsCore;
using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Entities.EchoCore.FriendCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Entities.Enums;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Repositorys;
using CoreLib.Interfaces.Services;
using CoreLib.MapperProfiles.MapperProfileConverters;
using DomainCoreApi.EFCORE;
using DomainCoreApi.EFCORE.Configurations.FriendCore;
using DomainCoreApi.Handlers;
using DomainCoreApi.Services.Bases;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DomainCoreApi.Services
{
    public class UserService 
        : /*BaseEntityService<User, ulong>, */
        IUserService
    {
        private readonly EchoDbContext dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHandler _pwdHandler;
        private readonly Handlers.TokenHandler _tokenHandler;
        //private readonly IPushNotificationService _notificationService;
        private readonly CreateUserHandler _createUserHandler = new();
        public UserService(EchoDbContext dbContext, 
            IMapper mapper,
            //IPushNotificationService notificationService,
            IPasswordHandler pwdHandler, 
            Handlers.TokenHandler tokenHandler) 
            //: base(repository)
        {
            this.dbContext = dbContext;
            this._mapper = mapper;
            this._pwdHandler = pwdHandler;
            this._tokenHandler = tokenHandler;
            //this._notificationService = notificationService;
        }

        public async Task<bool> AcceptFriendRequestAsync(ulong senderId, ulong requestId)
        {
            try
            {

                var request = await dbContext.Set<IncomingFriendRequest>()
                    .AsQueryable()
                    .Include(e => e.SenderRequest)
                    .FirstOrDefaultAsync(e => e.Id == requestId);
                //verify acceptingacc is part of inc request.
                if (request == null || request.ReceiverId != senderId)
                {
                    return false;
                }
                var participancies = await dbContext.Set<FriendshipParticipancy>()
                    .AsQueryable()
                    //filter rows by participantid part of key.
                    .Where(e => e.ParticipantId == senderId || e.ParticipantId == request.SenderRequest.SenderId) 
                    .GroupBy(r => r.SubjectId) // find rows where friendship is same id and group them
                 .ToListAsync();
                var existingFriendship = participancies.Where(x => 
                x.Count() > 1) //check if more than 1 in group meaning that two different rows have been matched on subjectid
                .SelectMany(g => g); //flatten result into list
                if (existingFriendship.Any()) //list will be empty if no matches
                {
                    return false;
                }

                Friendship friendship = new()
                {
                    Participants = new List<FriendshipParticipancy>()
                        {
                            new FriendshipParticipancy()
                            {
                                    ParticipantId = request.SenderRequest.SenderId,
                            },
                            new FriendshipParticipancy()
                            {
                                    ParticipantId = senderId,
                            }
                        },
                };

                dbContext.Set<OutgoingFriendRequest>().Remove(request.SenderRequest);
                dbContext.Set<IncomingFriendRequest>().Remove(request);
                await dbContext.Set<Friendship>().AddAsync(friendship);
                var res = await dbContext.SaveChangesAsync();
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
                if (requestDTO.TypeId == 0)
                {
                    return false;
                }

                var senderAcc = await dbContext.Set<Account>()
                    .Include(e => e.Connections)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == senderId);
                if (senderAcc == null)
                {
                    return false;
                }
                var type = await dbContext.Set<ConnectionType>().FirstOrDefaultAsync(e=>e.Id == requestDTO.TypeId);
                if (type==null)
                {
                    return false;
                }
                senderAcc.Connections ??= new List<AccountConnection>();

                AccountConnection connection = new()
                {
                    Name = requestDTO.Name,
                    Token = requestDTO.Token,
                    DisplayOnProfile = requestDTO.DisplayOnProfile,
                    ConnectionId = requestDTO.TypeId,
                };

                senderAcc.Connections.Add(connection);
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
                if (senderId == userId)
                {
                    return false;
                }
                var accs = await dbContext.Set<Account>()
                    .Include(e=>e.Friendships)
                    .Include(e=>e.IncomingFriendRequests.Where(e=>e.ReceiverId==senderId||e.ReceiverId==userId)).ThenInclude(e=>e.SenderRequest) // find incoming requests from sender side
                    .Include(e=>e.OutgoingFriendRequests.Take(0))
                    .Where(e => e.Id == senderId || e.Id == userId)
                    .AsSplitQuery()
                    .ToListAsync();
                var relatedFriendship = dbContext.Set<FriendshipParticipancy>().Local
                    .GroupBy(e => e.SubjectId)
                    .FirstOrDefault(e => e.Count() > 1);
                    //.Select(e=>e); // find matched friendparticipancy if any then flatten
                var user = accs?.FirstOrDefault(e => e.Id == userId);
                var sender = accs?.FirstOrDefault(e => e.Id == senderId);
                var shouldRemoveFriend = relatedFriendship != null && relatedFriendship.Any();
                if (shouldRemoveFriend)
                {
                    dbContext.Set<Friendship>().Remove(relatedFriendship.First().Subject); //blocking someone will remove them from friendslist
                }
                var shouldRemoveRequests = user.OutgoingFriendRequests.Any() || sender.OutgoingFriendRequests.Any();
                if (shouldRemoveRequests)
                {
                    var request = new List<OutgoingFriendRequest>(user.OutgoingFriendRequests).Concat(sender.OutgoingFriendRequests); // expensive way to result in one request xd
                    //blocking someone will remove friendsrequests as it is impossible to request or receive requests of friendship from blocked users
                    //afaik you can only have one request relation between accounts present at once so just removing first is fine. // we dont know which side sent request (*facepalm*)
                    var relatedRequest = request.FirstOrDefault(); //only outgoing requests linked to the sender acc will be present here
                    dbContext.Set<OutgoingFriendRequest>().Remove(relatedRequest);
                    dbContext.Set<IncomingFriendRequest>().Remove(relatedRequest.ReceiverRequest); 
                }
                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                AccountBlock blockrelation = new()
                {
                    BlockedId = userId,
                    BlockerId = senderId,
                };

                await dbContext.Set<AccountBlock>().AddAsync(blockrelation); //throws error if already blocked fyi
                
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
                var request = await dbContext.Set<OutgoingFriendRequest>().Include(e => e.ReceiverRequest).AsQueryable().FirstOrDefaultAsync(e => e.Id == requestId);
                if (request == null || request.SenderId != senderId)
                {
                    return false;
                }
                dbContext.Set<OutgoingFriendRequest>().Remove(request);
                dbContext.Set<IncomingFriendRequest>().Remove(request.ReceiverRequest);
                var res = await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        private string GetIconFromList()
        {
            List<string> list = new List<string>();
            Random rnd = new Random();
            list.Add("https://imgur.com/qp6Jjpj.png");
            list.Add("https://imgur.com/4eWTjzg.png");
            list.Add("https://imgur.com/thNssAf.png");
            list.Add("https://imgur.com/qMgDaU5.png");
            list.Add("https://imgur.com/Q7pl0t3.png");
            list.Add("https://imgur.com/IkANdGw.png");
            list.Add("https://imgur.com/vmWZgGu.png");
            int r = rnd.Next(list.Count);
            return ((string)list[r]);
        }

        private Account GetNewDefaultAccount(string name, string email, DateTime dateOfBirth, string displayName, bool allowMails)
        {
            return new Account()
            {
                Name = name,
                TimeCreated = DateTime.UtcNow,
                //CustomStatus = new() //should not contain at first
                //{
                //    CustomMessage = "Online"
                //},
                ActivityStatusId = 1,
                Roles = new List<AccountRole>() { },
                User = new()
                {
                    Email = email,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = false,
                    PasswordSetDate = DateTime.UtcNow,
                    DateOfBirth = dateOfBirth,
                    //SecurityCredentials = userPwd
                },
                Profile = new()
                {
                    DisplayName = !String.IsNullOrEmpty(displayName) ? displayName : name,
                    AvatarFileURL = GetIconFromList(),
                    BannerColor = "#5C23D9",
                },
                Settings = new()
                {
                    LanguageId = 10,
                    AccessibilitySettings = new()
                    {
                        SaturationPercent = 255,
                        ApplySaturationToCustomColors = false,
                        AlwaysUnderlineLinks = true,
                        SyncProfileThemes = true,
                        SyncContrastSettings = true,
                        RoleColorMode = RoleColorMode.ShowRoleColorsInNames,
                        SyncReducedMotionWithPC = true,
                        ReducedMotion = true,
                        AutoPlayGIFsOnAppFocus = true,
                        PlayAnimatedEmojis = true,
                        StickerAnimationMode = StickerAnimationMode.AlwaysAnimate,
                        ShowSendMessageButton = true,
                        AllowTextToSpeech = false,
                        TextToSpeechRate = 255,
                    },
                    AppearanceSettings = new()
                    {
                        ThemeId = 1,
                        InAppIcon = "",
                        DarkSideBar = true,
                        PixelChatFontScale = 255,
                        PixelSpaceBetweenMessageGroupsScale = 255,
                    },
                    AdvancedSettings = new()
                    {
                        DeveloperMode = false,
                        AutoNavigateServerHome = false,
                    },
                    BillingInformation = new(),
                    ChatSettings = new(),
                    FriendRequestSettings = new()
                    {
                        Everyone = true,
                        FriendsOfFriends = false,
                        ServerMembers = false,
                    },
                    KeybindSettings = new(),
                    NotificationSettings = new()
                    {
                        FocusModeEnabled = false,
                        EnableDesktopNotifications = true,
                        EnableUnreadMessageBadge = true,
                        EnableTaskbarFlashing = true,
                        ReceiveCommunicationEmails = allowMails,
                        ReceiveRecommendationEmails = allowMails,
                        ReceiveSocialEmails = allowMails,
                        ReceiveTipEmails = allowMails,
                        ReceiveAnnouncementAndUpdateEmails = allowMails,
                    },
                    PrivacySettings = new()
                    {
                        DMFromFriends = DMAllow.Show,
                        DMFromUnknownUsers = DMAllow.Show,
                        DMFromServerChatroom = DMAllow.Show,
                    },
                    SoundboardSettings = new()
                    {
                        SoundboardVolume = 100,
                        //Soundboard = 100,
                    },
                    StreamerModeSettings = new()
                    {
                        EnableStreamerMode = false,
                        HidePersonalInformation = true,
                        HideInviteLinks = false,
                        DisableNotifications = false,
                        DisableSounds = false,
                    },
                    VoiceSettings = new()
                    {
                        InputDevice = "",
                        OutputDevice = "",
                        InputVolume = 100,
                        OutputVolume = 100,
                        InputMode = InputMode.VoiceActivity,
                        EchoCancellation = true,
                        NoiseSuppression = NoiseSuppression.Standard,
                        AdvancedVoiceActivity = false,
                        AutomaticGainControl = false,
                    },
                    VideoSettings = new()
                    {
                        AlwaysPreviewVideo = false,
                        CameraDevice = "",
                        VideoBackground = "",
                        UseOpenH264VideoCodec = false,
                        EnableHardwareAccelerationForVideo = false,
                        EnableForceQualityOfServicePacketPrio = false,
                        UseDDLInjectionToCaptureScreen = false
                    },
                    ActivitySettings = new()
                    {
                        DisplayCurrentActivityAsAStatusMessage = true,
                        ShareActivityStatusOnLargeServerJoin = true,
                        AllowFriendsToJoinGame = true,
                        AllowVoiceChannelParticipantsToJoinGame = true,
                    },
                    WindowSettings = new(),
                    GameOverlaySettings = new(),
                },
            };
        }

        public async Task<bool> RegisterAsync(RegisterRequestDTO input)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var userPwd = await _pwdHandler.CreatePassword(input.Password);
                Account account = GetNewDefaultAccount(
                    input.Username, 
                    input.Email, 
                    input.DateOfBirth, 
                    input.DisplayName, 
                    input.AllowEchoMails);
                account.User.SecurityCredentials = userPwd;
                await dbContext.Set<Account>().AddAsync(account);

                await dbContext.SaveChangesAsync();
                //dbContext.Attach<Account>(account);
                var role = new AccountRole() { RoleId = 1, AccountId=account.Id };
                await dbContext.Set<AccountRole>().AddAsync(role);
                //account.Roles.Add(role);
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                return false;
            }
            return true;
        }

        public async Task<bool> DeafenSelfAsync(ulong senderId)
        {
            try
            {
                Account acc = await dbContext.Set<Account>().AsQueryable()
                    .Include(e => e.Settings)
                    .ThenInclude(e => e.VoiceSettings)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == senderId);

                acc.Settings.VoiceSettings.DeafenSelf = true;

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
                if (request == null || request.ReceiverId != senderId)
                {
                    return false;
                }
                dbContext.Set<OutgoingFriendRequest>().Remove(request.SenderRequest);
                dbContext.Set<IncomingFriendRequest>().Remove(request); //dunno why but client cascade doesnt want to work even when object is loaded in context.
                var res = await dbContext.SaveChangesAsync();
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

        public async Task<TokenDTO> LoginAsync(LoginRequestDTO attempt)
        {
            try
            {
                var user = await dbContext.Set<User>()
                    .Include(e => e.SecurityCredentials)
                    .Include(e => e.Account).ThenInclude(e=>e.Sessions)
                    .FirstOrDefaultAsync(e => e.Email == attempt.Email);
                if (user is not null && await _pwdHandler.CheckPassword(attempt.Password, user.SecurityCredentials))
                {
                    //get access and refresh token back to caller.
                    var accessToken = _tokenHandler.GetAccessToken(user.Account);
                    var refreshToken = _tokenHandler.GetRefreshToken(user.Account);
                    AccountSession session = new()
                    {
                         AccountId = user.Account.Id,
                          ExpirationTime = null, //this is for echo to set expiration time if necessary
                           Location = "", // not yet supported
                           DeviceId = "", // not yet supported
                        TimeStarted = DateTime.Now,
                            TimeStopped = null,
                             AccessToken = accessToken,
                             RefreshToken = refreshToken,
                    };
                    await dbContext.Set<AccountSession>().AddAsync(session);
                    var tokens = new TokenDTO()
                    {
                         RefreshToken = refreshToken,
                         Token =  accessToken,
                    };
                    await dbContext.SaveChangesAsync();
                    return tokens;
                }
                throw new Exception("You suck at hacking bruv");
            }
            catch (Exception e)
            {
                //errors
                throw;
            }
        }

        public async Task<bool> MuteSelfAsync(ulong senderId)
        {
            try
            {
                Account acc = await dbContext.Set<Account>().AsQueryable()
                    .Include(e => e.Settings)
                    .ThenInclude(e=>e.VoiceSettings)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == senderId);
               
                acc.Settings.VoiceSettings.MuteSelf = true;
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
                if (senderId == userId)
                {
                    return false;
                }
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
                    ExpirationTime = requestDTO.TimeExpires,
                };

                //dbContext.Set<Account>().Attach(senderAccount); //throws error if already blocked fyi
                //senderAccount.MutedVoices.Add(muterelation);
                //var res = await dbContext.SaveChangesAsync();
                await dbContext.Set<AccountMute>().AddAsync(muterelation);

                await dbContext.SaveChangesAsync();

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
                if (senderId == friendId)
                {
                    return false;
                }
                var participancies = await dbContext.Set<FriendshipParticipancy>().Include(e => e.Participant).Include(e => e.Subject).AsQueryable().Where(e => e.ParticipantId == senderId || e.ParticipantId == friendId).ToListAsync();
                if (participancies==null)
                {
                    return false;
                }
                var removed = participancies.GroupBy(e => e.SubjectId).FirstOrDefault(e => e.Count() > 1)?.Select(e=>e).First().Subject;
                if (removed == null)
                {
                    return false;
                }
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
                Account acc = await dbContext.Set<Account>().AsQueryable().Include(e => e.Connections).FirstOrDefaultAsync(e => e.Id == senderId);
                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                var removed = acc.Connections.FirstOrDefault(e => e.Id == connectionId);
                if (removed == null)
                {
                    return false;
                }
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
                if (requestDTO.Name.IsNullOrEmpty()) //request validation
                {
                    return false;
                }
                Account receiverAcc = await dbContext.Set<Account>().AsQueryable().AsNoTracking().FirstOrDefaultAsync(e => e.Name == requestDTO.Name);
                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);

                if (senderId == receiverAcc.Id) //validate user is other than self
                {
                    return false;
                }

                var senderAcc = await dbContext.Set<Account>().AsQueryable()
                    .Include(e => e.Friendships).ThenInclude(e => e.Subject).ThenInclude(e => e.Participants.Where(e => e.ParticipantId != senderId))
                    .Include(e => e.OutgoingFriendRequests).ThenInclude(e => e.ReceiverRequest)
                    .Include(e => e.IncomingFriendRequests).ThenInclude(e => e.SenderRequest)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == senderId);


                if (senderAcc.OutgoingFriendRequests.Any(e => e.ReceiverRequest.ReceiverId == receiverAcc.Id) || senderAcc.Friendships.Any(e => e.Subject.Participants.Select(e => e.ParticipantId).Contains(receiverAcc.Id)))
                //check if already sent request or already friends
                {
                    return false;
                }

                var incomingFromReceiver = senderAcc.IncomingFriendRequests.FirstOrDefault(e => e.SenderRequest.SenderId == receiverAcc.Id);
                if (incomingFromReceiver != null) //if receiver already sent sender a request
                {
                    // if already received friendrequest and trying to send one then just accept incoming.
                    Friendship friendship = new()
                    {
                        Participants = new List<FriendshipParticipancy>()
                        {
                            new FriendshipParticipancy()
                            {
                                    ParticipantId = incomingFromReceiver.SenderRequest.SenderId,
                            },
                            new FriendshipParticipancy()
                            {
                                    ParticipantId = senderId,
                            }
                        },
                    };

                    dbContext.Set<OutgoingFriendRequest>().Remove(incomingFromReceiver.SenderRequest);
                    dbContext.Set<IncomingFriendRequest>().Remove(incomingFromReceiver); //cleanup cause appearently clientcascade doesnt work????
                    await dbContext.Set<Friendship>().AddAsync(friendship);
                    await dbContext.SaveChangesAsync();
                    return true;
                }

                //send request
                IncomingFriendRequest request = new()
                {
                    ReceiverId = receiverAcc.Id,
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

        public async Task<bool> SetCustomStatusAsync(ulong senderId, SetCustomStatusRequestDTO requestDTO) //check if this works??
        {
            try
            {
                var senderAcc = await dbContext.Set<Account>().Include(e => e.CustomStatus).AsSingleQuery().AsNoTracking().FirstOrDefaultAsync(e => e.Id == senderId);
                if (senderAcc == null)
                {
                    return false;
                }

                //efficient update
                //await dbContext.Set<AccountCustomStatus>().AsQueryable().Where(e=>e.Id==senderId)
                //    .ExecuteUpdateAsync(setters => 
                //    setters.SetProperty(b => b.CustomMessage, requestDTO.Content)
                //    .SetProperty(b=>b.ExpirationTime, requestDTO.TimeExpires)
                //    );

                var relation = new AccountCustomStatus()
                {
                    CustomMessage = requestDTO.Content,
                    ExpirationTime = requestDTO.TimeExpires,
                    Id = senderId,
                };

                var relationExists = senderAcc.CustomStatus != null;

                if (requestDTO.Content.IsNullOrEmpty() && !relationExists) //if request is null ignore by now
                {
                    return false;
                }

                if (requestDTO.Content.IsNullOrEmpty() && relationExists) //if status exists and request is null then remove it
                {
                    dbContext.Set<AccountCustomStatus>().Remove(relation);
                }

                if (!requestDTO.Content.IsNullOrEmpty() && relationExists) //if note is already loaded into context just update it.
                {
                    dbContext.Set<AccountCustomStatus>().Update(relation);
                }

                if (!requestDTO.Content.IsNullOrEmpty() && !relationExists) //add if not null and content not null
                {
                    await dbContext.Set<AccountCustomStatus>().AddAsync(relation);
                    //senderAcc.NotedAccounts.Add(relation); // cant use changetracker cause acc is not attached.
                }

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
                if (senderId == userId)
                {
                    return false;
                }

                var senderAcc = await dbContext.Set<Account>().Include(e => e.NicknamedAccounts.Where(e => e.SubjectId == userId)).AsNoTracking().FirstOrDefaultAsync(e => e.Id == senderId);
                if (senderAcc == null)
                {
                    return false;
                }

                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                AccountNickname relation = new()
                {
                    SubjectId = userId,
                    AuthorId = senderId,
                    Nickname = requestDTO.Nickname
                };

                var relationExists = senderAcc.NicknamedAccounts.Any();

                if (requestDTO.Nickname.IsNullOrEmpty() && !relationExists) //if request is null ignore by now
                {
                    return false;
                }

                if (requestDTO.Nickname.IsNullOrEmpty() && relationExists) //if status exists and request is null then remove it
                {
                    dbContext.Set<AccountNickname>().Remove(relation);
                }

                if (!requestDTO.Nickname.IsNullOrEmpty() && relationExists) //if note is already loaded into context just update it.
                {
                    dbContext.Set<AccountNickname>().Update(relation);
                }

                if (!requestDTO.Nickname.IsNullOrEmpty() && !relationExists) //add if not null and content not null
                {
                    await dbContext.Set<AccountNickname>().AddAsync(relation);
                    //senderAcc.NotedAccounts.Add(relation); // cant use changetracker cause acc is not attached.
                }

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
                if (senderId == userId)
                {
                    return false;
                }

                var senderAcc = await dbContext.Set<Account>().Include(e => e.NotedAccounts.Where(e => e.SubjectId == userId)).AsNoTracking().FirstOrDefaultAsync(e => e.Id == senderId);
                if (senderAcc == null)
                {
                    return false;
                }

                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                AccountNote relation = new()
                {
                    SubjectId = userId,
                    AuthorId = senderId,
                    Note = requestDTO.Note, 
                };

                var relationExists = senderAcc.NotedAccounts.Any();

                if (requestDTO.Note.IsNullOrEmpty() && !relationExists) //if request is null ignore by now
                {
                    return false;
                }

                if (requestDTO.Note.IsNullOrEmpty() && relationExists) //if status exists and request is null then remove it
                {
                    dbContext.Set<AccountNote>().Remove(relation);
                }

                if (!requestDTO.Note.IsNullOrEmpty() && relationExists) //if note is already loaded into context just update it.
                {
                    dbContext.Set<AccountNote>().Update(relation);
                }

                if (!requestDTO.Note.IsNullOrEmpty() && !relationExists) //add if not null and content not null
                {
                    await dbContext.Set<AccountNote>().AddAsync(relation);
                    //senderAcc.NotedAccounts.Add(relation); // cant use changetracker cause acc is not attached.
                }


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
                var phoneUtil = PhoneNumberUtil.GetInstance();
                //var numberProto = phoneUtil.Parse(requestDTO.NewPhoneNumber, "ZZ"); //why ZZ means international i don't know
                var numberProto = phoneUtil.Parse(requestDTO.NewPhoneNumber, ""); //probably just needs empty?
                //later on put number in validation table or cache with corresponding random code then send sms to number and have user verify number via code.
                //kind of like a normal reset password email flow or 2step verification.
                if (!phoneUtil.IsValidNumber(numberProto))
                {
                    return false;
                }
                var formattedPhone = phoneUtil.Format(numberProto, PhoneNumberFormat.INTERNATIONAL);
                var senderAcc = await dbContext.Set<Account>().AsQueryable().Include(e => e.User).FirstOrDefaultAsync(x => x.Id == senderId);
                if (senderAcc == null)
                {
                    return false;
                }
                senderAcc.User.PhoneNumber = formattedPhone;
                var res = await dbContext.SaveChangesAsync();
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
                if (senderId == 0)
                {
                    return false;
                }
                var senderAcc = await dbContext.Set<Account>()
                    .Include(e => e.ActivityStatus)
                    .Include(e => e.CustomStatus)
                    .FirstOrDefaultAsync(e => e.Id == senderId);
                if (senderAcc == null)
                {
                    return false;
                }
                var status = await dbContext.Set<AccountActivityStatus>()
                    .FirstOrDefaultAsync(e => e.Id == requestDTO.Id);
                if (status == null)
                {
                    return false;
                }
                
                senderAcc.ActivityStatusId = requestDTO.Id;
                var res = await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> SetDisplayNameAsync(ulong senderId, UserMinimalDTO user)
        {
            try
            {
                if (senderId == 0)
                {
                    return false;
                }
                Account acc = new()
                {
                    Id = senderId,
                };
                var sender = await dbContext.Set<Account>().AsQueryable().Include(e => e.Profile).FirstOrDefaultAsync(x => x.Id == senderId);
                if (sender != null)
                {
                    sender.Profile.DisplayName = user.DisplayName;
                    dbContext.Set<AccountProfile>().Attach(sender.Profile);
                    var res = await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<List<ActivityStatusDTO>> GetListOfStatusAsync()
        {
            try
            {
                List<ActivityStatusDTO> list = new();
                var result = await dbContext.Set<AccountActivityStatus>().ToListAsync();
                foreach (var r in result)
                {
                    list.Add(
                        new ActivityStatusDTO{
                            Id = r.Id,
                            Name = r.Name,
                            Description = r.Description,
                            Icon = r.Icon,
                            IconColor = r.IconColor,
                        });
                }
                return list;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> SetUserVolumeAsync(ulong senderId, ulong userId, SetUserVolumeRequestDTO requestDTO)
        {
            try
            {
                if (senderId == userId)
                {
                    return false;
                }

                var senderAcc = await dbContext.Set<Account>().Include(e => e.PersonalAccountVolumes.Where(e => e.SubjectId == userId)).FirstOrDefaultAsync(e => e.Id == senderId);
                if (senderAcc == null)
                {
                    return false;
                }

                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                AccountAccountVolume relation = new()
                {
                    OwnerId = senderId,
                    SubjectId = userId,
                    Volume = requestDTO.Volume,
                };

                int defaultVolume = 100;


                var relationExists = senderAcc.PersonalAccountVolumes.Any();

                if (requestDTO.Volume == defaultVolume && !relationExists) //if request is null ignore by now
                {
                    return false;
                }

                if (requestDTO.Volume == defaultVolume && relationExists) //if status exists and request is null then remove it
                {
                    dbContext.Set<AccountAccountVolume>().Remove(relation);
                }

                if (requestDTO.Volume != defaultVolume && !relationExists) //add if not null and content not null
                {
                    senderAcc.PersonalAccountVolumes.Add(relation);
                }


                var res = await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdatePasswordAsync(ulong id, string password)
        {
            try
            {
                //perhaps in the future change security credentials to link to user through primary key since it wont be changed and we wont enforce "not previous password" policy
                var acc = await dbContext.Set<Account>().Include(e=>e.User).ThenInclude(e=>e.SecurityCredentials).FirstOrDefaultAsync(e=>e.Id == id);
                var userCred = await _pwdHandler.CreatePassword(password);
                acc.User.SecurityCredentials.PasswordHash = userCred.PasswordHash;
                acc.User.SecurityCredentials.Salt = userCred.Salt;
                //dbContext.Set<SecurityCredentials>().Update(userCred);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {

                return false;
            }
            return true;
        }

        public async Task<bool> ForgotPasswordAsync(string email, string username)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SendFriendRequestAsync(ulong senderId, ulong receiverId)
        {
            try
            {
                if (senderId == receiverId)
                {
                    return false;
                }

                var acc = await dbContext.Set<Account>().AsQueryable()
                    .Include(e => e.Friendships).ThenInclude(e => e.Subject).ThenInclude(e => e.Participants.Where(e => e.ParticipantId != senderId))
                    .Include(e => e.OutgoingFriendRequests).ThenInclude(e => e.ReceiverRequest)
                    .Include(e => e.IncomingFriendRequests).ThenInclude(e => e.SenderRequest)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == senderId);

                //var requests = await dbContext.Set<OutgoingFriendRequest>().AsQueryable().Include(e => e.ReceiverRequest).Where(e => e.SenderId == senderId).AsNoTracking().ToListAsync();

                if (acc.OutgoingFriendRequests.Any(e => e.ReceiverRequest.ReceiverId == receiverId) || acc.Friendships.Any(e => e.Subject.Participants.Select(e => e.ParticipantId).Contains(receiverId)))
                //check if already sent request or already friends
                {
                    return false;
                }

                var incomingFromReceiver = acc.IncomingFriendRequests.FirstOrDefault(e => e.SenderRequest.SenderId == receiverId);
                if (incomingFromReceiver != null)
                {
                    // if already received friendrequest and trying to send one then just accept incoming.
                    Friendship friendship = new()
                    {
                        Participants = new List<FriendshipParticipancy>()
                        {
                            new FriendshipParticipancy()
                            {
                                    ParticipantId = incomingFromReceiver.SenderRequest.SenderId,
                            },
                            new FriendshipParticipancy()
                            {
                                    ParticipantId = senderId,
                            }
                        },
                    };

                    dbContext.Set<OutgoingFriendRequest>().Remove(incomingFromReceiver.SenderRequest);
                    dbContext.Set<IncomingFriendRequest>().Remove(incomingFromReceiver); //cleanup cause appearently clientcascade doesnt work????
                    await dbContext.Set<Friendship>().AddAsync(friendship);
                    await dbContext.SaveChangesAsync();
                    return true;
                }

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
                var acc = await dbContext.Set<Account>().Include(e => e.Connections).AsNoTracking().FirstOrDefaultAsync(e => e.Id == senderId);
                if (acc == null)
                {
                    return false;
                }
                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                var connection = acc.Connections.FirstOrDefault(e => e.Id == connectionId);
                if (connection == null)
                {
                    return false;
                }
                dbContext.Set<AccountConnection>().Attach(connection); //attach cause no tracking query
                connection.DisplayOnProfile = requestDTO.DisplayOnProfile;
                var res = await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        private async Task<List<Chat>> LoadChatsFromIdsAsync(List<ulong> chatIdsToLoad)
        {
            var chats = await dbContext.Set<Chat>()
                         //chat
                         .Include(e => e.Messages).ThenInclude(e => e.Attachments) //auto includes parent and children
                         .Include(e => e.Participants).ThenInclude(e => e.Participant).ThenInclude(e => e.Profile)
                         .Include(e => e.Participants).ThenInclude(e => e.Participant).ThenInclude(e => e.ActivityStatus)
                         .Include(e => e.Participants).ThenInclude(e => e.Participant).ThenInclude(e => e.CustomStatus)
                         .Include(e => e.Participants).ThenInclude(e => e.Participant).ThenInclude(e => e.Friendships).ThenInclude(e => e.Subject).ThenInclude(e => e.Participants)
                         .Include(e => e.Invites).ThenInclude(e => e.Inviter)
                         .Include(e => e.PinnedMessages)//.ThenInclude(e => e.PinnedMessages) //auto connects messages
                         .Where(e => chatIdsToLoad.Contains(e.Id))
                         //.AsNoTrackingWithIdentityResolution()
                         .AsSplitQuery()
                         .ToListAsync();
            return chats;
        }

        private async Task<List<Server>> LoadServersFromIdsAsync(List<ulong> serverIdsToLoad)
        {
            var servers = await dbContext.Set<Server>()
                    //server
                    .Include(e => e.AuditLogs).ThenInclude(e => e.Account)
                    .Include(e => e.BanList).ThenInclude(e => e.Account)
                    .Include(e => e.ChannelCategories).ThenInclude(e => e.RolePermissions)
                    .Include(e => e.ChannelCategoryMemberPermissions!)
                    .Include(e => e.ChannelCategoryMemberSettings!)
                    .Include(e => e.Emotes).ThenInclude(e => e.Uploader)
                    .Include(e => e.Events).ThenInclude(e => e.Creator)
                    .Include(e => e.Invites).ThenInclude(e => e.Inviter)
                    .Include(e => e.Members).ThenInclude(e => e.Account).ThenInclude(e => e.Profile)
                    .Include(e => e.Members).ThenInclude(e => e.Account).ThenInclude(e => e.ActivityStatus)
                    .Include(e => e.Members).ThenInclude(e => e.Account).ThenInclude(e => e.CustomStatus)
                    .Include(e => e.Members).ThenInclude(e => e.Account).ThenInclude(e => e.Friendships).ThenInclude(e => e.Participant)
                    .Include(e => e.Roles).ThenInclude(e => e.Permissions).ThenInclude(e => e.Permission)
                    .Include(e => e.Roles).ThenInclude(e => e.Recipients)
                    //.Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.Roles).ThenInclude(e=>e.ChannelCategoryRolePermissions)
                    //.Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.Roles).ThenInclude(e=>e.TextChannelRolePermissions)
                    //.Include(x => x.Servers).ThenInclude(e=>e.Server).ThenInclude(e=>e.Roles).ThenInclude(e=>e.VoiceChannelRolePermissions)
                    .Include(e => e.Roles).ThenInclude(e => e.ChannelCategoryRoles).ThenInclude(e => e.Permissions)
                    .Include(e => e.Roles).ThenInclude(e => e.TextChannelRoles).ThenInclude(e => e.Permissions)
                    .Include(e => e.Roles).ThenInclude(e => e.VoiceChannelRoles).ThenInclude(e => e.Permissions)
                    .Include(e => e.Settings).ThenInclude(e => e.Region)
                    .Include(e => e.SoundboardSounds).ThenInclude(e => e.Uploader).ThenInclude(e => e.Profile)
                    //txtchannel
                    .Include(e => e.TextChannels).ThenInclude(e => e.Messages).ThenInclude(e => e.Author)
                    .Include(e => e.TextChannels).ThenInclude(e => e.PinnedMessages)
                    .Include(e => e.TextChannelMemberPermissions)
                    .Include(e => e.TextChannelMemberSettings)
                    //voicechannel
                    .Include(e => e.VoiceChannels).ThenInclude(e => e.Invites)
                    .Include(e => e.VoiceChannels).ThenInclude(e => e.Region)
                    .Include(e => e.VoiceChannelMemberPermissions)
                    .Include(e => e.VoiceChannelMemberSettings)
                     .Where(e => serverIdsToLoad.Contains(e.Id))
                    //.AsNoTrackingWithIdentityResolution()
                    .AsSplitQuery().ToListAsync();
            return servers;
        }

        public async Task<UserFullDTO> LoadUserSessionDataAsync(ulong senderId) //perhaps split this into multiple sections that get called upon loading application
        {
            try
            {
                //load account first, then load chats and servers
                var senderAcc = await dbContext.Set<Account>()//.AsQueryable() //probably make this queryable and make the joins reused query appends or make compiled query
                .Include(x => x.TextChannelMessageTrackers) //users own textchannelmessagetrackers
                .Include(x => x.ChatMessageTrackers) //only users own trackers

                //self
                .Include(x => x.ActivityStatus) //probably not needed
                .Include(x => x.Connections).ThenInclude(e => e.ConnectionType)
                .Include(x => x.CustomStatus)
                .Include(x => x.Sessions)
                .Include(x => x.Servers)
                .Include(x => x.Chats)
                 .Include(x => x.User).ThenInclude(e => e.SecurityCredentials)
                .Include(x => x.Violations).ThenInclude(e => e.Appeal) //users violations and if they appealed
                .Include(x => x.Profile) //probably dont need this cause this account gets included from chat or server

                .Include(x => x.Roles).ThenInclude(e => e.Role).ThenInclude(e => e.Permissions)
                .Include(x => x.Friendships).ThenInclude(e => e.Subject).ThenInclude(e => e.Participants).ThenInclude(e => e.Participant).ThenInclude(e => e.ActivityStatus)
                .Include(x => x.Friendships).ThenInclude(e => e.Subject).ThenInclude(e => e.Participants).ThenInclude(e => e.Participant).ThenInclude(e => e.CustomStatus)
                .Include(x => x.Friendships).ThenInclude(e => e.Subject).ThenInclude(e => e.Participants).ThenInclude(e => e.Participant).ThenInclude(e => e.Profile)

                .Include(x => x.IncomingFriendRequests).ThenInclude(e => e.SenderRequest).ThenInclude(e => e.Sender).ThenInclude(e => e.Profile) //dont  actually know if i need to include profile or just sender handle name
                .Include(x => x.IncomingFriendRequests).ThenInclude(e => e.SenderRequest).ThenInclude(e => e.Sender).ThenInclude(e => e.ActivityStatus) //dont  actually know if i need to include profile or just sender handle name
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
                .Include(x => x.FriendSuggestions).ThenInclude(e => e.Suggestion).ThenInclude(e => e.Profile)
                .Include(x => x.FriendSuggestions).ThenInclude(e => e.Suggestion).ThenInclude(e => e.ActivityStatus)
                .Include(x => x.BlockedAccounts).ThenInclude(e => e.Blocked).ThenInclude(e => e.Profile) //dont know about profile probably just need accountname handle
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
                .AsSplitQuery()
                //.AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(e => e.Id == senderId);
                if (senderAcc == null)
                {
                    return null;
                }
                var chatsToLoad = new List<ulong>(senderAcc.Chats.Select(e => e.SubjectId));
                //var friendshipsToLoad = new List<ulong>(senderAcc.Chats.Select(e => e.SubjectId)); //not needed
                var serversToLoad = new List<ulong>(senderAcc.Servers.Select(e => e.ServerId));
                List<Chat> senderChats = null;
                List<Server> senderServers = null;
                if (chatsToLoad.Count > 0)
                {
                    senderChats = await LoadChatsFromIdsAsync(chatsToLoad); //actually doesnt need to assign chats cause dbcontext handles lifetime scope

                }
                if (serversToLoad.Count > 0)
                {
                    senderServers = await LoadServersFromIdsAsync(serversToLoad);
                }

                return GetUserFullDTO(senderAcc, true, true, true);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        /// <summary>
        /// attempts to find friendship participancies where one side of the friendship contains an account that the <paramref name="accountWithContext"/> is also friends with.
        /// to make use of this function you must have loaded the friendships, participants and external accounts into the current dbset local of the caller context.
        /// <see href="https://learn.microsoft.com/en-us/ef/core/querying/related-data/eager"/>
        /// </summary>
        /// <param name="accountWithContext"></param>
        /// <returns>lookup list containing the external accounts participancies link to a mutual friend.</returns>
        private List<IGrouping<ulong, FriendshipParticipancy>> GetMutualFriendLookup(Account accountWithContext)
        {
            //mutual friendship calculation.
            ////get lists to crosscheck with
            var senderFriendIds = accountWithContext.Friendships.SelectMany(e => e.Subject.Participants.Where(e => e.ParticipantId != accountWithContext.Id)).Select(e => e.ParticipantId);
            var senderFriendshipIds = accountWithContext.Friendships.Select(e => e.SubjectId);

            ////get friendshipparticipancies in context that contain senderfriends
            var FriendshipsWithSenderFriends = dbContext.Set<FriendshipParticipancy>().Local.Where(fp => senderFriendIds.Contains(fp.ParticipantId));

            //filter away friendships where sender is in from local list.
            FriendshipsWithSenderFriends = FriendshipsWithSenderFriends.Where(fp => !senderFriendshipIds.Contains(fp.SubjectId));

            //now we must group the friendparticipancies by participants that are not friends to make a lookup list
            var groupedFriendships = FriendshipsWithSenderFriends.GroupBy(e => e.ParticipantId).ToList();
            return groupedFriendships;
        }
        /// <summary>
        /// attempts to find server profiles which is shared with the <paramref name="accountWithContext"/>. 
        /// to make use of this function you must have loaded the servers and server profiles into the current dbset local of the caller context.
        /// <see href="https://learn.microsoft.com/en-us/ef/core/querying/related-data/eager"/>
        /// </summary>
        /// <param name="accountWithContext"></param>
        /// <returns>lookup list containing the external accounts server profiles.</returns>
        private List<IGrouping<ulong, ServerProfile>> GetMutualServerServerProfileLookup(Account accountWithFullServerContext)
        {
            //mutual server calculation
            //we've already loaded senderservers in account context so we just need to select all memberships from servers to lookup by members accountid and then later when we need to use it flatten the group and select the server.
            var senderMutualServerProfiles = accountWithFullServerContext.Servers.Select(e => e.Server)?.SelectMany(e => e.Members).GroupBy(e => e.AccountId);
            var groupedServerProfiles = senderMutualServerProfiles.ToList();
            return groupedServerProfiles;
        }
        /// <summary>
        /// encapsulates userfulldto mapping option logic.
        /// </summary>
        /// <param name="accountWithContext"></param>
        /// <param name="resolveMutualFriends"></param>
        /// <param name="resolveMutualServers"></param>
        /// <param name="minimizeDuplicatesUsingCacheLookup"></param>
        /// <returns></returns>
        private UserFullDTO GetUserFullDTO(Account accountWithContext, bool resolveMutualFriends = false, bool resolveMutualServers = false, bool minimizeDuplicatesUsingCacheLookup = false)
        {
            List<IGrouping<ulong, FriendshipParticipancy>> mutualFriendsLookup = new();
            List<IGrouping<ulong, ServerProfile>> mutualServersLookup = new();
            if (resolveMutualFriends)
            {
                mutualFriendsLookup = GetMutualFriendLookup(accountWithContext);
            }
            if (resolveMutualServers)
            {
                mutualServersLookup = GetMutualServerServerProfileLookup(accountWithContext);

            }

            //to keep references to minimize data throughput keep mapped references where possible
            //make converter factory to get same converter instances essentially keeping context for mapped objects allowing us to make a dictionary lookup within them to look for existing maps
            var ConverterFactory = new EchoMappingConverterFactory();

            return _mapper.Map<UserFullDTO>(accountWithContext, opts =>
            {
                if (minimizeDuplicatesUsingCacheLookup)
                {
                    opts.ConstructServicesUsing(ConverterFactory.Resolve);
                }
                opts.AfterMap((src, dest) =>
                {
                    //select server and chat member's profile
                    var memberProfileInstances = dest.Servers.SelectMany(e => e.Members.Where(e => e.Profile != null).Select(e => e.Profile))
                    .Concat(dest.DirectMessages.SelectMany(e => e.Participants.Where(e => e.Profile != null).Select(e => e.Profile)))
                    .Where(e => e.Id != accountWithContext.Id) //filter away own profile cause no reason to know own mutual friends xd
                    .ToList();

                    foreach (var memberProfile in memberProfileInstances)
                    {
                        var mutualFriends = mutualFriendsLookup?.FirstOrDefault(e => e.Key == memberProfile.Id)? //find member relevant list from lookup
                        .Select(e => e.Subject.Participants.FirstOrDefault(e => e.ParticipantId != memberProfile.Id)); //select the other end of the participancy.
                                                                                                                       //.ToList(); //need to tolist in case no friends were loaded
                        if (mutualFriends != null && mutualFriends.Any())
                        {
                            memberProfile.MutualFriends = _mapper.Map<List<UserDTO>>(mutualFriends.ToList(), opts =>
                            {
                                if (minimizeDuplicatesUsingCacheLookup)
                                {
                                    opts.ConstructServicesUsing(ConverterFactory.Resolve);
                                }
                            });

                        }

                        var mutualServers = mutualServersLookup?.FirstOrDefault(e => e.Key == memberProfile.Id)? //find member serverprofiles from lookup
                        .Select(e => e.Server); //select servers instead of the profiles.
                                                //.ToList(); //need to tolist in case servers werent loaded
                        if (mutualServers != null && mutualServers.Any())
                        {
                            memberProfile.MutualServers = _mapper.Map<List<ServerMinimalDTO>>(mutualServers, opts =>
                            {
                                if (minimizeDuplicatesUsingCacheLookup)
                                {
                                    opts.ConstructServicesUsing(ConverterFactory.Resolve);
                                }

                            });

                        }


                        var note = accountWithContext.NotedAccounts?.FirstOrDefault(e => e.SubjectId == memberProfile.Id);
                        if (note != null)
                        {
                            memberProfile.Note = note.Note;
                        }
                    }
                    //remember to implement chat orderweight by newest activity.
                    //remember to implement dm chat name by other person thats not the loaded user.
                });
            });
        }

        public async Task<bool> UndeafenSelfAsync(ulong senderId)
        {
            try
            {
                Account acc = await dbContext.Set<Account>().AsQueryable()
                    .Include(e => e.Settings)
                    .ThenInclude(e => e.VoiceSettings)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == senderId);

                acc.Settings.VoiceSettings.DeafenSelf = false;

                var res = await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UnmuteSelfAsync(ulong senderId)
        {
            try
            {
                Account acc = await dbContext.Set<Account>().AsQueryable()
                    .Include(e => e.Settings)
                    .ThenInclude(e => e.VoiceSettings)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == senderId);

                acc.Settings.VoiceSettings.MuteSelf = false;

                var res = await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UnblockUserAsync(ulong senderId, ulong userId)
        {
            try
            {
                if (senderId == userId)
                {
                    return false;
                }
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

                dbContext.Set<AccountBlock>().Remove(blockrelation); //throws error if not already blocked fyi
                var res = await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UnmuteUserAsync(ulong senderId, ulong userId)
        {
            try
            {
                if (senderId == userId)
                {
                    return false;
                }
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
                //senderAccount.MutedVoices.Add(muterelation);

                //dbContext.Set<Account>().Attach(senderAccount); //throws error if already blocked fyi
                //senderAccount.MutedVoices.Remove(muterelation);
                dbContext.Set<AccountMute>().Remove(muterelation);

                var res = await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> StartDirectMessages(ulong senderId, ulong receiverId)
        {
            try
            {
                if (senderId == receiverId)
                {
                    return false;
                }
               
                var participancies = await dbContext.Set<AccountDirectMessageRelation>()
                    .AsSplitQuery()
                    .Where(e => e.OwnerId == senderId || e.OwnerId == receiverId) //filter rows by participantid part of key.
                 .ToListAsync();
                var existingFriendship = participancies
                    .GroupBy(r => r.RelationId) // find rows where dm is same id and group them
                    .Where(x => x.Count() > 1) //check if more than 1 in group meaning that two different rows have been matched on subjectid
                .SelectMany(g => g); //flatten result into list
                if (existingFriendship.Any()) //list will be empty if no matches
                {
                    //here you would normally return the existing chats id via controller or publish a domain event via signalr or masstransit to tell the specific client of the chat and to navigate to it.
                    return false;
                }

                DirectMessageRelation directMessageRelation = new()
                {
                    AccountsInRelation = new List<AccountDirectMessageRelation>()
                        {
                            new AccountDirectMessageRelation()
                            {
                                    OwnerId = senderId,
                            },
                            new AccountDirectMessageRelation()
                            {
                                    OwnerId = receiverId,
                            }
                        },
                     Chat = new Chat()
                     {
                           Name = Guid.NewGuid().ToString(), //name doesnt matter since it will display as the other person for the viewer. //just put guid for now since cant be null and i want to see the chats easily in db
                           Participants = new List<ChatParticipancy>()
                           {
                               new()
                               {
                                    ParticipantId = senderId,
                                     IsOwner = true, //dont know if i should make them owner or if i should just make both nonowner
                                    Hidden = false,
                               },
                               new()
                               {
                                    ParticipantId= receiverId,
                                    IsOwner = true, //dont know if i should make them owner or if i should just make both nonowner
                                    Hidden = true, //starts hidden, will remove dm relation if no messages are sent within a certain timeframe probably
                               },
                           },
                     }
                };

                
                await dbContext.Set<DirectMessageRelation>().AddAsync(directMessageRelation);
                var res = await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> StartDirectMessages(ulong senderId, ulong receiverId, SendMessageRequestDTO requestDTO)
        {
            //the client should check existing chats to see if they are present in memory before calling this function
            //if they are then they should call chatcontroller sendmessage
            //if they are not then they should call this function which creates relation and then message.
            try
            {
                if (senderId == receiverId)
                {
                    return false;
                }

                var participancies = await dbContext.Set<AccountDirectMessageRelation>()
                    .Include(x => x.Relation)
                    .ThenInclude(x=>x.Chat)
                    .Where(e => e.OwnerId == senderId || e.OwnerId == receiverId) //filter rows by participantid part of key.
                    .AsSplitQuery()
                 .ToListAsync();
                var existingFriendship = participancies
                    .GroupBy(r => r.RelationId) // find rows where dm is same id and group them
                    .Where(x => x.Count() > 1) //check if more than 1 in group meaning that two different rows have been matched on subjectid
                .SelectMany(g => g); //flatten result into list

                var message = new ChatMessage()
                {
                    AuthorId = senderId,
                    Content = requestDTO.Content,
                    ParentId = null,

                    Attachments = requestDTO.Attachments.Select(e => new ChatMessageAttachment()
                    {
                        Description = e.Description,
                        FileLocationURL = e.FileLocationURL,
                        FileName = e.FileName
                    }).ToList(),
                };

                if (existingFriendship.Any()) //list will be empty if no matches
                {
                    ////send message if dm exists //nah then you wouldnt be able to use this function since you've already got the data.
                    //message.MessageHolderId = existingFriendship.First().Relation.ChatId;
                    //await dbContext.Set<ChatMessage>().AddAsync(message);
                    //await dbContext.SaveChangesAsync();
                    //here you would normally return the existing chats id via controller or publish a domain event via signalr or masstransit to tell the specific client of the chat and to navigate to it.
                    return false;
                }

                DirectMessageRelation directMessageRelation = new()
                {
                    AccountsInRelation = new List<AccountDirectMessageRelation>()
                        {
                            new AccountDirectMessageRelation()
                            {
                                    OwnerId = senderId,
                            },
                            new AccountDirectMessageRelation()
                            {
                                    OwnerId = receiverId,
                            }
                        },
                    Chat = new Chat()
                    {
                        Name = Guid.NewGuid().ToString(), //name doesnt matter since it will display as the other person for the viewer. //just put guid for now since cant be null and i want to see the chats easily in db
                        Participants = new List<ChatParticipancy>()
                           {
                               new()
                               {
                                    ParticipantId = senderId,
                                     IsOwner = true, //dont know if i should make them owner or if i should just make both nonowner
                                    Hidden = false,
                               },
                               new()
                               {
                                    ParticipantId= receiverId,
                                    IsOwner = true, //dont know if i should make them owner or if i should just make both nonowner
                                    Hidden = false, //starts hidden, will remove dm relation if no messages are sent within a certain timeframe probably
                               },
                           },
                         Messages= new List<ChatMessage>()
                         {
                             new()
                             { 
                                 AuthorId= senderId,
                                  Content = requestDTO.Content,
                                     ParentId = null,
                                      
                                  Attachments=requestDTO.Attachments.Select(e=>new ChatMessageAttachment()
                                  {
                                       Description=e.Description,
                                        FileLocationURL=e.FileLocationURL,
                                       FileName   =e.FileName
                                  }).ToList(),
                             }
                         }
                    }
                };


                await dbContext.Set<DirectMessageRelation>().AddAsync(directMessageRelation);
                var res = await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<TokenDTO> RefreshAuthenticationAsync(ulong senderId, string refreshToken)
        {
            try
            {
               
                var acc = await dbContext.Set<Account>()
                    .Include(e => e.Sessions
                    .Where(e => //device is not supported yet.
                    e.RefreshToken == refreshToken))
                    .FirstOrDefaultAsync(e => e.Id == senderId);
                   
                if (acc == null)
                {
                    throw new Exception("account not present");
                }
                var session = acc.Sessions.FirstOrDefault();
                if (session == null)
                {
                    throw new Exception("session doesnt exist");
                }
                //token session has to be ongoing,
                //expiration time has to be null for perma
                //or above time now else it has expired.
                var isValid = session.TimeStopped == null &&
                    session.ExpirationTime == null 
                    || session.ExpirationTime > DateTime.UtcNow;
                if (!isValid)
                {
                    throw new Exception("token expired or logged out");
                }
                //rotate both tokens.
                var newToken = _tokenHandler.GetAccessToken(acc);
                var newRefreshToken = _tokenHandler.GetRefreshToken(acc);
                session.RefreshToken = newRefreshToken;
                session.AccessToken = newToken;
                var tokens = new TokenDTO()
                {
                     RefreshToken = newRefreshToken,
                     Token = newToken,
                };
                await dbContext.SaveChangesAsync();
                return tokens;
              
            }
            catch (Exception e)
            {
                //errors
                throw;
            }
        }

        public async Task<bool> LogoutAsync(ulong senderId, string refreshToken)
        {
            try
            {
                var acc = await dbContext.Set<Account>()
                    .Include(e => e.Sessions
                    .Where(e => //device is not supported yet.
                    e.TimeStopped == null && //only find logged out sessions
                    e.RefreshToken == refreshToken))
                    .FirstOrDefaultAsync(e => e.Id == senderId);

                if (acc == null)
                {
                    return false;
                    //throw new Exception("account not present");
                }
                var session = acc.Sessions.FirstOrDefault();
                if (session == null)
                {
                    return false;
                    //throw new Exception("session doesnt exist");
                }
                session.TimeStopped = DateTime.UtcNow;
                await dbContext.SaveChangesAsync();

            }
            catch (Exception e)
            {
                //errors
                throw;
            }
             return true;
        }
    }
}
