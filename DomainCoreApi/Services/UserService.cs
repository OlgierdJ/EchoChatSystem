using AutoMapper;
using Azure.Core;
using CoreLib.DTO.EchoCore.UserCore;
using CoreLib.DTO.RequestCore.FriendCore;
using CoreLib.DTO.RequestCore.RelationCore;
using CoreLib.DTO.RequestCore.UserCore;
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
using DomainCoreApi.EFCORE;
using DomainCoreApi.EFCORE.Configurations.FriendCore;
using DomainCoreApi.Handlers;
using DomainCoreApi.Services.Bases;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhoneNumbers;
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

                var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                //verify acceptingacc is part of inc request.
                if (request == null || request.ReceiverId == senderId)
                {
                    return false;
                }
                var existingFriendship = await dbContext.Set<FriendshipParticipancy>().AsQueryable()
                    .Where(e => e.ParticipantId == senderId || e.ParticipantId == request.SenderRequest.SenderId) //filter rows by participantid part of key.
                    .GroupBy(r => r.SubjectId) // find rows where friendship is same id and group them
                 .Where(x => x.Count() > 1) //check if more than 1 in group meaning that two different rows have been matched on subjectid
                 .SelectMany(g => g) //flatten result into list
                 .ToListAsync();
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
                dbContext.Set<IncomingFriendRequest>().Remove(request); //cleanup cause appearently clientcascade doesnt work????
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
                if (requestDTO.TypeId==0)
                {
                    return false;
                }
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

        public async Task<bool> RegisterAsync(RegisterRequestDTO input)
        {
            try
            {
                var userPwd = await _pwdHandler.CreatePassword(input.Password);
                Account account = new Account()
                {
                    Name = input.Username,
                    TimeCreated = DateTime.UtcNow,
                    //CustomStatus = new() //should not contain at first
                    //{
                    //    CustomMessage = "Online"
                    //},
                    ActivityStatusId = 1,
                    Roles = new List<Role>() { },
                    User = new()
                    {
                        Email = input.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = false,
                        PasswordSetDate = DateTime.UtcNow,
                        DateOfBirth = input.DateOfBirth,
                        SecurityCredentials = userPwd
                    },
                    Profile = new()
                    {
                        DisplayName = !String.IsNullOrEmpty(input.DisplayName) ? input.DisplayName : input.Username,
                        AvatarFileURL = GetIconFromList(),
                        BannerColor = "Success",
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
                await dbContext.Set<Account>().AddAsync(account);

                await dbContext.SaveChangesAsync();
                account.Roles.Add(new Role() { Id = 1 });
                await dbContext.SaveChangesAsync();

                //var data = await _createUserHandler.CreateHandler(input);
                //var result = await _repository.AddAsync(data.Item1);
                //data.Item2.UserId = result.Id;
                //await _pwdHandler.CreatePassword(input.Password, data.Item1.Id);
                //await _accountService.AddAsync(data.Item2);
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

        public async Task<string> LoginAsync(LoginRequestDTO attempt)
        {
            var user = await dbContext.Set<User>()
                .Include(e => e.SecurityCredentials)
                .Include(e => e.Account)
                .FirstOrDefaultAsync(e => e.Email == attempt.Email);
            if (user is not null && await _pwdHandler.CheckPassword(attempt.Password, user.SecurityCredentials))
            {
                //send a jwt back to the website
                DomainCoreApi.Handlers.TokenHandler tokenHandler = new();
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
                var acc = await dbContext.Set<Account>().Include(e=>e.Friendships).ThenInclude(e=>e.Participant).AsQueryable().FirstOrDefaultAsync(e => e.Id == senderId);
                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                var removed = acc.Friendships.FirstOrDefault(e => e.Subject.Participants.Any(e => e.ParticipantId == friendId)).Subject;
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
                //Account acc = new()
                //{
                //    Id = senderId,
                //    //something different than potential change //0 is always different than null and no id can have id 0 so its gucci
                //};
                var acc = await dbContext.Set<Account>().Include(e => e.CustomStatus).FirstOrDefaultAsync(e => e.Id == senderId);
                if (acc == null)
                {
                    return false;
                }

                //efficient update
                //await dbContext.Set<AccountCustomStatus>().AsQueryable().Where(e=>e.Id==senderId)
                //    .ExecuteUpdateAsync(setters => 
                //    setters.SetProperty(b => b.CustomMessage, requestDTO.Content)
                //    .SetProperty(b=>b.ExpirationTime, requestDTO.TimeExpires)
                //    );

                if (requestDTO.Content.IsNullOrEmpty() && acc.CustomStatus == null) //if request is null ignore by now
                {
                    return false;
                }

                if (!requestDTO.Content.IsNullOrEmpty() && acc.CustomStatus != null) //update if not null and content not null
                {
                    acc.CustomStatus.CustomMessage = requestDTO.Content;
                    acc.CustomStatus.ExpirationTime = requestDTO.TimeExpires;
                }

                if (!requestDTO.Content.IsNullOrEmpty() && acc.CustomStatus == null)//if request is not null insert and save.
                {
                    acc.CustomStatus = new() { Id = senderId, CustomMessage = requestDTO.Content, ExpirationTime = requestDTO.TimeExpires };
                }

                if (requestDTO.Content.IsNullOrEmpty() && acc.CustomStatus != null) //if status exists and request is null then remove it
                {
                    dbContext.Set<AccountCustomStatus>().Remove(acc.CustomStatus);
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

                var senderAcc = await dbContext.Set<Account>().Include(e => e.NicknamedAccounts.Where(e => e.SubjectId == userId)).FirstOrDefaultAsync(e => e.Id == senderId);
                if (senderAcc == null)
                {
                    return false;
                }
               
                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                AccountNickname relation = new()
                {
                    SubjectId = userId,
                    AuthorId = senderId,
                    Nickname = requestDTO.Nickname,
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

                if (!requestDTO.Nickname.IsNullOrEmpty() && !relationExists) //add if not null and content not null
                {
                    senderAcc.NicknamedAccounts.Add(relation);
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

                var senderAcc = await dbContext.Set<Account>().Include(e => e.NotedAccounts.Where(e=>e.SubjectId == userId)).FirstOrDefaultAsync(e => e.Id == senderId);
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

                if (!requestDTO.Note.IsNullOrEmpty() && !relationExists) //add if not null and content not null
                {
                    senderAcc.NotedAccounts.Add(relation);
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
                //dbContext.Set<User>().Attach(senderAcc.User); //dont need to attach
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
                if (requestDTO.Id == 0)
                {
                    return false;
                }
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

                if (requestDTO.Volume==defaultVolume && !relationExists) //if request is null ignore by now
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
                var userCred = await _pwdHandler.CreatePassword(password);
                userCred.UserId = id;
                dbContext.Set<SecurityCredentials>().Update(userCred);
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
                    .Include(e=>e.Friendships).ThenInclude(e=>e.Subject).ThenInclude(e=>e.Participants.Where(e=>e.ParticipantId!=senderId))
                    .Include(e => e.OutgoingFriendRequests).ThenInclude(e=>e.ReceiverRequest)
                    .Include(e=>e.IncomingFriendRequests).ThenInclude(e=>e.SenderRequest)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == senderId);

                //var requests = await dbContext.Set<OutgoingFriendRequest>().AsQueryable().Include(e => e.ReceiverRequest).Where(e => e.SenderId == senderId).AsNoTracking().ToListAsync();

                if (acc.OutgoingFriendRequests.Any(e => e.ReceiverRequest.ReceiverId == receiverId) || acc.Friendships.Any(e=>e.Subject.Participants.Select(e=>e.ParticipantId).Contains(receiverId))) 
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

                .Include(x => x.Roles).ThenInclude(e => e.Permissions)
                .Include(x => x.Friendships).ThenInclude(e => e.Subject).ThenInclude(e => e.Participants).ThenInclude(e => e.Participant).ThenInclude(e => e.ActivityStatus)
                .Include(x => x.Friendships).ThenInclude(e => e.Subject).ThenInclude(e => e.Participants).ThenInclude(e => e.Participant).ThenInclude(e => e.CustomStatus)

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
                .FirstOrDefaultAsync(e => e.Id == senderId);

                var chatsToLoad = new List<ulong>(senderAcc.Chats.Select(e => e.SubjectId));
                var friendshipsToLoad = new List<ulong>(senderAcc.Chats.Select(e => e.SubjectId));
                var serversToLoad = new List<ulong>(senderAcc.Servers.Select(e => e.ServerId));
                List<Chat> senderChats = null;
                List<Server> senderServers = null;
                if (chatsToLoad.Count > 0)
                {

                    senderChats = await dbContext.Set<Chat>()
                         //chat
                         .Include(e => e.Messages).ThenInclude(e => e.Attachments) //auto includes parent and children
                         .Include(e => e.Participants).ThenInclude(e => e.Participant).ThenInclude(e => e.Profile).ThenInclude(e => e.Account).ThenInclude(e => e.ActivityStatus)
                         .Include(e => e.Invites)
                         .Include(e => e.Pinboard).ThenInclude(e => e.PinnedMessages) //auto connects messages
                         .Where(e => chatsToLoad.Contains(e.Id))
                         .AsSplitQuery().ToListAsync();
                }
                if (serversToLoad.Count > 0)
                {
                    senderServers = await dbContext.Set<Server>()
                    //server
                    .Include(e => e.AuditLogs).ThenInclude(e => e.Account)
                    .Include(e => e.BanList).ThenInclude(e => e.Account)
                    .Include(e => e.ChannelCategories).ThenInclude(e => e.RolePermissions)
                    .Include(e => e.ChannelCategoryMemberPermissions)
                    .Include(e => e.ChannelCategoryMemberSettings)
                    .Include(e => e.Emotes).ThenInclude(e => e.Uploader)
                    .Include(e => e.Events).ThenInclude(e => e.Creator)
                    .Include(e => e.Invites).ThenInclude(e => e.Inviter)
                    .Include(e => e.Members).ThenInclude(e => e.Account).ThenInclude(e => e.Profile)
                    .Include(e => e.Members).ThenInclude(e => e.Account).ThenInclude(e => e.ActivityStatus)
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
                    .Include(e => e.TextChannels).ThenInclude(e => e.Pinboard).ThenInclude(e => e.PinnedMessages)
                    .Include(e => e.TextChannelMemberPermissions)
                    .Include(e => e.TextChannelMemberSettings)
                    //voicechannel
                    .Include(e => e.VoiceChannels).ThenInclude(e => e.VoiceInvites)
                    .Include(e => e.VoiceChannels).ThenInclude(e => e.Region)
                    .Include(e => e.VoiceChannelMemberPermissions)
                    .Include(e => e.VoiceChannelMemberSettings)
                     .Where(e => serversToLoad.Contains(e.Id))
                    .AsSplitQuery().ToListAsync();
                }
                ////get list to crosscheck
                //var senderFriends = senderAcc.Friendships.SelectMany(e => e.Subject.Participants.Where(e=>e.ParticipantId!=senderAcc.Id)).Select(e => e.ParticipantId);
                ////get accs in context other than sender
                //var otherFriendshipsWithMutualFriends = dbContext.Set<Friendship>().Local.Where(f => !f.Participants.Any(fp=>fp.ParticipantId == senderAcc.Id) && f.Participants.Any(fp=>senderFriends.Contains(fp.ParticipantId)));


                //var mutualAquaintances = otherFriendshipsWithMutualFriends.SelectMany(e => e.Participants.Select(e=>e.Participant));


                return mapper.Map<UserFullDTO>(senderAcc
                //,opts=>opts
                //.AfterMap((src, dest) =>
                //{
                //    //select server and chat member's profile
                //    var memberProfileInstances = dest.Servers.SelectMany(e => e.Members.Where(e=>e.Profile!=null).Select(e=>e.Profile))
                //    .Concat(dest.DirectMessages.SelectMany(e => e.Participants.Where(e => e.Profile != null).Select(e => e.Profile)))
                //    .ToList();
                //    foreach (var memberProfile in memberProfileInstances)
                //    {
                //        memberProfile.MutualFriends = mapper.Map<List<UserDTO>>(mutualAquaintances.Where(e => e.Id == memberProfile.Id));
                //    }

                //})
                );
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UndeafenSelfAsync(ulong senderId)
        {
            try
            {
                VoiceSettings voiceSettings = new()
                {
                    Id = senderId,
                    DeafenSelf = true, //must be different value than new value
                };


                dbContext.Set<VoiceSettings>().Attach(voiceSettings);
                voiceSettings.DeafenSelf = false;
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
                VoiceSettings voiceSettings = new()
                {
                    Id = senderId,
                    MuteSelf = true,
                };


                dbContext.Set<VoiceSettings>().Attach(voiceSettings);
                voiceSettings.MuteSelf = false;
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

                senderAccount.BlockedAccounts.Add(blockrelation);
                dbContext.Set<Account>().Attach(senderAccount); //throws error if already blocked fyi
                senderAccount.BlockedAccounts.Remove(blockrelation);
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
                senderAccount.MutedVoices.Add(muterelation);

                dbContext.Set<Account>().Attach(senderAccount); //throws error if already blocked fyi
                senderAccount.MutedVoices.Remove(muterelation);
                var res = await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
