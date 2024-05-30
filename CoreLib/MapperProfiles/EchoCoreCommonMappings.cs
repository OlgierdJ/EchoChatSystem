using AutoMapper;
using CoreLib.DTO.EchoCore.ChatCore;
using CoreLib.DTO.EchoCore.ChatCore.TextCore;
using CoreLib.DTO.EchoCore.ChatCore.VoiceCore;
using CoreLib.DTO.EchoCore.MiscCore;
using CoreLib.DTO.EchoCore.MiscCore.AppearanceCore;
using CoreLib.DTO.EchoCore.MiscCore.ModerationCore;
using CoreLib.DTO.EchoCore.ServerCore;
using CoreLib.DTO.EchoCore.UserCore;
using CoreLib.DTO.EchoCore.UserCore.SettingsCore;
using CoreLib.DTO.EchoCore.UserCore.SubscriptionCore;
using CoreLib.DTO.RequestCore.MessageCore;
using CoreLib.Entities.EchoCore;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ApplicationCore;
using CoreLib.Entities.EchoCore.ApplicationCore.Settings;
using CoreLib.Entities.EchoCore.ApplicationCore.SettingsCore;
using CoreLib.Entities.EchoCore.ApplicationCore.SubscriptionCore;
using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Entities.EchoCore.FriendCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.VoiceChannel;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ModerationCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.SettingsCore;
using CoreLib.Entities.EchoCore.ServerCore.Management;
using CoreLib.Entities.Enums;
using CoreLib.MapperProfiles.MapperProfileConverters;
using System;

