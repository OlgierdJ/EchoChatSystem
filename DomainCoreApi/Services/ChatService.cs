using AutoMapper;
using CoreLib.DTO.RequestCore.ChatCore;
using CoreLib.DTO.RequestCore.InviteCore;
using CoreLib.DTO.RequestCore.MessageCore;
using CoreLib.DTO.RequestCore.RelationCore;
using CoreLib.DTO.RequestCore.ServerCore;
using CoreLib.DTO.RequestCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ApplicationCore;
using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Bases;
using CoreLib.Interfaces.Services;
using DomainCoreApi.EFCORE;
using DomainCoreApi.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace DomainCoreApi.Services
{
    public class ChatService : IChatService
    {
        private readonly EchoDbContext context;
        private readonly IMapper mapper;
        private readonly IPushNotificationService notificationService;

        public ChatService(EchoDbContext context, IMapper mapper, IPushNotificationService notificationService)
        {
            this.context = context;
            this.mapper = mapper;
            this.notificationService = notificationService;
        }

        public async Task<bool> AddChatParticipant(ulong senderId, ulong chatId, ulong participantId)
        {
            try
            {
                var chat = await context.Set<Chat>()
                    .Include(e => e.Participants).ThenInclude(e=>e.Participant).ThenInclude(e=>e.Friendships).ThenInclude(e=>e.Subject).ThenInclude(e=>e.Participants)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == chatId);

                var senderacc = chat.Participants.FirstOrDefault(e => e.ParticipantId == senderId).Participant; //i know the double check is redundant but whatever
                var isMember = chat.Participants.Any(e => e.ParticipantId == senderId);
                var friendIds = senderacc.Friendships.SelectMany(friendship => friendship.Subject.Participants.Where(e => e.ParticipantId != senderId)).Select(e => e.ParticipantId); //select friendids
                var canAddParticipant = !friendIds.Contains(participantId); //must be friends with all
                if (!isMember || !canAddParticipant)
                {
                    return false;
                }

                var participant = new ChatParticipancy()
                    {
                        ParticipantId = participantId,
                        SubjectId = chatId
                    };

                await context.Set<ChatParticipancy>().AddAsync(participant);

                var res = await context.SaveChangesAsync();
                if (res < 1)
                {
                    return false;
                }

                //should attach participants to chat participants automatically
                var participants = await context.Set<ChatParticipancy>()
                    .Include(e => e.Participant).ThenInclude(e => e.Profile)
                    .AsSplitQuery()
                    .Where(e => e.SubjectId == chatId).ToListAsync();


                var accs = participants.Select(e => e.Participant);

                //need to make roundtrip to get name data
                var chatName = accs.Select(x => x.Name).Aggregate((current, next) => current + ", " + next);
                if (chatName.Length > 100) //if chatname is too long given domain rules
                {
                    chatName = chatName.Substring(0, 97) + "...";
                }

                await context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> RemoveChatParticipant(ulong senderId, ulong chatId, ulong participantId)
        {
            try
            {
                var chat = await context.Set<Chat>()
                    .Include(e => e.Participants)
                    .FirstOrDefaultAsync(e => e.Id == chatId);

                var isMember = chat.Participants.Any(e => e.ParticipantId == senderId); //i know the double check is redundant but whatever
                if (!isMember)
                {
                    return false;
                }

                var participant = new ChatParticipancy()
                {
                    ParticipantId = participantId,
                    SubjectId = chatId
                };

                context.Set<ChatParticipancy>().Remove(participant);

                var res = await context.SaveChangesAsync();

                if (res<1)
                {
                    return false;
                }

                //should attach participants to chat participants automatically
                var participants = await context.Set<ChatParticipancy>()
                    .Include(e => e.Participant).ThenInclude(e => e.Profile)
                    .AsSplitQuery()
                    .Where(e => e.SubjectId == chatId).ToListAsync();


                var accs = participants.Select(e => e.Participant);

                //need to make roundtrip to get name data
                var chatName = accs.Select(x => x.Name).Aggregate((current, next) => current + ", " + next);
                if (chatName.Length > 100) //if chatname is too long given domain rules
                {
                    chatName = chatName.Substring(0, 97) + "...";
                }

                await context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> AddChatParticipants(ulong senderId, ulong chatId, ICollection<ulong> participantIds)
        {
            try
            {
                //if (participantIds.Contains(senderId)) // cant add yourself can you???
                //{
                //    participantIds.Remove(senderId);
                //}
                //ICollection<ulong> partCheckIds = new List<ulong>(participantIds) //crazy way to instantiate list wth
                //{
                //    senderId
                //};
                //var chat = await context.Set<Chat>()
                //    .Include(e => e.Participants.Where(l => partCheckIds.Contains(l.ParticipantId))).ThenInclude(e=>e.Participant).ThenInclude(e=>e.Profile)
                //    .AsSplitQuery()
                //    .FirstOrDefaultAsync(e => e.Id == chatId);

                //var newParticipants = partCheckIds.Where(id => chat.Participants.Any(e => e.ParticipantId == id))
                //    .Select(id => new ChatParticipancy()
                //    {
                //        ParticipantId = id,
                //        SubjectId = chatId
                //    }).ToList(); 

                var chat = await context.Set<Chat>()
                    .Include(e => e.Participants).ThenInclude(e => e.Participant).ThenInclude(e => e.Friendships).ThenInclude(e => e.Subject).ThenInclude(e => e.Participants)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == chatId);

                var senderacc = chat.Participants.FirstOrDefault(e => e.ParticipantId == senderId).Participant; //i know the double check is redundant but whatever
                var isMember = chat.Participants.Any(e => e.ParticipantId == senderId);

                var friendIds = senderacc.Friendships.SelectMany(friendship => friendship.Subject.Participants.Where(e=>e.ParticipantId!=senderId)).Select(e=>e.ParticipantId); //select friendids
                var canAddParticipants = !participantIds.Except(friendIds).Any(); //must be friends with all
                if (!isMember || !canAddParticipants)
                {
                    return false;
                }
                
                var participancies = participantIds
                    .Where(id=>id!=senderId) //filter just in case so it doesnt throw error if they accidentially posted sender in added
                    .Select(id => new ChatParticipancy()
                {
                    ParticipantId = id,
                    SubjectId = chatId
                }).ToList();

                await context.Set<ChatParticipancy>().AddRangeAsync(participancies);

                var res = await context.SaveChangesAsync();

                if (res < 1)
                {
                    return false;
                }

                //should attach participants to chat participants automatically
                var participants = await context.Set<ChatParticipancy>()
                    .Include(e => e.Participant).ThenInclude(e => e.Profile)
                    .AsSplitQuery()
                    .Where(e => e.SubjectId == chatId).ToListAsync();


                var accs = participants.Select(e => e.Participant);

                //need to make roundtrip to get name data
                var chatName = accs.Select(x => x.Name).Aggregate((current, next) => current + ", " + next);
                if (chatName.Length > 100) //if chatname is too long given domain rules
                {
                    chatName = chatName.Substring(0, 97) + "...";
                }

                await context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ConsumeChatInvite(ulong senderId, ulong chatId, string inviteCode)
        {
            try
            {
                //verify sender is member
                var chat = await context.Set<Chat>()
                    .Include(e => e.Participants.Where(t => t.ParticipantId == senderId).Take(1))
                    .Include(e => e.Invites.Where(e=>e.InviteCode == inviteCode).Take(1))
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == chatId);
                //var senderAcc = await context.Set<Account>().Include(e => e.Roles).ThenInclude(e=>e.Permissions).AsSplitQuery().FirstOrDefaultAsync();
                var isMember = chat.Participants.Any(e => e.ParticipantId == senderId); //i know the double check is redundant but whatever
                var invite = chat.Invites.FirstOrDefault(e=>e.InviteCode==inviteCode); //i know the double check is redundant but whatever
                var inviteValid = invite.ExpirationTime > DateTime.Now && invite.TotalUses<invite.TimesUsed && invite.InviterId!=senderId;
                if (invite==null || isMember || !inviteValid)
                {
                    return false;
                }

                ChatParticipancy participancy = new()
                {
                     ParticipantId = senderId,
                      SubjectId = invite.SubjectId,
                    //temp membership is not supported yet
                };
                context.Set<ChatParticipancy>().Add(participancy);

                var res = await context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CreateChat(ulong senderId, ICollection<ulong> startParticipants)
        {
            using var transaction = await context.Database.BeginTransactionAsync(); //need transaction cause multiple database transaction steps and possibility of rollback
            try
            {
                if (!startParticipants.Contains(senderId))
                {
                    startParticipants.Add(senderId);
                }
                //verify sender is member
                var accs = await context.Set<Account>()
                    .Include(a=>a.Profile)
                    .AsSplitQuery()
                    .Where(l => startParticipants.Contains(l.Id))
                    .AsNoTracking().ToListAsync();

                var chatName = accs.Select(x => x.Name).Aggregate((current, next) => current + ", " + next);
                if (chatName.Length > 100) //if chatname is too long given domain rules
                {
                    chatName = chatName.Substring(0, 97)+"..."; 
                }

                Chat newChat = new()
                {
                    Name = chatName,
                    Pinboard = new(),
                    Participants = new List<ChatParticipancy>()
                    
                };

                context.Set<Chat>().Add(newChat);

                await context.SaveChangesAsync();

                context.Attach<Chat>(newChat);

                var participants = accs.Select(e => new ChatParticipancy()
                {
                    IsOwner = e.Id == senderId,
                    ParticipantId = e.Id,
                    SubjectId = newChat.Id
                }).ToList();

                foreach (var participant in participants)
                {
                    //context.Set<ChatParticipancy>().Add(participant);
                    newChat.Participants.Add(participant);
                }

                await context.SaveChangesAsync();
                await transaction.CommitAsync();
                //res = await context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                return false;
            }
            return true;
        }

        public async Task<bool> CreateChatInvite(ulong senderId, ulong chatId, CreateInviteRequestDTO requestDTO)
        {
            try
            {
                //verify sender is member
                var chat = await context.Set<Chat>()
                    .Include(e => e.Participants.Where(t => t.ParticipantId == senderId).Take(1))
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == chatId);
                //var senderAcc = await context.Set<Account>().Include(e => e.Roles).ThenInclude(e=>e.Permissions).AsSplitQuery().FirstOrDefaultAsync();
                var isMember = chat.Participants.Any(e => e.ParticipantId == senderId);
                if (!isMember)
                {
                    return false;
                }

                ChatInvite inv = new()
                {
                    ExpirationTime = requestDTO.TimeExpires,
                    InviterId = senderId,
                    SubjectId = chatId,
                    TotalUses = requestDTO.TotalUses,
                    //temp membership is not supported yet
                };
                context.Set<ChatInvite>().Add(inv);

                var res = await context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteChat(ulong senderId, ulong chatId)
        {
            try
            {
                //verify sender is member && owner
                var chat = await context.Set<Chat>()
                    .Include(e => e.Participants.Where(t => t.ParticipantId == senderId).Take(1))
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == chatId);
                var isAllowed = chat.Participants.Any(e => e.ParticipantId == senderId && e.IsOwner==true);
                if (!isAllowed)
                {
                    return false;
                }
                context.Set<Chat>().Remove(chat);
                var res = await context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> HideChat(ulong senderId, ulong chatId)
        {
            try
            {
                //verify sender is member
                var participancy = await context.Set<ChatParticipancy>()
                    //.Include(e => e.Messages.OrderByDescending(m => m.TimeSent).Take(1))
                    //.Include(e => e.MessageTrackers.Where(t => t.OwnerId == senderId).Take(1))
                    //.Include(e => e.Participants.Where(t => t.ParticipantId == senderId).Take(1))
                    //.Include(e => e.Invites.Where(t => t.InviterId == senderId))
                    //.Include(e => e.Mutes.Where(t => t.MuterId == senderId))
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.ParticipantId == senderId && e.SubjectId==chatId);
                //var senderAcc = await context.Set<Account>().Include(e => e.Roles).ThenInclude(e=>e.Permissions).AsSplitQuery().FirstOrDefaultAsync();
                var isMember = participancy!=null;
                if (!isMember)
                {
                    return false;
                }

                participancy.Hidden = true;
                var res = await context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> LeaveChat(ulong senderId, ulong chatId)
        {
            try
            {
                //verify sender is member
                var chat = await context.Set<Chat>()
                    //.Include(e => e.Messages.OrderByDescending(m => m.TimeSent).Take(1))
                    .Include(e => e.MessageTrackers.Where(t => t.OwnerId == senderId).Take(1))
                    .Include(e => e.Participants)
                    .Include(e => e.Invites.Where(t => t.InviterId == senderId).Take(1))
                    .Include(e => e.Mutes.Where(t => t.MuterId == senderId).Take(1))
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == chatId);
                //var senderAcc = await context.Set<Account>().Include(e => e.Roles).ThenInclude(e=>e.Permissions).AsSplitQuery().FirstOrDefaultAsync();
                var isMember = chat.Participants.Any(e => e.ParticipantId == senderId);
                if (!isMember)
                {
                    return false;
                }
                var newOwner = chat.Participants.OrderBy(e => e.TimeJoined).FirstOrDefault();
                if (newOwner == null) //then chat is orphan and should be removed
                {
                    context.Set<Chat>().Remove(chat);
                }
                newOwner.IsOwner = true;
                context.Set<ChatAccountMessageTracker>().RemoveRange(chat.MessageTrackers);
                //context.Set<ChatInvite>().RemoveRange(chat.Invites); //maybe wait with this one
                context.Set<ChatMute>().RemoveRange(chat.Mutes);
                context.Set<ChatParticipancy>().RemoveRange(chat.Participants.Where(e=>e.ParticipantId==senderId).Take(1));
                var res = await context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> MarkChatAsRead(ulong senderId, ulong chatId)
        {
            try
            {
                //verify sender is member
                var chat = await context.Set<Chat>()
                    .Include(e => e.Messages.OrderByDescending(m=>m.TimeSent).Take(1))
                    .Include(e=>e.MessageTrackers.Where(t=>t.OwnerId == senderId).Take(1))
                    .Include(e=>e.Participants.Where(t=>t.ParticipantId == senderId).Take(1))
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == chatId);
                //var senderAcc = await context.Set<Account>().Include(e => e.Roles).ThenInclude(e=>e.Permissions).AsSplitQuery().FirstOrDefaultAsync();
                var isMember = chat.Participants.Any(e => e.ParticipantId == senderId);
                if (!isMember)
                {
                    return false;
                }
                var tracker = chat.MessageTrackers.FirstOrDefault(e=>e.OwnerId == senderId);
                var newestMsg = chat.Messages.First();
                tracker.SubjectId = newestMsg.Id;
                var res = await context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> MuteChat(ulong senderId, ulong chatId, MuteRequestDTO requestDTO)
        {
            try
            {
                var isMember = context.Set<ChatParticipancy>().Any(o => o.ParticipantId == senderId && o.SubjectId == chatId);
                //var senderAcc = await context.Set<Account>().Include(e => e.Roles).ThenInclude(e=>e.Permissions).AsSplitQuery().FirstOrDefaultAsync();
                if (!isMember)
                {
                    return false;
                }
                Account senderAccount = new()
                {
                    Id = senderId,
                    MutedChats = new List<ChatMute>()
                };
                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                ChatMute relation = new()
                {
                    MuterId = senderId,
                    SubjectId = chatId,
                };

                context.Set<Account>().Attach(senderAccount); //throws error if already blocked fyi
                senderAccount.MutedChats.Add(relation);
                var res = await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> PinChatMessage(ulong senderId, ulong chatId, ulong messageId)
        {
            try
            {
                //verify sender is member
                var isMember = context.Set<ChatParticipancy>().Any(o => o.ParticipantId == senderId && o.SubjectId == chatId);
                //var senderAcc = await context.Set<Account>().Include(e => e.Roles).ThenInclude(e=>e.Permissions).AsSplitQuery().FirstOrDefaultAsync();
                if (!isMember)
                {
                    return false;
                }
                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                ChatMessagePin relation = new()
                {
                    PinboardId = chatId,
                    MessageId = messageId,
                };
                ChatMessage sysmsg = new()
                {
                     Content = "@" +senderId.ToString() + " pinned https://echo.chat/"+chatId.ToString()+"/"+messageId.ToString()+" to this chat.",
                      MessageHolderId = chatId,
                };
                context.Set<ChatMessagePin>().Add(relation);
                context.Set<ChatMessage>().Add(sysmsg);

                var res = await context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> RemoveChatMessage(ulong senderId, ulong chatId, ulong messageId)
        {
            try
            {
                //verify sender is member //doesnt matter here
                var chat = await context.Set<Chat>().Include(e => e.Participants)
                    //.Include(e=>e.MessageTrackers) //ignore tracker logic for now cause seems quite complex and doesnt really do anything
                    .Include(e =>e.Messages.Where(msg=> msg.Id==messageId && msg.AuthorId==senderId))
                    //.Include(e=>e.Messages.OrderByDescending(msg=>msg.TimeSent))
                    .Include(e => e.Participants)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == chatId);
                var isMember = chat.Participants.Any(e=>e.ParticipantId==senderId);
                //var senderAcc = await context.Set<Account>().Include(e => e.Roles).ThenInclude(e=>e.Permissions).AsSplitQuery().FirstOrDefaultAsync();
                if (!isMember)
                {
                    return false;
                }

                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                var msg = chat.Messages.FirstOrDefault(e => e.Id == messageId);
                if (msg != null) //if null then either the message doesnt exist or the author is wrong therefore they are not allowed
                {
                    return false;
                }
                chat.Messages.Remove(msg);

                var res = await context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> SendChatMessage(ulong senderId, ulong chatId, SendMessageRequestDTO requestDTO)
        {
            try
            {
                //verify sender is member
                var chat = await context.Set<Chat>()
                    .Include(e => e.Participants)
                    .Include(e=>e.Messages.Take(0))
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e=>e.Id == chatId);
                //var senderAcc = await context.Set<Account>().Include(e => e.Roles).ThenInclude(e=>e.Permissions).AsSplitQuery().FirstOrDefaultAsync();
                var isMember = chat.Participants.Any(e => e.ParticipantId == senderId);
                if (!isMember)
                {
                    return false;
                }
                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                ChatMessage msg = new()
                {
                     AuthorId = senderId,
                      Content = requestDTO.Content,
                       //MessageHolderId = chatId,
                         ParentId=requestDTO.ReplyId,
                     Attachments = requestDTO.Attachments.Select(e=>new ChatMessageAttachment
                     {
                          Description = e.Description,
                           FileLocationURL = e.FileLocationURL,
                            FileName = e.FileName,
                     }).ToList(),
                };
                chat.Messages.Add(msg);

                var res = await context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> SetChatImage(ulong senderId, ulong chatId, SetImageRequestDTO requestDTO)
        {
            try
            {
                //verify sender is member
                var chat = await context.Set<Chat>()
                    .Include(e => e.Participants)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == chatId);
                //var senderAcc = await context.Set<Account>().Include(e => e.Roles).ThenInclude(e=>e.Permissions).AsSplitQuery().FirstOrDefaultAsync();
                var isMember = chat.Participants.Any(e => e.ParticipantId == senderId);
                if (!isMember)
                {
                    return false;
                }
                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                chat.IconUrl = requestDTO.ImageFileURL;

                var res = await context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UnmuteChat(ulong senderId, ulong chatId)
        {
            try
            {
                //verify sender is member //doesnt matter here
                var isMember = context.Set<ChatParticipancy>().Any(o => o.ParticipantId == senderId && o.SubjectId == chatId);
                //var senderAcc = await context.Set<Account>().Include(e => e.Roles).ThenInclude(e=>e.Permissions).AsSplitQuery().FirstOrDefaultAsync();
                if (!isMember)
                {
                    return false;
                }
                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                ChatMute relation = new()
                {
                    SubjectId = chatId,
                    MuterId = senderId,
                };
                context.Set<ChatMute>().Remove(relation);

                var res = await context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UnpinChatMessage(ulong senderId, ulong chatId, ulong messageId)
        {
            try
            {
                //verify sender is member
                var isMember = context.Set<ChatParticipancy>().Any(o => o.ParticipantId == senderId && o.SubjectId == chatId);
                //var senderAcc = await context.Set<Account>().Include(e => e.Roles).ThenInclude(e=>e.Permissions).AsSplitQuery().FirstOrDefaultAsync();
                if (!isMember)
                {
                    return false;
                }
                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                ChatMessagePin relation = new()
                {
                    PinboardId = chatId,
                    MessageId = messageId,
                };

                context.Set<ChatMessagePin>().Remove(relation);

                var res = await context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateChat(ulong senderId, ulong chatId, UpdateChatRequestDTO requestDTO)
        {
            try
            {
                //verify sender is member
                var chat = await context.Set<Chat>()
                    .Include(e => e.Participants)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == chatId);
                //var senderAcc = await context.Set<Account>().Include(e => e.Roles).ThenInclude(e=>e.Permissions).AsSplitQuery().FirstOrDefaultAsync();
                var isMember = chat.Participants.Any(e => e.ParticipantId == senderId);
                if (!isMember)
                {
                    return false;
                }
                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                chat.Name = requestDTO.Name;

                var res = await context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateChatMessage(ulong senderId, ulong chatId, ulong messageId, EditMessageRequestDTO requestDTO)
        {
            try
            {
                //verify sender is member
                var chat = await context.Set<Chat>()
                    .Include(e => e.Participants)
                    .Include(e=>e.Messages.Where(e=>e.Id == messageId))
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == chatId);
                //var senderAcc = await context.Set<Account>().Include(e => e.Roles).ThenInclude(e=>e.Permissions).AsSplitQuery().FirstOrDefaultAsync();
                if (chat == null)
                {
                    return false;
                }
                var isMember = chat.Participants.Any(e => e.ParticipantId == senderId);
                if (!isMember)
                {
                    return false;
                }
                var msg = chat.Messages.FirstOrDefault(e => e.Id == messageId);
                if (msg == null || msg.AuthorId != senderId)
                {
                    return false;
                }
                //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
                msg.Content = requestDTO.Content;
                msg.TimeEdited = DateTime.Now;

                var res = await context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}