namespace CoreLib.MapperProfiles
{
    public class EchoCoreCommonMappings : Profile
    {
        public EchoCoreCommonMappings()
        {
            //need chat voicechat and member not done serverstuff too //i hate mapping aftermap i fk hate it why does it not reuse innersrc for innerdest???

            //CreateMap<Account, UserMinimalDTO>().ForMember(dest => dest.DisplayName, opts => opts.MapFrom(val => val.Profile.DisplayName)); //output members are same name so no need to map
            //chatcore
            CreateMap<Account, MemberDTO>()
               .ForMember(dest => dest.ActiveStatus, opts => opts.Ignore())
               .ForMember(dest => dest.ImageIconURL, opts => opts.MapFrom(e => e.Profile.AvatarFileURL))
               .ForMember(dest => dest.IsOwner, opts => opts.Ignore()) // map aftermap based on chat owner id.
               .ForMember(dest => dest.Id, opts => opts.MapFrom(e => e.Id))
               .ForMember(dest => dest.NameColour, opts => opts.MapFrom(e => e.ActivityStatus.Name.ToLower() != "offline" ? "#ffffff" : "#000000"))
               .ForMember(dest => dest.GroupingName, opts => opts.MapFrom(e => e.ActivityStatus.Name.ToLower() != "offline" ? "ONLINE" : "OFFLINE"))
               .ForMember(dest => dest.DisplayName, opts => opts.MapFrom(e => e.Profile.DisplayName))
               .ForMember(dest => dest.Profile, opts => opts.Ignore())
               .AfterMap((src, dest, ctx) =>
               {
                   dest.Profile = ctx.Mapper.Map<ExternalUserProfileDTO>(src.Profile);
                   dest.ActiveStatus = ctx.Mapper.Map<ActiveActivityStatusDTO>(src);
               }); //map badges, mutual servers, mutualfriends //maybe works maybe ignore at first and load from client.
            CreateMap<Account, UserMinimalDTO>()
               .ForMember(dest => dest.ImageIconURL, opts => opts.MapFrom(src => src.Profile.AvatarFileURL))
               .ForMember(dest => dest.DisplayName, opts => opts.MapFrom(src => src.Profile.DisplayName))
               .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id)).PreserveReferences();
            CreateMap<ServerTextChannel, ChatDTO>()
                .ForMember(dest => dest.RoleSettings, opts => opts.MapFrom(src => src.RoleSettings)) //output members are same name so no need to map
                .ForMember(dest => dest.MemberSettings, opts => opts.MapFrom(src => src.MemberSettings))
                .ForMember(dest => dest.Participants, opts => opts.Ignore())
                .ForMember(dest => dest.UserPermissions, opts => opts.Ignore())
                .ForMember(dest => dest.Muted, opts => opts.Ignore())
                .ForMember(dest => dest.LastReadMessageId, opts => opts.Ignore())
                .ForMember(dest => dest.CategoryName, opts => opts.Ignore())
                .ForMember(dest => dest.OrderWeight, opts => opts.Ignore())  //map muted, lastreadmessageid, participants, permissions, membersettings, roles aftermap //map this in aftermap from service layer.
                .ForMember(dest => dest.IconUrl, opts => opts.MapFrom(src => "HashtagIcon.png")); //map muted, lastreadmessageid, participants, permissions, membersettings, roles aftermap //map this in aftermap from service layer.
            CreateMap<ChatParticipancy, ChatDTO>()
                .ForMember(dest => dest.IconUrl, opts => opts.MapFrom(src => src.Subject.IconUrl))
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Subject.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Subject.Name))
                .ForMember(dest => dest.Messages, opts => opts.MapFrom(src => src.Subject.Messages))
                .ForMember(dest => dest.Pinboard, opts => opts.Ignore())
                .ForMember(dest => dest.Participants, opts => opts.MapFrom(src => src.Subject.Participants))
                .ForMember(dest => dest.Muted, opts => opts.MapFrom(src => src.Participant.MutedChats.Any(c => c.SubjectId == src.SubjectId)))
                .ForMember(dest => dest.RoleSettings, opts => opts.Ignore())
                .ForMember(dest => dest.MemberSettings, opts => opts.Ignore())
                //.ForMember(dest => dest.Participants, opts => opts.Ignore())
                .ForMember(dest => dest.UserPermissions, opts => opts.Ignore())
                .ForMember(dest => dest.LastReadMessageId, opts => opts.Ignore())
                .ForMember(dest => dest.CategoryName, opts => opts.Ignore())
                .ForMember(dest => dest.OrderWeight, opts => opts.Ignore());  //map muted, lastreadmessageid, participants, permissions, permissions, membersettings, roles aftermap //map this in aftermap from service layer.
            CreateMap<Chat, ChatDTO>()
                .ForMember(dest => dest.RoleSettings, opts => opts.Ignore())
                .ForMember(dest => dest.MemberSettings, opts => opts.Ignore())
                .ForMember(dest => dest.Participants, opts => opts.Ignore())
                .ForMember(dest => dest.UserPermissions, opts => opts.Ignore())
                .ForMember(dest => dest.Muted, opts => opts.Ignore())
                .ForMember(dest => dest.LastReadMessageId, opts => opts.Ignore())
                .ForMember(dest => dest.CategoryName, opts => opts.Ignore())
                .ForMember(dest => dest.OrderWeight, opts => opts.Ignore());  //map muted, lastreadmessageid, participants, permissions, permissions, membersettings, roles aftermap //map this in aftermap from service layer.

            CreateMap<ServerTextChannel, ChatMinimalDTO>().ForMember(dest => dest.CategoryName, opts => opts.MapFrom(src => src.Category.Name)); //output members are same name so no need to map
            CreateMap<ServerVoiceChannel, ChatMinimalDTO>().ForMember(dest => dest.CategoryName, opts => opts.MapFrom(src => src.Category.Name)); //output members are same name so no need to map
            CreateMap<Chat, ChatMinimalDTO>()
                .ForMember(dest => dest.CategoryName, opts => opts.Ignore())
                .ForMember(dest => dest.OrderWeight, opts => opts.Ignore())
                .PreserveReferences();

            CreateMap<PaymentMethod, PaymentMethodDTO>(); //output members are same name so no need to map
            CreateMap<PaymentType, PaymentTypeDTO>(); //output members are same name so no need to map
            CreateMap<Subscription, SubscriptionDTO>(); //output members are same name so no need to map
            CreateMap<SubscriptionPlan, SubscriptionPlanDTO>(); //output members are same name so no need to map
            CreateMap<SubscriptionTransaction, SubscriptionTransactionDTO>(); //output members are same name so no need to map
            CreateMap<SubscriptionTransactionGroup, SubscriptionTransactionGroupDTO>(); //output members are same name so no need to map
            CreateMap<SubscriptionTransactionRefund, SubscriptionTransactionRefundDTO>(); //output members are same name so no need to map

            CreateMap<AccessibilitySettings, AccessibilitySettingsDTO>(); //output members are same name so no need to map
            CreateMap<ActivitySettings, ActivitySettingsDTO>(); //output members are same name so no need to map
            CreateMap<AdvancedSettings, AdvancedSettingsDTO>(); //output members are same name so no need to map
            CreateMap<AppearanceSettings, AppearanceSettingsDTO>(); //output members are same name so no need to map
            CreateMap<BillingInformation, BillingInformationDTO>()
                .ForMember(e => e.Transactions, opts => opts.MapFrom(src => src.Subscriptions.SelectMany(sub => sub.SubcriptionTransactions))); //output members are same name so no need to map
            CreateMap<ChatSettings, ChatSettingsDTO>(); //output members are same name so no need to map
            CreateMap<FriendRequestSettings, FriendRequestSettingsDTO>(); //output members are same name so no need to map
            CreateMap<GameOverlaySettings, GameOverlaySettingsDTO>(); //output members are same name so no need to map
            CreateMap<KeybindSettings, KeybindSettingsDTO>(); //output members are same name so no need to map
            CreateMap<NotificationSettings, NotificationSettingsDTO>(); //output members are same name so no need to map
            CreateMap<PrivacySettings, PrivacySettingsDTO>(); //output members are same name so no need to map
            CreateMap<SoundboardSettings, SoundboardSettingsDTO>(); //output members are same name so no need to map
            CreateMap<StreamerModeSettings, StreamerModeSettingsDTO>(); //output members are same name so no need to map
            CreateMap<VideoSettings, VideoSettingsDTO>(); //output members are same name so no need to map
            CreateMap<VoiceSettings, VoiceSettingsDTO>(); //output members are same name so no need to map
            CreateMap<WindowSettings, WindowSettingsDTO>(); //output members are same name so no need to map

            CreateMap<AccountActivityStatus, ActivityStatusDTO>(); //output members are same name so no need to map
            CreateMap<AccountActivityStatus, ActivityStatusMinimalDTO>(); //output members are same name so no need to map

            CreateMap<OutgoingFriendRequest, FriendRequestDTO>()
                .ForMember(dest => dest.Person, opts => opts.MapFrom(e => e.ReceiverRequest.Receiver))
                .ForMember(dest => dest.Type, opts => opts.MapFrom(e => RequestType.Outgoing));
            CreateMap<IncomingFriendRequest, FriendRequestDTO>()
               .ForMember(dest => dest.Person, opts => opts.MapFrom(e => e.SenderRequest.Sender))
               .ForMember(dest => dest.Type, opts => opts.MapFrom(e => RequestType.Incoming));

            CreateMap<ChatInvite, InviteDTO>()
                .ForMember(dest => dest.ImageIconURL, opts => opts.MapFrom(e => e.Subject.IconUrl)) //map ImageIconURL (chat icon)
                .ForMember(dest => dest.Title, opts => opts.MapFrom(e => e.Subject.Name)) //map Title (chat name)
                .ForMember(dest => dest.Description, opts => opts.MapFrom(e => e.Subject.Participants.Count())) //map Description (for chat how many members.)
                .ForMember(dest => dest.InviteLink, opts => opts.MapFrom(e => "https://localhost:7269/Chat/ConsumeInvite/" + e.InviteCode)) //map InviteLink (httpslink to Chat/consumeInvite/code), 
                .ForMember(dest => dest.Type, opts => opts.MapFrom(e => InviteType.Chat)); //map InviteType (chat) // aftermap
            CreateMap<ChatInvite, InviteMinimalDTO>()
                .ForMember(dest => dest.InviteLink, opts => opts.MapFrom(e => "https://localhost:7269/Chat/ConsumeInvite/" + e.InviteCode)) //map InviteLink (httpslink to Chat/consumeInvite/code), 
                .ForMember(dest => dest.Type, opts => opts.MapFrom(e => InviteType.Chat)); //map InviteType (chat) // aftermap

            CreateMap<ServerInvite, InviteDTO>()
                .ForMember(dest => dest.ImageIconURL, opts => opts.MapFrom(e => e.Subject.Settings.ServerImageUrl)) //map ImageIconURL (server icon)
                .ForMember(dest => dest.Title, opts => opts.MapFrom(e => e.Subject.Name)) //map Title (server name)
                .ForMember(dest => dest.Description, opts => opts.MapFrom(e => e.Channel.Name)) //map Description (for server where to)
                .ForMember(dest => dest.InviteLink, opts => opts.MapFrom(e => "https://localhost:7269/Server/textchannel/ConsumeInvite/" + e.InviteCode)) //map InviteLink (httpslink to Server/textchannel/consumeInvite/code), 
                .ForMember(dest => dest.Type, opts => opts.MapFrom(e => InviteType.Server)); //map InviteType (server) // aftermap
            CreateMap<ServerInvite, InviteMinimalDTO>()
                .ForMember(dest => dest.InviteLink, opts => opts.MapFrom(e => "https://localhost:7269/Server/textchannel/ConsumeInvite/" + e.InviteCode)) //map InviteLink (httpslink to Server/textchannel/consumeInvite/code), 
                .ForMember(dest => dest.Type, opts => opts.MapFrom(e => InviteType.Server)); //map InviteType (server) // aftermap

            CreateMap<ServerVoiceInvite, InviteDTO>()
                .ForMember(dest => dest.ImageIconURL, opts => opts.MapFrom(e => e.Subject.Settings.ServerImageUrl)) //map ImageIconURL (server icon)
                .ForMember(dest => dest.Title, opts => opts.MapFrom(e => e.Subject.Name)) //map Title (server name)
                .ForMember(dest => dest.Description, opts => opts.MapFrom(e => e.Channel.Name)) //map Description (for server where to)
                .ForMember(dest => dest.InviteLink, opts => opts.MapFrom(e => "https://localhost:7269/Server/voicechannel/ConsumeInvite/" + e.InviteCode)) //map InviteLink (httpslink to Server/consumeInvite/code), 
                .ForMember(dest => dest.Type, opts => opts.MapFrom(e => InviteType.Server)); //map InviteType (server) // aftermap
            CreateMap<ServerVoiceInvite, InviteMinimalDTO>()
                .ForMember(dest => dest.InviteLink, opts => opts.MapFrom(e => "https://localhost:7269/Server/voicechannel/ConsumeInvite/" + e.InviteCode)) //map InviteLink (httpslink to Server/consumeInvite/code), 
                .ForMember(dest => dest.Type, opts => opts.MapFrom(e => InviteType.Server)); //map InviteType (server) // aftermap
                                                                                             //map ImageIconURL (server icon), Title (Server name), Description (for server where to), InviteLink (httpslink to Server/consumeInvite/code), InviteType (server) // aftermap
            //CreateMap<ChatPinboard, PinboardDTO>(); //output members are same name so no need to map
            //CreateMap<ServerTextChannelPinboard, PinboardDTO>(); //output members are same name so no need to map
            CreateMap<ChatMessageAttachment, MessageAttachmentDTO>(); //output members are same name so no need to map
            CreateMap<ServerTextChannelMessageAttachment, MessageAttachmentDTO>(); //output members are same name so no need to map
            CreateMap<ChatMessage, MessageMinimalDTO>();
            CreateMap<ChatMessage, MessageDTO>()
                .ForMember(dest => dest.IsPinned, opts => opts.MapFrom(e => e.MessagePin != null))
                .ForMember(dest => dest.Replied, opts => opts.MapFrom(e => e.Parent)) //output members are same name so no need to map
                .ForMember(dest => dest.Sender, opts => opts.Ignore()).AfterMap((src, dest, ctx) =>
                {
                    dest.Sender = ctx.Mapper.Map<UserMinimalDTO>(src.Author);
                }); //output members are same name so no need to map
            CreateMap<ChatMessagePin, MessageDTO>()
                 .ForMember(dest => dest.IsPinned, opts => opts.MapFrom(e => true))
                .ForMember(dest => dest.TimeSent, opts => opts.MapFrom(e => e.Message.TimeSent))
                .ForMember(dest => dest.TimeEdited, opts => opts.MapFrom(e => e.Message.TimeEdited))
                .ForMember(dest => dest.Attachments, opts => opts.MapFrom(e => e.Message.Attachments))
                .ForMember(dest => dest.Content, opts => opts.MapFrom(e => e.Message.Content))
                .ForMember(dest => dest.Id, opts => opts.MapFrom(e => e.Message.Id))
                .ForMember(dest => dest.Replied, opts => opts.Ignore()) //output members are same name so no need to map
                .ForMember(dest => dest.Sender, opts => opts.Ignore()).AfterMap((src, dest, ctx) =>
                {
                    dest.Sender = ctx.Mapper.Map<UserMinimalDTO>(src.Message.Author);
                    dest.Replied = ctx.Mapper.Map<MessageMinimalDTO>(src.Message.Parent);
                }); //output members are same name so no need to map
            CreateMap<ServerTextChannelMessage, MessageMinimalDTO>();
            CreateMap<ServerTextChannelMessage, MessageDTO>()
                .ForMember(dest => dest.IsPinned, opts => opts.MapFrom(e => e.MessagePin != null))
                .ForMember(dest => dest.Replied, opts => opts.Ignore()) //output members are same name so no need to map
                .ForMember(dest => dest.Sender, opts => opts.Ignore()).AfterMap((src, dest, ctx) =>
                {
                    dest.Sender = ctx.Mapper.Map<UserMinimalDTO>(src.Author);
                    dest.Replied = ctx.Mapper.Map<MessageMinimalDTO>(src.Parent);
                }); //output members are same name so no need to map
            CreateMap<ServerTextChannelMessagePin, MessageDTO>()
                .ForMember(dest => dest.IsPinned, opts => opts.MapFrom(e => true))
                .ForMember(dest => dest.TimeSent, opts => opts.MapFrom(e => e.Message.TimeSent))
                .ForMember(dest => dest.TimeEdited, opts => opts.MapFrom(e => e.Message.TimeEdited))
                .ForMember(dest => dest.Attachments, opts => opts.MapFrom(e => e.Message.Attachments))
                .ForMember(dest => dest.Content, opts => opts.MapFrom(e => e.Message.Content))
                .ForMember(dest => dest.Id, opts => opts.MapFrom(e => e.Message.Id))
                .ForMember(dest => dest.Replied, opts => opts.Ignore()) //output members are same name so no need to map
                .ForMember(dest => dest.Sender, opts => opts.Ignore()).AfterMap((src, dest, ctx) =>
                {
                    dest.Sender = ctx.Mapper.Map<UserMinimalDTO>(src.Message.Author);
                    dest.Replied = ctx.Mapper.Map<MessageMinimalDTO>(src.Message.Parent);
                }); //output members are same name so no need to map
            CreateMap<ServerSoundboardSound, SoundboardSoundDTO>(); //output members are same name so no need to map
            CreateMap<ServerSoundboardSound, SoundboardSoundExtendedDTO>(); //output members are same name so no need to map
            CreateMap<ServerVoiceChannel, VoiceChatDTO>()
                .ForMember(dest => dest.RoleSettings, opts => opts.MapFrom(src => src.AllowedRoles)) //need to ignore active participants or perhaps get activeparticipants from voicechannel signalr group but that seems like a hassle
                .ForMember(dest => dest.UserPermissions, opts => opts.Ignore()) //map aftermap based on user permissions
                .ForMember(dest => dest.CategoryName, opts => opts.MapFrom(src => src.Category.Name)) //need to ignore active participants or perhaps get activeparticipants from voicechannel signalr group but that seems like a hassle
                .ForMember(dest => dest.IsMuted, opts => opts.Ignore()) //need to ignore active participants or perhaps get activeparticipants from voicechannel signalr group but that seems like a hassle
                .ForMember(dest => dest.ActiveParticipants, opts => opts.Ignore()); //need to ignore active participants or perhaps get activeparticipants from voicechannel signalr group but that seems like a hassle
                                                                                    //.ForMember(dest=>dest.IsMuted, opts=>opts.MapFrom(e=>e.muters.where(userid))); //map ismuted, membersettings, rolesettings, userpermissions from aftermap with userid.
            CreateMap<ServerEmote, EmoteDTO>(); //output members are same name so no need to map
            CreateMap<ServerEmote, EmoteExtendedDTO>(); //output members are same name so no need to map
            CreateMap<Language, LanguageDTO>(); //output members are same name so no need to map
            CreateMap<Theme, ThemeDTO>(); //output members are same name so no need to map
            CreateMap<ServerAuditLog, AuditLogDTO>().ForMember(dest => dest.User, opts => opts.MapFrom(val => val.Account));
            CreateMap<ServerBan, BanDTO>()
                .ForMember(dest => dest.User, opts => opts.Ignore())
                .AfterMap((src, dest, ctx) =>
                {
                    dest.User = ctx.Mapper.Map<UserMinimalDTO>(new AccountBlock() { BlockedId = src.Id, Blocked = src.Account });
                    
                });

            CreateMap<AccountSession, DeviceSessionDTO>(); //output members are same name so no need to map
            CreateMap<ServerChannelCategoryRolePermission, StatefulPermissionExtendedDTO>()
              .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
              .ForMember(dest => dest.Description, opts => opts.MapFrom(val => val.Permission.Description))
              .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name))
              .ForMember(dest => dest.GroupingName, opts => opts.MapFrom(val => val.Permission.Category.Name)); //state is the same so no need to map
            CreateMap<ServerChannelCategoryRolePermission, StatefulPermissionDTO>()
              .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
              //.ForMember(dest => dest.State, opts => opts.MapFrom(val => val.State))
              .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name));
            CreateMap<ServerChannelCategoryRolePermission, PermissionMinimalDTO>()
              .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
              .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name)); //state is the same so no need to map

            CreateMap<ServerChannelCategoryPermission, PermissionMinimalDTO>()
              .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
              .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name)); //state is the same so no need to map

            CreateMap<ServerChannelCategoryMemberPermission, StatefulPermissionExtendedDTO>()
              .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
              .ForMember(dest => dest.Description, opts => opts.MapFrom(val => val.Permission.Description))
              .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name))
              .ForMember(dest => dest.GroupingName, opts => opts.MapFrom(val => val.Permission.Category.Name)); //state is the same so no need to map
            CreateMap<ServerChannelCategoryMemberPermission, StatefulPermissionDTO>()
              .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
              //.ForMember(dest => dest.State, opts => opts.MapFrom(val => val.State))
              .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name));
            CreateMap<ServerChannelCategoryMemberPermission, PermissionMinimalDTO>()
              .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
              .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name)); //state is the same so no need to map

            CreateMap<ServerVoiceChannelRolePermission, StatefulPermissionExtendedDTO>()
               .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
               .ForMember(dest => dest.Description, opts => opts.MapFrom(val => val.Permission.Description))
               .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name))
               .ForMember(dest => dest.GroupingName, opts => opts.MapFrom(val => val.Permission.Category.Name)); //state is the same so no need to map
            CreateMap<ServerVoiceChannelRolePermission, StatefulPermissionDTO>()
              .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
              //.ForMember(dest => dest.State, opts => opts.MapFrom(val => val.State))
              .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name));
            CreateMap<ServerVoiceChannelRolePermission, PermissionMinimalDTO>()
             .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
             .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name)); //state is the same so no need to map
            CreateMap<ServerVoiceChannelPermission, PermissionMinimalDTO>()
             .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
             .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name)); //state is the same so no need to map

            CreateMap<ServerVoiceChannelMemberPermission, StatefulPermissionExtendedDTO>()
               .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
               .ForMember(dest => dest.Description, opts => opts.MapFrom(val => val.Permission.Description))
               .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name))
               .ForMember(dest => dest.GroupingName, opts => opts.MapFrom(val => val.Permission.Category.Name)); //state is the same so no need to map
            CreateMap<ServerVoiceChannelMemberPermission, StatefulPermissionDTO>()
              .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
              //.ForMember(dest => dest.State, opts => opts.MapFrom(val => val.State))
              .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name));
            CreateMap<ServerVoiceChannelMemberPermission, PermissionMinimalDTO>()
             .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
             .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name)); //state is the same so no need to map

            CreateMap<ServerTextChannelRolePermission, StatefulPermissionExtendedDTO>()
               .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
               .ForMember(dest => dest.Description, opts => opts.MapFrom(val => val.Permission.Description))
               .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name))
               .ForMember(dest => dest.GroupingName, opts => opts.MapFrom(val => val.Permission.Category.Name)); //state is the same so no need to map
            CreateMap<ServerTextChannelRolePermission, StatefulPermissionDTO>()
              .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
              //.ForMember(dest => dest.State, opts => opts.MapFrom(val => val.State))
              .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name));
            CreateMap<ServerTextChannelRolePermission, PermissionMinimalDTO>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name)); //state is the same so no need to map
            CreateMap<ServerTextChannelPermission, PermissionMinimalDTO>()
             .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
             .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name)); //state is the same so no need to map

            CreateMap<ServerTextChannelMemberPermission, StatefulPermissionExtendedDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(val => val.Permission.Description))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name))
                .ForMember(dest => dest.GroupingName, opts => opts.MapFrom(val => val.Permission.Category.Name)); //state is the same so no need to map
            CreateMap<ServerTextChannelMemberPermission, StatefulPermissionDTO>()
              .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
              //.ForMember(dest => dest.State, opts => opts.MapFrom(val => val.State))
              .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name));
            CreateMap<ServerTextChannelMemberPermission, PermissionMinimalDTO>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name)); //state is the same so no need to map

            CreateMap<ServerRolePermission, StatefulPermissionExtendedDTO>()
               .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
               .ForMember(dest => dest.Description, opts => opts.MapFrom(val => val.Permission.Description))
               .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name))
               .ForMember(dest => dest.GroupingName, opts => opts.MapFrom(val => val.Permission.Category.Name)); //state is the same so no need to map
            CreateMap<ServerRolePermission, StatefulPermissionDTO>()
              .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
              //.ForMember(dest => dest.State, opts => opts.MapFrom(val => val.State))
              .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name));
            CreateMap<ServerRolePermission, PermissionMinimalDTO>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name)); //state is the same so no need to map

            CreateMap<Permission, PermissionMinimalDTO>().PreserveReferences(); //output members are same name so no need to map ??? mayb make mapping from RolePermission
            CreateMap<ServerPermission, PermissionMinimalDTO>().PreserveReferences(); //output members are same name so no need to map ????

            CreateMap<ServerPermission, PermissionDTO>()
               .ForMember(dest => dest.GroupingName, opts => opts.MapFrom(val => val.Category.Name)).PreserveReferences(); //state is the same so no need to map; //output members are same name so no need to map ????

            CreateMap<ServerRolePermission, PermissionDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(val => val.Permission.Description))
                .ForMember(dest => dest.GroupingName, opts => opts.MapFrom(val => val.Permission.Category.Name));

            CreateMap<ServerVoiceChannelRolePermission, PermissionDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(val => val.Permission.Description))
                .ForMember(dest => dest.GroupingName, opts => opts.MapFrom(val => val.Permission.Category.Name));
            CreateMap<ServerVoiceChannelMemberPermission, PermissionDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(val => val.Permission.Description))
                .ForMember(dest => dest.GroupingName, opts => opts.MapFrom(val => val.Permission.Category.Name));
            CreateMap<ServerVoiceChannelPermission, PermissionDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(val => val.Permission.Description))
                .ForMember(dest => dest.GroupingName, opts => opts.MapFrom(val => val.Permission.Category.Name));

            CreateMap<ServerTextChannelRolePermission, PermissionDTO>()
               .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
               .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name))
               .ForMember(dest => dest.Description, opts => opts.MapFrom(val => val.Permission.Description))
               .ForMember(dest => dest.GroupingName, opts => opts.MapFrom(val => val.Permission.Category.Name));
            CreateMap<ServerTextChannelMemberPermission, PermissionDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(val => val.Permission.Description))
                .ForMember(dest => dest.GroupingName, opts => opts.MapFrom(val => val.Permission.Category.Name));
            CreateMap<ServerTextChannelPermission, PermissionDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(val => val.Permission.Description))
                .ForMember(dest => dest.GroupingName, opts => opts.MapFrom(val => val.Permission.Category.Name));

            CreateMap<ServerChannelCategoryRolePermission, PermissionDTO>()
               .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
               .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name))
               .ForMember(dest => dest.Description, opts => opts.MapFrom(val => val.Permission.Description))
               .ForMember(dest => dest.GroupingName, opts => opts.MapFrom(val => val.Permission.Category.Name));
               
            CreateMap<ServerChannelCategoryMemberPermission, PermissionDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(val => val.Permission.Description))
                .ForMember(dest => dest.GroupingName, opts => opts.MapFrom(val => val.Permission.Category.Name));
            CreateMap<ServerChannelCategoryPermission, PermissionDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(val => val.PermissionId))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(val => val.Permission.Name))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(val => val.Permission.Description))
                .ForMember(dest => dest.GroupingName, opts => opts.MapFrom(val => val.Permission.Category.Name));

            CreateMap<ConnectionType, ConnectionDTO>(); //output members are same name so no need to map
            CreateMap<AccountConnection, UserConnectionDTO>().ForMember(dest => dest.Type, opts => opts.MapFrom(e => e.ConnectionType)); //output members are same name so no need to map
            CreateMap<Country, CountryDTO>(); //output members are same name so no need to map
            CreateMap<AcceptedCurrency, CurrencyDTO>(); //output members are same name so no need to map
            CreateMap<Keybind, KeybindDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(e => e.ApplicationKeybindId))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(e => e.ApplicationKeybind.Description))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(e => e.ApplicationKeybind.Name)); //probably this is fine
            CreateMap<ServerRegion, RegionDTO>(); //output members are same name so no need to map
            CreateMap<ServerEvent, ServerEventDTO>().ForMember(e => e.InterestedUsers, opts => opts.Ignore()); //interested users not yet implemented
            //?????Check permission and userpermissions tmrw?????

            CreateMap<ServerVoiceChannelMemberSettings, UserMinimalWithPermissionsDTO>()
                .ForMember(dest => dest.DisplayName, opts => opts.MapFrom(e => e.Profile.Nickname))
                .ForMember(dest => dest.Id, opts => opts.MapFrom(e => e.AccountId))
                //.ForMember(dest=>dest.Permissions, opts=>opts.MapFrom(e=>e.Permissions)) //probably not needed for map cause memberpermissions has been mapped
                .ForMember(dest => dest.ImageIconURL, opts => opts.MapFrom(e => e.Profile.ImageIconURL)); //output members are same name so no need to map

            CreateMap<ServerTextChannelMemberSettings, UserMinimalWithPermissionsDTO>()
                .ForMember(dest => dest.DisplayName, opts => opts.MapFrom(e => e.Profile.Nickname))
                .ForMember(dest => dest.Id, opts => opts.MapFrom(e => e.AccountId))
                //.ForMember(dest=>dest.Permissions, opts=>opts.MapFrom(e=>e.Permissions)) //probably not needed for map cause memberpermissions has been mapped
                .ForMember(dest => dest.ImageIconURL, opts => opts.MapFrom(e => e.Profile.ImageIconURL)); //output members are same name so no need to map

            CreateMap<ServerChannelCategoryMemberSettings, UserMinimalWithPermissionsDTO>()
                .ForMember(dest => dest.DisplayName, opts => opts.MapFrom(e => e.Profile.Nickname))
                .ForMember(dest => dest.Id, opts => opts.MapFrom(e => e.AccountId))
                //.ForMember(dest=>dest.Permissions, opts=>opts.MapFrom(e=>e.Permissions)) //probably not needed for map cause memberpermissions has been mapped
                .ForMember(dest => dest.ImageIconURL, opts => opts.MapFrom(e => e.Profile.ImageIconURL)); //output members are same name so no need to map

            //CreateMap<ChatAccountMessageTracker, NotificationMessageDTO>().ForMember(dest=>dest.Id, opts=>opts.MapFrom(e=>)); //aftermap on chat, textchannel to get amount of messages unread???

            //CreateMap<Permission, PermissionMinimalDTO>(); //output members are same name so no need to map ??? mayb make mapping from RolePermission

            CreateMap<ServerPermissionCategory, PermissionCategoryDTO>();//.ForMember(dest => dest.Permissions, opts => opts.MapFrom(val => val.Permissions)); //output members are same name so no need to map probably??????


            CreateMap<Role, RoleMinimalDTO>().PreserveReferences(); //output members are same name so no need to map
            CreateMap<Role, RoleMinimalWithPermissionsDTO>().PreserveReferences(); //output members are same name so no need to map

            CreateMap<Server, ServerMinimalDTO>()
                .ForMember(dest => dest.ImageIconURL, opts => opts.MapFrom(src => src.Settings.ServerImageUrl))
                .PreserveReferences();
            CreateMap<IList<Server>, IList<ServerMinimalDTO>>().ConvertUsing<IServerListServerMinimalListConverter>();

            CreateMap<ServerProfile, ServerMinimalDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(e => e.ServerId))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(e => e.Server.Name))
                .ForMember(dest => dest.ImageIconURL, opts => opts.MapFrom(e => e.Server.Settings.ServerImageUrl))
                .PreserveReferences();


            CreateMap<ServerRole, RoleMinimalDTO>().PreserveReferences(); //output members are same name so no need to map
            CreateMap<ServerRole, RoleMinimalWithPermissionsDTO>().PreserveReferences(); //output members are same name so no need to map
            CreateMap<AccountRole, RoleMinimalDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(e => e.RoleId))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(e => e.Role.Name))
                .PreserveReferences();
            CreateMap<AccountRole, RoleMinimalWithPermissionsDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(e => e.RoleId))
                .ForMember(dest => dest.Permissions, opts => opts.MapFrom(e => e.Role.Permissions))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(e => e.Role.Name))
                .PreserveReferences();
            CreateMap<ServerProfileServerRole, RoleMinimalDTO>()
               .ForMember(dest => dest.Id, opts => opts.MapFrom(e => e.RoleId))
               .ForMember(dest => dest.Name, opts => opts.MapFrom(e => e.Role.Name))
               .PreserveReferences();
            CreateMap<ServerProfileServerRole, RoleMinimalWithPermissionsDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(e => e.RoleId))
                .ForMember(dest => dest.Permissions, opts => opts.MapFrom(e => e.Role.Permissions))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(e => e.Role.Name))
                .PreserveReferences();
            CreateMap<ServerProfileServerRole, HierarchalRoleMinimalDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(e => e.RoleId))
                .ForMember(dest => dest.DisplaySeperatelyFromOnlineMembers, opts => opts.MapFrom(e => e.Role.DisplaySeperatelyFromOnlineMembers))
                .ForMember(dest => dest.Colour, opts => opts.MapFrom(e => e.Role.Colour))
                .ForMember(dest => dest.AllowAnyoneToMention, opts => opts.MapFrom(e => e.Role.AllowAnyoneToMention))
                .ForMember(dest => dest.IconURL, opts => opts.MapFrom(e => e.Role.IconURL))
                .ForMember(dest => dest.Importance, opts => opts.MapFrom(e => e.Role.Importance))
                .ForMember(dest => dest.IsAdmin, opts => opts.MapFrom(e => e.Role.IsAdmin))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(e => e.Role.Name))
                .PreserveReferences(); //output members are same name so no need to map
            CreateMap<ServerProfileServerRole, HierarchalRoleMinimalWithPermissionsDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(e => e.RoleId))
                .ForMember(dest => dest.DisplaySeperatelyFromOnlineMembers, opts => opts.MapFrom(e => e.Role.DisplaySeperatelyFromOnlineMembers))
                .ForMember(dest => dest.Colour, opts => opts.MapFrom(e => e.Role.Colour))
                .ForMember(dest => dest.AllowAnyoneToMention, opts => opts.MapFrom(e => e.Role.AllowAnyoneToMention))
                .ForMember(dest => dest.IconURL, opts => opts.MapFrom(e => e.Role.IconURL))
                .ForMember(dest => dest.Importance, opts => opts.MapFrom(e => e.Role.Importance))
                .ForMember(dest => dest.IsAdmin, opts => opts.MapFrom(e => e.Role.IsAdmin))
                .ForMember(dest => dest.Permissions, opts => opts.MapFrom(e => e.Role.Permissions))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(e => e.Role.Name))
                .PreserveReferences();  //output members are same name so no need to map
            CreateMap<ServerRole, HierarchalRoleMinimalDTO>().PreserveReferences(); //output members are same name so no need to map
            CreateMap<ServerRole, HierarchalRoleMinimalWithPermissionsDTO>().PreserveReferences(); //output members are same name so no need to map
            //CreateMap<ServerRole, ServerRoleDTO>(); // ??????????????? have we actually made an entity that allows for global permissions towards a role or user within a server ??????????????????
            CreateMap<ServerVoiceChannelRole, HierarchalRoleMinimalWithPermissionsDTO>()
               .ForMember(dest => dest.AllowAnyoneToMention, opts => opts.MapFrom(e => e.Role.AllowAnyoneToMention))
               .ForMember(dest => dest.Colour, opts => opts.MapFrom(e => e.Role.Colour))
               .ForMember(dest => dest.DisplaySeperatelyFromOnlineMembers, opts => opts.MapFrom(e => e.Role.DisplaySeperatelyFromOnlineMembers))
               .ForMember(dest => dest.IconURL, opts => opts.MapFrom(e => e.Role.IconURL))
               .ForMember(dest => dest.IsAdmin, opts => opts.MapFrom(e => e.Role.IsAdmin))
               .ForMember(dest => dest.Name, opts => opts.MapFrom(e => e.Role.Name))
               .ForMember(dest => dest.Id, opts => opts.MapFrom(e => e.RoleId))
               .ForMember(dest => dest.Importance, opts => opts.MapFrom(e => e.Role.Importance))
               .PreserveReferences();
            CreateMap<ServerVoiceChannelRole, HierarchalRoleMinimalDTO>()
               .ForMember(dest => dest.AllowAnyoneToMention, opts => opts.MapFrom(e => e.Role.AllowAnyoneToMention))
               .ForMember(dest => dest.Colour, opts => opts.MapFrom(e => e.Role.Colour))
               .ForMember(dest => dest.DisplaySeperatelyFromOnlineMembers, opts => opts.MapFrom(e => e.Role.DisplaySeperatelyFromOnlineMembers))
               .ForMember(dest => dest.IconURL, opts => opts.MapFrom(e => e.Role.IconURL))
               .ForMember(dest => dest.IsAdmin, opts => opts.MapFrom(e => e.Role.IsAdmin))
               .ForMember(dest => dest.Name, opts => opts.MapFrom(e => e.Role.Name))
               .ForMember(dest => dest.Id, opts => opts.MapFrom(e => e.RoleId))
               .ForMember(dest => dest.Importance, opts => opts.MapFrom(e => e.Role.Importance))
               .PreserveReferences();

            CreateMap<ServerTextChannelRole, HierarchalRoleMinimalWithPermissionsDTO>()
                .ForMember(dest => dest.AllowAnyoneToMention, opts => opts.MapFrom(e => e.Role.AllowAnyoneToMention))
               .ForMember(dest => dest.Colour, opts => opts.MapFrom(e => e.Role.Colour))
               .ForMember(dest => dest.DisplaySeperatelyFromOnlineMembers, opts => opts.MapFrom(e => e.Role.DisplaySeperatelyFromOnlineMembers))
               .ForMember(dest => dest.IconURL, opts => opts.MapFrom(e => e.Role.IconURL))
               .ForMember(dest => dest.IsAdmin, opts => opts.MapFrom(e => e.Role.IsAdmin))
               .ForMember(dest => dest.Name, opts => opts.MapFrom(e => e.Role.Name))
               .ForMember(dest => dest.Id, opts => opts.MapFrom(e => e.RoleId))
               .ForMember(dest => dest.Importance, opts => opts.MapFrom(e => e.Role.Importance))
               .PreserveReferences();
            CreateMap<ServerTextChannelRole, HierarchalRoleMinimalDTO>()
                .ForMember(dest => dest.AllowAnyoneToMention, opts => opts.MapFrom(e => e.Role.AllowAnyoneToMention))
               .ForMember(dest => dest.Colour, opts => opts.MapFrom(e => e.Role.Colour))
               .ForMember(dest => dest.DisplaySeperatelyFromOnlineMembers, opts => opts.MapFrom(e => e.Role.DisplaySeperatelyFromOnlineMembers))
               .ForMember(dest => dest.IconURL, opts => opts.MapFrom(e => e.Role.IconURL))
               .ForMember(dest => dest.IsAdmin, opts => opts.MapFrom(e => e.Role.IsAdmin))
               .ForMember(dest => dest.Name, opts => opts.MapFrom(e => e.Role.Name))
               .ForMember(dest => dest.Id, opts => opts.MapFrom(e => e.RoleId))
               .ForMember(dest => dest.Importance, opts => opts.MapFrom(e => e.Role.Importance))
               .PreserveReferences();

            CreateMap<ServerChannelCategoryRole, HierarchalRoleMinimalWithPermissionsDTO>()
                .ForMember(dest => dest.AllowAnyoneToMention, opts => opts.MapFrom(e => e.Role.AllowAnyoneToMention))
               .ForMember(dest => dest.Colour, opts => opts.MapFrom(e => e.Role.Colour))
               .ForMember(dest => dest.DisplaySeperatelyFromOnlineMembers, opts => opts.MapFrom(e => e.Role.DisplaySeperatelyFromOnlineMembers))
               .ForMember(dest => dest.IconURL, opts => opts.MapFrom(e => e.Role.IconURL))
               .ForMember(dest => dest.IsAdmin, opts => opts.MapFrom(e => e.Role.IsAdmin))
               .ForMember(dest => dest.Name, opts => opts.MapFrom(e => e.Role.Name))
               .ForMember(dest => dest.Id, opts => opts.MapFrom(e => e.RoleId))
               .ForMember(dest => dest.Importance, opts => opts.MapFrom(e => e.Role.Importance))
               .PreserveReferences();
            CreateMap<ServerChannelCategoryRole, HierarchalRoleMinimalDTO>()
               .ForMember(dest => dest.AllowAnyoneToMention, opts => opts.MapFrom(e => e.Role.AllowAnyoneToMention))
               .ForMember(dest => dest.Colour, opts => opts.MapFrom(e => e.Role.Colour))
               .ForMember(dest => dest.DisplaySeperatelyFromOnlineMembers, opts => opts.MapFrom(e => e.Role.DisplaySeperatelyFromOnlineMembers))
               .ForMember(dest => dest.IconURL, opts => opts.MapFrom(e => e.Role.IconURL))
               .ForMember(dest => dest.IsAdmin, opts => opts.MapFrom(e => e.Role.IsAdmin))
               .ForMember(dest => dest.Name, opts => opts.MapFrom(e => e.Role.Name))
               .ForMember(dest => dest.Id, opts => opts.MapFrom(e => e.RoleId))
               .ForMember(dest => dest.Importance, opts => opts.MapFrom(e => e.Role.Importance))
               .PreserveReferences();





            //CreateMap<Server, ServerDTO>().ForMember(e=>e.);

            CreateMap<AccountServerFolder, ServerFolderDTO>(); //output members are same name so no need to map
            CreateMap<AccountBlock, UserMinimalDTO>()
                .ForMember(dest => dest.ImageIconURL, opts => opts.MapFrom(src => src.Blocked.Profile.AvatarFileURL))
                .ForMember(dest => dest.DisplayName, opts => opts.MapFrom(src => src.Blocked.Name))
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.BlockedId))
                .PreserveReferences();

            CreateMap<AccountProfile, UserProfileDTO>()
                .ForMember(dest => dest.BannerColour, opts => opts.MapFrom(src => src.BannerColor))
                .ForMember(dest => dest.AboutMe, opts => opts.MapFrom(src => src.About))
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.AccountId))
                .ForMember(dest => dest.User, opts => opts.MapFrom(src => src.Account))
                .PreserveReferences();
            CreateMap<ServerProfile, UserProfileDTO>()
                .ForMember(dest => dest.BannerColour, opts => opts.MapFrom(src => src.Account.Profile.BannerColor))
                .ForMember(dest => dest.AboutMe, opts => opts.MapFrom(src => src.Account.Profile.About))
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.AccountId))
                .ForMember(dest => dest.User, opts => opts.MapFrom(src => src.Account))
                .PreserveReferences();

            CreateMap<FriendSuggestion, UserDTO>()
                .ForMember(dest => dest.ActiveStatus, opts => opts.Ignore())
                .ForMember(dest => dest.ImageIconURL, opts => opts.MapFrom(e => e.Suggestion.Profile.AvatarFileURL))
                .ForMember(dest => dest.Id, opts => opts.MapFrom(e => e.Suggestion.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(e => e.Suggestion.Name))
                .ForMember(dest => dest.DisplayName, opts => opts.MapFrom(e => e.Suggestion.Profile.DisplayName))
                .AfterMap((src, dest, context) =>
                {
                    dest.ActiveStatus = context.Mapper.Map<ActiveActivityStatusDTO>(src.Suggestion);
                })
                .PreserveReferences();

            CreateMap<FriendshipParticipancy, UserDTO>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Participant.Name))
                .ForMember(dest => dest.ActiveStatus, opts => opts.Ignore())
                .ForMember(dest => dest.DisplayName, opts => opts.MapFrom(src => src.Participant.Profile.DisplayName))
                .ForMember(dest => dest.ImageIconURL, opts => opts.MapFrom(src => src.Participant.Profile.AvatarFileURL))
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.ParticipantId))
                .AfterMap((src, dest, context) =>
                {
                    dest.ActiveStatus = context.Mapper.Map<ActiveActivityStatusDTO>(src.Participant);
                });

                CreateMap<IList<FriendshipParticipancy>, IList<UserDTO>>()
                .ConvertUsing<IFriendshipParticipancyListUserDTOListConverter>();
                /*.PreserveReferences()*/;

            CreateMap<Account, ActiveActivityStatusDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.ActivityStatus.Id))
                .ForMember(dest => dest.Icon, opts => opts.MapFrom(src => src.ActivityStatus.Icon))
                .ForMember(dest => dest.IconColor, opts => opts.MapFrom(src => src.ActivityStatus.IconColor))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.ActivityStatus.Name))
                .ForMember(dest => dest.DisplayedContent, opts => opts.MapFrom(src => src.CustomStatus.CustomMessage))
                .PreserveReferences();


            CreateMap<Account, UserFullDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.ActiveStatus, opts => opts.Ignore())
                //.ForMember(dest => dest.ActiveStatus, opts => opts.MapFrom(src => new ActiveActivityStatusDTO()
                //{
                //    Id = src.ActivityStatusId,
                //    Icon = src.ActivityStatus.Icon,
                //    IconColor = src.ActivityStatus.IconColor,
                //    Name = src.ActivityStatus.Name,
                //    DisplayedContent = src.CustomStatus.CustomMessage //perhaps works fine???? appearantly cant nullcheck here properly so will leave it like this
                //}))
                .ForMember(dest => dest.Servers, opts => opts.MapFrom(e => e.Servers.Where(server => server.FolderId == null)))
                .ForMember(dest => dest.AccessibilitySettings, opts => opts.MapFrom(e => e.Settings.AccessibilitySettings))
                .ForMember(dest => dest.ActivitySettings, opts => opts.MapFrom(e => e.Settings.ActivitySettings))
                .ForMember(dest => dest.AdvancedSettings, opts => opts.MapFrom(e => e.Settings.AdvancedSettings))
                .ForMember(dest => dest.AppearanceSettings, opts => opts.MapFrom(e => e.Settings.AppearanceSettings))
                .ForMember(dest => dest.ChatSettings, opts => opts.MapFrom(e => e.Settings.ChatSettings))
                .ForMember(dest => dest.FriendRequestSettings, opts => opts.MapFrom(e => e.Settings.FriendRequestSettings))
                .ForMember(dest => dest.GameOverlaySettings, opts => opts.MapFrom(e => e.Settings.GameOverlaySettings))
                .ForMember(dest => dest.KeybindSettings, opts => opts.MapFrom(e => e.Settings.KeybindSettings))
                .ForMember(dest => dest.NotificationSettings, opts => opts.MapFrom(e => e.Settings.NotificationSettings))
                .ForMember(dest => dest.PrivacySettings, opts => opts.MapFrom(e => e.Settings.PrivacySettings))
                .ForMember(dest => dest.SoundboardSettings, opts => opts.MapFrom(e => e.Settings.SoundboardSettings))
                .ForMember(dest => dest.StreamerModeSettings, opts => opts.MapFrom(e => e.Settings.StreamerModeSettings))
                .ForMember(dest => dest.VideoSettings, opts => opts.MapFrom(e => e.Settings.VideoSettings))
                .ForMember(dest => dest.VoiceSettings, opts => opts.MapFrom(e => e.Settings.VoiceSettings))
                .ForMember(dest => dest.WindowSettings, opts => opts.MapFrom(e => e.Settings.WindowSettings))
                .ForMember(dest => dest.BillingInformation, opts => opts.MapFrom(e => e.Settings.BillingInformation))
                .ForMember(dest => dest.BlockedUsers, opts => opts.MapFrom(e => e.BlockedAccounts))
                .ForMember(dest => dest.Connections, opts => opts.MapFrom(e => e.Connections))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(e => e.User.Email))
                .ForMember(dest => dest.Friends, opts => opts.MapFrom(acc => acc.Friendships.Select(e => e.Subject).SelectMany(e => e.Participants.Where(e => e.ParticipantId != acc.Id)).ToList())) //aftermap?
                                                                                                                                                                                                     //.ForMember(dest => dest.Friends, opts => opts.MapFrom(src => src.Friendships.Select(f=>f.Participants.Where(p=>p.Id != src.Id)))) //aftermap?
                .ForMember(dest => dest.Language, opts => opts.MapFrom(e => e.Settings.Language))
                .ForMember(dest => dest.PhoneNumber, opts => opts.MapFrom(e => e.User.PhoneNumber))
                .ForMember(dest => dest.Requests, opts => opts.Ignore()) //map aftermap to use mapper to translate request.
                .ForMember(dest => dest.AppPermissions, opts => opts.MapFrom(src => src.Roles.SelectMany(e => e.Role.Permissions).DistinctBy(p => p.Id))) //map aftermap to use mapper to translate request.
                .ForMember(dest => dest.ServerFolders, opts => opts.MapFrom(e => e.Folders))
                .ForMember(dest => dest.Suggestions, opts => opts.MapFrom(e => e.FriendSuggestions))
                .ForMember(dest => dest.UserProfile, opts => opts.MapFrom(e => e.Profile))
                .ForMember(dest => dest.ServerProfiles, opts => opts.MapFrom(e => e.Servers))
                .ForMember(dest => dest.Devices, opts => opts.Ignore())
                .ForMember(dest => dest.DirectMessages, opts => opts.MapFrom(e => e.Chats)) //output members are same name so no need to map
                .AfterMap((src, dest, context) =>
                 {
                     dest.ActiveStatus = context.Mapper.Map<ActiveActivityStatusDTO>(src);
                     var rqlist = new List<FriendRequestDTO>(context.Mapper.Map<List<FriendRequestDTO>>(src.IncomingFriendRequests));
                     rqlist.AddRange(context.Mapper.Map<List<FriendRequestDTO>>(src.OutgoingFriendRequests));
                     dest.Requests = rqlist.OrderBy(e => e.Person.DisplayName).ToList();
                     dest.Devices = context.Mapper.Map<List<DeviceSessionDTO>>(src.Sessions);
                 }).PreserveReferences();
            CreateMap<ServerProfile, ServerDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(e => e.ServerId))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(e => e.Server.Name))
                .ForMember(dest => dest.ImageIconURL, opts => opts.MapFrom(e => e.Server.Settings.ServerImageUrl))
                .ForMember(dest => dest.Muted, opts => opts.MapFrom(e => e.Account.MutedServers.Any(ms => ms.SubjectId == e.ServerId)))
                .ForMember(dest => dest.Members, opts => opts.MapFrom(e => e.Server.Members))
                .ForMember(dest => dest.SoundboardSounds, opts => opts.MapFrom(e => e.Server.SoundboardSounds))
                .ForMember(dest => dest.Emotes, opts => opts.MapFrom(e => e.Server.Emotes))
                .ForMember(dest => dest.BanList, opts => opts.MapFrom(e => e.Server.BanList))
                .ForMember(dest => dest.AuditLogs, opts => opts.MapFrom(e => e.Server.AuditLogs))
                .ForMember(dest => dest.Invites, opts => opts.MapFrom(e => e.Server.Invites))
                .ForMember(dest => dest.Events, opts => opts.MapFrom(e => e.Server.Events))
                .ForMember(dest => dest.Settings, opts => opts.MapFrom(e => e.Server.Settings))
                .ForMember(dest => dest.Categories, opts => opts.MapFrom(e => e.Server.ChannelCategories))
                .ForMember(dest => dest.TextChannels, opts => opts.MapFrom(e => e.Server.TextChannels.Where(c => c.CategoryId == null)))
                .ForMember(dest => dest.VoiceChannels, opts => opts.MapFrom(e => e.Server.VoiceChannels.Where(c => c.CategoryId == null)))
                .ForMember(dest => dest.RoleSettings, opts => opts.MapFrom(e => e.Server.Roles))
                //.ForMember(dest => dest., opts => opts.MapFrom(e => e.Server.Roles))
                .ForMember(dest => dest.MemberSettings, opts => opts.Ignore()) //ignored for now
                .ForMember(dest => dest.UserRoles, opts => opts.MapFrom(e => e.Roles))
                .ForMember(dest => dest.UserPermissions, opts => opts.Ignore()); //ignored for now cause probably easier to loop through permissions on clientside but i am just concerned with outputting unneccessary data
                                                                                 //.ForMember(dest => dest.UserPermissions, opts => opts.MapFrom(e => e.Roles.SelectMany(rolegrant => rolegrant.Role.Permissions).DistinctBy(p => p.PermissionId)));

            //{
            //    targetValue ??= new List<PermissionDTO>();

            //    var valuesA = context.Mapper.Map<IReadOnlyList<DestinationC>>(source.TextChannelMemberPermissions);
            //    var valuesB = context.Mapper.Map<IReadOnlyList<DestinationC>>(source.permiss);

            //    targetValue.AddRange(valuesA);
            //    targetValue.AddRange(valuesB);

            //    return targetValue;
            //})); //output members are same name so no need to map


            CreateMap<ServerVoiceInvite, ServerInviteDTO>()
                .ForMember(dest => dest.Location, opts => opts.MapFrom(e => e.Channel.Name))
                .ForMember(dest => dest.User, opts => opts.MapFrom(e => e.Inviter));
            CreateMap<ServerInvite, ServerInviteDTO>()
                .ForMember(dest => dest.Location, opts => opts.MapFrom(e => e.Channel.Name))
                .ForMember(dest => dest.User, opts => opts.MapFrom(e => e.Inviter));








            CreateMap<ChatParticipancy, MemberDTO>()
                .ForMember(dest => dest.ActiveStatus, opts => opts.Ignore())
               .ForMember(dest => dest.ImageIconURL, opts => opts.MapFrom(src => src.Participant.Profile.AvatarFileURL))
               .ForMember(dest => dest.IsOwner, opts => opts.MapFrom(src => src.IsOwner)) // map aftermap based on chat owner id.
               .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.ParticipantId))
               .ForMember(dest => dest.NameColour, opts => opts.MapFrom(src => src.Participant.ActivityStatus.Name.ToLower() != "offline" ? "#ffffff" : "#000000"))
               .ForMember(dest => dest.GroupingName, opts => opts.MapFrom(src => src.Participant.ActivityStatus.Name.ToLower() != "offline" ? "ONLINE" : "OFFLINE"))
               .ForMember(dest => dest.DisplayName, opts => opts.MapFrom(src => src.Participant.Profile.DisplayName))
               .ForMember(dest => dest.Profile, opts => opts.MapFrom(src => src.Participant))
               .AfterMap((src, dest, context) =>
               {
                   dest.ActiveStatus = context.Mapper.Map<ActiveActivityStatusDTO>(src.Participant);
               }); //map badges, mutual servers, mutualfriends //maybe works maybe ignore at first and load from client.//output members are same name so no need to map

            //CreateMap<ServerProfileServerRole, HierarchalRoleMinimalDTO>().ForAllMembers(dest=>dest.MapFrom(e=>e.Role)); //maybe works? finds inner role and uses that map to get hierarchalrole




            //CreateMap<chat, NotificationMessageDTO>().ForMember(dest=>dest.Id, opts=>opts.MapFrom(e=>)); //output members are same name so no need to map


            CreateMap<ServerProfile, ExternalUserProfileDTO>()
                .ForMember(dest => dest.User, opts => opts.MapFrom(e => e.Account)) //map badges, etc
                .ForMember(dest => dest.Badges, opts => opts.Ignore()) //map badges, etc
                .ForMember(dest => dest.MembershipBadges, opts => opts.Ignore()) //map badges, etc
                .ForMember(dest => dest.BannerColour, opts => opts.MapFrom(e => e.Account.Profile.BannerColor)) //map badges, etc
                .ForMember(dest => dest.AboutMe, opts => opts.MapFrom(e => e.Account.Profile.About)) //map badges, etc
                .ForMember(dest => dest.Roles, opts => opts.MapFrom(e => e.Roles.Select(e => e.Role)))
                .ForMember(dest => dest.MutualFriends, opts => opts.Ignore()) //map friends aftermap from both user ids
                                                                              //.ForMember(dest=>dest.MutualFriends, opts=>opts.MapFrom(e=>e.Roles.Select(e=>e.Role))) //map friends aftermap from both user ids
                .ForMember(dest => dest.MutualServers, opts => opts.Ignore()) //map mutual servers aftermap from both user ids
                                                                              //.ForMember(dest=>dest.MutualServers, opts=>opts.MapFrom(e=>e.Roles.Select(e=>e.Role))) //map mutual servers aftermap from both user ids
                .ForMember(dest => dest.Note, opts => opts.Ignore()) //map notes after mapping from user relation.
                                                                     //.ForMember(dest=>dest.Note, opts=>opts.MapFrom(e=>e.Account.)) //map notes after mapping from user relation.
                .ForMember(dest => dest.Id, opts => opts.MapFrom(e => e.AccountId))
                .AfterMap((src, dest, context) =>
                {
                    dest.MembershipBadges = new List<MembershipBadgeDTO>() //maybe just make entity on serverside
                   {
                        new MembershipBadgeDTO(){ IconName="Echo", IconURL="EchoLogo.png", OrderingWeight=0, TimeJoined=src.Account.TimeCreated   },
                        new MembershipBadgeDTO(){ IconName=src.Server.Name, IconURL=src.Server.Settings.ServerImageUrl, OrderingWeight=1, TimeJoined=src.TimeJoined   }
                   };
                }); //map badges, etc

            CreateMap<ServerProfile, MemberDTO>()
               .ForMember(dest => dest.ActiveStatus, opts => opts.Ignore())
               .ForMember(dest => dest.Id, opts => opts.MapFrom(e => e.AccountId))
               .ForMember(dest => dest.NameColour, opts => opts.MapFrom(e => e.Roles.Select(g => g.Role).OrderByDescending(t => t.Importance).First().Colour))
               .ForMember(dest => dest.GroupingName, opts => opts.MapFrom((src, dest) =>
               {
                   var temp = src.Account.ActivityStatus.Name.ToLower();
                   if (temp == "offline" || temp == "invisible")
                   {
                       return "OFFLINE";
                   }
                   if (src.Roles.Any()) //should always have everyone role though
                   {
                       return src.Roles.Select(g => g.Role).OrderByDescending(t => t.Importance).First().Name;
                   }
                   return "ONLINE";
               }))
               .ForMember(dest => dest.DisplayName, opts => opts.MapFrom(e => e.Nickname))
               .ForMember(dest => dest.Profile, opts => opts.MapFrom(e => e))
               .AfterMap((src, dest, context) =>
               {
                   dest.ActiveStatus = context.Mapper.Map<ActiveActivityStatusDTO>(src.Account);
               }); //map badges, mutual servers, mutualfriends //maybe works

            CreateMap<ServerProfile, UserDTO>()
               .ForMember(dest => dest.ActiveStatus, opts => opts.Ignore())
               .ForMember(dest => dest.Id, opts => opts.MapFrom(e => e.AccountId))
               .ForMember(dest => dest.ImageIconURL, opts => opts.MapFrom(e => e.ImageIconURL))
               .ForMember(dest => dest.Name, opts => opts.MapFrom(e => e.Account.Name))
               .ForMember(dest => dest.DisplayName, opts => opts.MapFrom(e => e.Nickname))
               .AfterMap((src, dest, context) =>
               {
                   dest.ActiveStatus = context.Mapper.Map<ActiveActivityStatusDTO>(src.Account);
               }).PreserveReferences();

            //CreateMap<ChatParticipancy, UserDTO>()
            //  .ForMember(dest => dest.ActiveStatus, opts => opts.MapFrom(e => new ActiveActivityStatusDTO()
            //  {
            //      Id = e.Participant.ActivityStatusId,
            //      Icon = e.Participant.ActivityStatus.Icon,
            //      IconColor = e.Participant.ActivityStatus.IconColor,
            //      Name = e.Participant.ActivityStatus.Name,
            //      DisplayedContent = e.Participant.CustomStatus.CustomMessage //perhaps works fine???? appearantly cant nullcheck here properly so will leave it like this
            //  }))
            //  .ForMember(dest => dest.ImageIconURL, opts => opts.MapFrom(e => e.ImageIconURL))
            //  .ForMember(dest => dest.Name, opts => opts.MapFrom(e => e.Account.Name))
            //  .ForMember(dest => dest.DisplayName, opts => opts.MapFrom(e => e.Nickname));

            CreateMap<Account, ExternalUserProfileDTO>()
               .ForMember(dest => dest.User, opts => opts.MapFrom(e => e)) //map badges, etc
               .ForMember(dest => dest.Badges, opts => opts.Ignore()) //map badges, etc
               .ForMember(dest => dest.MembershipBadges, opts => opts.Ignore()) //map badges, etc
               .ForMember(dest => dest.BannerColour, opts => opts.MapFrom(e => e.Profile.BannerColor)) //map badges, etc
               .ForMember(dest => dest.AboutMe, opts => opts.MapFrom(e => e.Profile.About)) //map badges, etc
               .ForMember(dest => dest.Roles, opts => opts.Ignore())
               .ForMember(dest => dest.MutualFriends, opts => opts.Ignore()) //map friends aftermap from both user ids
                                                                             //.ForMember(dest=>dest.MutualFriends, opts=>opts.MapFrom(e=>e.Roles.Select(e=>e.Role))) //map friends aftermap from both user ids
               .ForMember(dest => dest.MutualServers, opts => opts.Ignore()) //map mutual servers aftermap from both user ids
                                                                             //.ForMember(dest=>dest.MutualServers, opts=>opts.MapFrom(e=>e.Roles.Select(e=>e.Role))) //map mutual servers aftermap from both user ids
               .ForMember(dest => dest.Note, opts => opts.Ignore())
               .AfterMap((src, dest, context) =>
               {
                   dest.MembershipBadges = new List<MembershipBadgeDTO>() //maybe just make entity on serverside
                   {
                        new MembershipBadgeDTO(){ IconName="Echo", IconURL="EchoLogo.png", OrderingWeight=0, TimeJoined=src.TimeCreated   },
                        //new MembershipBadgeDTO(){ IconName=e.Server.Name, IconURL=e.Server.Settings.ServerImageUrl, OrderingWeight=1, TimeJoined=e.TimeJoined   }
                   };
                   //dest.Badges = new List<BadgeDTO>() //maybe just make entity on serverside
                   //{
                   //     new BadgeDTO(){ Description="Echo member", IconURL="EchoLogo.png", OrderingWeight=0  },
                   //     //new MembershipBadgeDTO(){ IconName=e.Server.Name, IconURL=e.Server.Settings.ServerImageUrl, OrderingWeight=1, TimeJoined=e.TimeJoined   }
                   //};
               }).PreserveReferences(); //map notes after mapping from user relation.
                                                                     //.ForMember(dest=>dest.Note, opts=>opts.MapFrom(e=>e.Account.)) //map notes after mapping from user relation.


            CreateMap<Account, UserDTO>()
                .ForMember(dest => dest.ActiveStatus, opts => opts.Ignore())
                .ForMember(dest => dest.ImageIconURL, opts => opts.MapFrom(e => e.Profile.AvatarFileURL))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(e => e.Name))
                .ForMember(dest => dest.DisplayName, opts => opts.MapFrom(e => e.Profile.DisplayName))
                .AfterMap((src, dest, context) =>
                {
                    dest.ActiveStatus = context.Mapper.Map<ActiveActivityStatusDTO>(src);
                }).PreserveReferences();



            CreateMap<ServerChannelCategory, ChannelCategoryDTO>()
                .ForMember(dest => dest.RoleSettings, opts => opts.MapFrom(src => src.AllowedRoles)) //output members are same name so no need to map
                .ForMember(dest => dest.UserPermissions, opts => opts.Ignore()); //output members are same name so no need to map
            CreateMap<ServerWebhook, ServerWebhookDTO>(); //output members are same name so no need to map
            CreateMap<ServerSettings, ServerSettingsDTO>(); //output members are same name so no need to map
        }

    }
}