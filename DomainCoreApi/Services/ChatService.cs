using AutoMapper;
using Azure.Core;
using CoreLib.DTO.EchoCore.ChatCore.TextCore;
using CoreLib.DTO.RequestCore.ChatCore;
using CoreLib.DTO.RequestCore.InviteCore;
using CoreLib.DTO.RequestCore.MessageCore;
using CoreLib.DTO.RequestCore.RelationCore;
using CoreLib.DTO.RequestCore.ServerCore;
using CoreLib.DTO.RequestCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ApplicationCore;
using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Entities.EchoCore.FriendCore;
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
        //private readonly IPushNotificationService notificationService;

        public ChatService(EchoDbContext context, IMapper mapper /*IPushNotificationService notificationService*/)
        {
            this.context = context;
            this.mapper = mapper;
            //this.notificationService = notificationService;
        }
        /// <summary>
        /// attempts to add a user as a participant to a chat (must be friends)
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="chatId"></param>
        /// <param name="participantId"></param>
        /// <returns></returns>
        public async Task<bool> AddChatParticipant(ulong senderId, ulong chatId, ulong participantId)
        {
            try
            {
                if (senderId == participantId) //dont do anything in db if invalid call
                {
                    return false;
                }
                //find sender and receiver and include chat if present
                var chat = await context.Set<Chat>()
                    .Include(e => e.Participants).ThenInclude(e => e.Participant).ThenInclude(e => e.Profile)
                    .Include(e => e.Messages.OrderByDescending(e => e.TimeSent).Take(1))
                    .Include(e => e.DirectMessageRelation)
                    .FirstOrDefaultAsync(e => e.Id == chatId); //need chat participancies to find out if sender is part of chat and if participant is already part of chat
                if (chat == null)
                {
                    return false;
                }
                if (chat.DirectMessageRelation != null)
                {
                    //cant add participants to dm therefore create new chat
                    return await CreateChat(senderId, new List<ulong>(chat.Participants.Select(e => e.ParticipantId)) { participantId }); //probably dont need to clear cached context.
                }
                var accs = await context.Set<Account>()
                    .Include(e => e.Friendships).ThenInclude(e => e.Subject).ThenInclude(e => e.Participants.Where(e => e.ParticipantId == participantId || e.ParticipantId == senderId))
                    .Include(e => e.Chats.Where(e => e.SubjectId == chatId)) //need chat participancies to find out if sender is part of chat and if participant is already part of chat
                    .Include(e => e.Profile)
                    .Where(e => e.Id == senderId || e.Id == participantId)
                    .AsSplitQuery()
                    .ToListAsync();

                //get sender and receiver for processing
                var senderacc = accs.FirstOrDefault(e => e.Id == senderId);
                var receiveracc = accs.FirstOrDefault(e => e.Id == participantId);

                //check if sender is part of chat otherwise just ignore call and go back
                var addedIsMemberAlready = receiveracc.Chats.Any(); //just check if any participancies has been included cause we've filtered by chatid
                var isMember = senderacc.Chats.Any(); //just check if any participancies has been included cause we've filtered by chatid
                if (!isMember || addedIsMemberAlready)
                {
                    return false;
                }

                //check if participant can be added by sender
                var relatedFriendship = context.Set<FriendshipParticipancy>().Local.GroupBy(e => e.SubjectId).FirstOrDefault(e => e.Count() > 1); //group by subject cause we've only included friendship participancies into the context where id is senderid or participantid

                var canAddParticipant = relatedFriendship != null; //must be friends with all
                if (!canAddParticipant)
                {
                    return false;
                }
                var tracker = new ChatAccountMessageTracker()
                {
                    OwnerId = participantId,
                    CoOwnerId = chatId,
                    SubjectId = chat.Messages.FirstOrDefault()?.Id
                };

                var participant = new ChatParticipancy()
                {
                    ParticipantId = participantId,
                    SubjectId = chatId
                };

                await context.Set<ChatAccountMessageTracker>().AddAsync(tracker); // add new tracker since messages are "read" when added to chat.
                await context.Set<ChatParticipancy>().AddAsync(participant);

                await context.SaveChangesAsync();


                //need to make roundtrip to get name data
                //should attach participants to chat participants automatically
                //var participancies = await context.Set<ChatParticipancy>()
                //    .Include(e => e.Participant).ThenInclude(e => e.Profile)
                //    .Include(e=>e.Subject)
                //    .AsSplitQuery()
                //    .Where(e => e.SubjectId == chatId).ToListAsync();


                //var participants = participancies.Select(e => e.Participant);
                var participants = chat.Participants.Select(e => e.Participant);

                //generate default naming for previous added participants and check if chat is named that
                var prevChatName = participants.Where(e => e.Id != participantId).Select(x => x.Name).Aggregate((current, next) => current + ", " + next);
                if (prevChatName.Length > 100) //if chatname is too long given domain rules
                {
                    prevChatName = prevChatName.Substring(0, 97) + "...";
                }
                if (chat.Name != prevChatName) //if name is not the same then it has been set manually and should be kept
                {
                    return true;
                }
                //if chat is named prev generated name, generate new name and assign it.
                var chatName = participants.Select(x => x.Name).Aggregate((current, next) => current + ", " + next);
                if (chatName.Length > 100) //if chatname is too long given domain rules
                {
                    chatName = chatName.Substring(0, 97) + "...";
                }
                chat.Name = chatName;
                await context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// attempts to remove a user participant from a chat.
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="chatId"></param>
        /// <param name="participantId"></param>
        /// <returns></returns>
        public async Task<bool> RemoveChatParticipant(ulong senderId, ulong chatId, ulong participantId)
        {
            try
            {
                var chat = await context.Set<Chat>()
                    .Include(e => e.MessageTrackers.Where(e => e.OwnerId == participantId).Take(1))
                    .Include(e => e.Muters.Where(e => e.MuterId == participantId).Take(1))
                    .Include(e => e.Participants).ThenInclude(e => e.Participant).ThenInclude(e => e.Profile)
                    .Include(e => e.Participants).ThenInclude(e => e.Participant).ThenInclude(e => e.NicknamedAccounts.Where(e => e.AuthorId == senderId)) //perhaps use nickname in future or displayname or something.
                    .Include(e => e.DirectMessageRelation)
                    .FirstOrDefaultAsync(e => e.Id == chatId);
                if (chat == null || chat.DirectMessageRelation != null)
                {
                    //cant process empty chat or remove participant from dm chat.
                    return false;
                }

                var sender = chat.Participants.FirstOrDefault(e => e.ParticipantId == senderId);
                var participant = chat.Participants.FirstOrDefault(e => e.ParticipantId == participantId);
                var isMember = senderId != null; //i know the double check is redundant but whatever
                var participantIsMember = participant != null; //i know the double check is redundant but whatever
                if (!isMember || !participantIsMember)
                {
                    return false;
                }
                var canRemove = sender.IsOwner && !participant.IsOwner; //can only remove if higher permission status than participant.
                if (!canRemove)
                {
                    return false;
                }

                //await context.SaveChangesAsync(); //actually dont need to make a roundtrip before changing name since participant is loaded into context

                var participantAccs = chat.Participants.Select(e => e.Participant);

                //generate default naming for previous added participants and check if chat is named that
                var prevChatName = participantAccs.Select(x => x.Name).Aggregate((current, next) => current + ", " + next);
                if (prevChatName.Length > 100) //if chatname is too long given domain rules
                {
                    prevChatName = prevChatName.Substring(0, 97) + "...";
                }
                //if (chat.Name != prevChatName) //if name is not the same then it has been set manually and should be kept
                //{
                //    return true;
                //}
                //if chat is named prev generated name, generate new name and assign it.
                var chatName = participantAccs.Where(e => e.Id != participantId).Select(x => x.Name).Aggregate((current, next) => current + ", " + next);
                if (chatName.Length > 100) //if chatname is too long given domain rules
                {
                    chatName = chatName.Substring(0, 97) + "...";
                }
                chat.Name = chat.Name != prevChatName ? chat.Name : chatName;

                context.Set<ChatMute>().Remove(chat.Muters.FirstOrDefault()); //removing the relation can be done in one fell swoop.
                context.Set<ChatAccountMessageTracker>().Remove(chat.MessageTrackers.FirstOrDefault()); //removing the relation can be done in one fell swoop.
                context.Set<ChatParticipancy>().Remove(participant); //removing the relation can be done in one fell swoop.

                await context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// filters existing participants and tries to add rest at once (must be friends)
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="chatId"></param>
        /// <param name="participantIds"></param>
        /// <returns></returns>
        public async Task<bool> AddChatParticipants(ulong senderId, ulong chatId, ICollection<ulong> participantIds)
        {
            try
            {
                var lookupIds = new List<ulong>(participantIds) { senderId }; //accounts to pull into context.
                //find sender and receivers and include chat if present
                var chat = await context.Set<Chat>()
                    .Include(e => e.Participants).ThenInclude(e => e.Participant).ThenInclude(e => e.Profile)
                    .Include(e => e.Messages.OrderByDescending(e => e.TimeSent).Take(1))
                    .Include(e => e.DirectMessageRelation)
                    .FirstOrDefaultAsync(e => e.Id == chatId); //need chat participancies to find out if sender is part of chat and if participant is already part of chat
                if (chat == null)
                {
                    return false;
                }
                if (chat.DirectMessageRelation != null)
                {
                    //cant add participants to dm therefore create new chat
                    return await CreateChat(senderId, participantIds); //probably dont need to clear cached context.
                }
                var accs = await context.Set<Account>()
                    .Include(e => e.Friendships).ThenInclude(e => e.Subject)
                    .ThenInclude(e => e.Participants.Where(e => participantIds.Contains(e.ParticipantId))) //filter away non context participants
                    .Include(e => e.Chats.Where(e => e.SubjectId == chatId))
                    .Include(e => e.Profile)
                    .Where(e => lookupIds.Contains(e.Id))
                    .AsSplitQuery()
                    .ToListAsync();

                //get sender and receivers for processing
                var senderacc = accs.FirstOrDefault(e => e.Id == senderId);
                //find accounts to be added
                var receiverAccs = accs.Where(e => !e.Chats.Any()); //if chats are empty then they need to be added else just ignore them.
                var noMembersToBeAdded = !receiverAccs.Any(); //if there are no members to add from participantids

                //check if sender is part of chat otherwise just ignore call and go back
                var isMember = senderacc.Chats.Any(); //just check if any participancies has been included cause we've filtered by chatid
                if (!isMember || noMembersToBeAdded)
                {
                    return false;
                }

                var friendIds = senderacc.Friendships.Where(fp => fp.Subject.Participants.Count > 1) //loop through sender friends, find friendships where has more than 1 participant loaded in context.
                    .SelectMany(e => e.Subject.Participants.Where(e => e.ParticipantId != senderId)) //get other end of friendship, then flatten
                    .Select(e => e.ParticipantId); //get ids

                var canAddParticipants = !receiverAccs.Select(e => e.Id).Except(friendIds).Any(); //check if receiver ids has ids which are not in friendids
                if (!canAddParticipants)
                {
                    return false;
                }

                var newParticipancies = receiverAccs.Select(e => e.Id)
                    //take filtered receiverlist and try to add them
                    .Select(id => new ChatParticipancy()
                    {
                        ParticipantId = id,
                        SubjectId = chatId
                    }).ToList();

                var newTrackers = newParticipancies.Select(e => new ChatAccountMessageTracker()
                {
                    OwnerId = e.ParticipantId,
                    CoOwnerId = chatId,
                    SubjectId = chat.Messages.FirstOrDefault()?.Id
                });

                var prevParticipants = chat.Participants.Select(e => e.Participant).ToList(); //save local list of prev pointers

                await context.Set<ChatAccountMessageTracker>().AddRangeAsync(newTrackers);
                await context.Set<ChatParticipancy>().AddRangeAsync(newParticipancies);

                await context.SaveChangesAsync();

                //need to make roundtrip to get name data
                //should attach participants to chat participants automatically
                //var participancies = await context.Set<ChatParticipancy>()
                //    .Include(e => e.Participant).ThenInclude(e => e.Profile)
                //    .Include(e => e.Subject)
                //    .AsSplitQuery()
                //    .Where(e => e.SubjectId == chatId).ToListAsync();



                //generate default naming for previous added participants and check if chat is named that
                var prevChatName = prevParticipants.Select(x => x.Name).Aggregate((current, next) => current + ", " + next);
                if (prevChatName.Length > 100) //if chatname is too long given domain rules
                {
                    prevChatName = prevChatName.Substring(0, 97) + "...";
                }
                if (chat.Name != prevChatName) //if name is not the same then it has been set manually and should be kept
                {
                    return true;
                }

                var participants = prevParticipants.Concat(newParticipancies.Select(e => e.Participant));
                //if chat is named prev generated name, generate new name and assign it.
                var chatName = participants.Select(x => x.Name).Aggregate((current, next) => current + ", " + next);
                if (chatName.Length > 100) //if chatname is too long given domain rules
                {
                    chatName = chatName.Substring(0, 97) + "...";
                }
                chat.Name = chatName;

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
                    .Include(e => e.Invites.Where(e => e.InviteCode == inviteCode).Take(1))
                    .Include(e => e.Messages.OrderByDescending(e => e.TimeSent).Take(1))
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == chatId);
                if (chat == null || !chat.Invites.Any())
                {
                    //cant process nonexistant chat or nonexistant invite within chat. therefore go away plebian
                    return false;
                }
                var invite = chat.Invites.FirstOrDefault(); //already filtered from loading
                var isMember = chat.Participants.Any(); //already filtered from loading.
                //the invite has to be not expired, and the inviter cant use their own invite.
                //also if the totaluses is not 0 which means unlimited then the validation should also take times used in regard.
                var notExpired = invite.ExpirationTime > DateTime.Now;
                var notIssuedBySender = invite.InviterId != senderId;
                var canBeUsedStill = invite.TotalUses == 0 ? true : invite.TotalUses > invite.TimesUsed;
                var inviteValid = notExpired && notIssuedBySender && canBeUsedStill;
                if (isMember || !inviteValid)
                {
                    return false;
                }

                ChatParticipancy participancy = new()
                {
                    ParticipantId = senderId,
                    SubjectId = invite.SubjectId,
                    //temp membership is not supported yet
                };
                ChatAccountMessageTracker tracker = new()
                {
                    OwnerId = senderId,
                    CoOwnerId = invite.SubjectId,
                    SubjectId = chat.Messages.FirstOrDefault()?.Id
                };
                context.Set<ChatParticipancy>().Add(participancy);
                context.Set<ChatAccountMessageTracker>().Add(tracker);

                var res = await context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// attempts to create a new chat from a group of participants (must be friends).
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="startParticipants"></param>
        /// <returns></returns>
        public async Task<bool> CreateChat(ulong senderId, ICollection<ulong> startParticipants)
        {
            using var transaction = await context.Database.BeginTransactionAsync(); //need transaction cause multiple database transaction steps and possibility of rollback
            try
            {
                var lookupIds = new List<ulong>(startParticipants) { senderId }; //accounts to pull into context.
                //find sender and receivers and include chat if present
                var accs = await context.Set<Account>()
                    //blocked relations are appearently useless for determining participation in chat or voice and such -> see reference in accountblock entity.
                    //.Include(e => e.BlockedAccounts.Where(e=>lookupIds.Contains(e.BlockedId))) 
                    .Include(e => e.Friendships).ThenInclude(e => e.Subject)
                    .ThenInclude(e => e.Participants.Where(e => lookupIds.Contains(e.ParticipantId))) //filter away non context participants
                    .Where(e => lookupIds.Contains(e.Id))
                    .AsSplitQuery()
                    .ToListAsync();



                //get sender and receivers for processing
                var senderacc = accs.FirstOrDefault(e => e.Id == senderId);
                //find accounts to be added
                var receiverAccs = accs.Where(e => e.Id != senderId); //if chats are empty then they need to be added else just ignore them.
                var noMembersToBeAdded = !receiverAccs.Any(); //if there are no members to add from participantids
                if (noMembersToBeAdded)
                {
                    return false;
                }
                var friendIds = senderacc.Friendships.Where(fp => fp.Subject.Participants.Count > 1) //loop through sender friends, find friendships where has more than 1 participant loaded in context.
                    .SelectMany(e => e.Subject.Participants.Where(e => e.ParticipantId != senderId)) //get other end of friendship, then flatten
                    .Select(e => e.ParticipantId); //get ids

                var canAddParticipants = !receiverAccs.Select(e => e.Id).Except(friendIds).Any(); //check if receiver ids has ids which are not in friendids
                if (!canAddParticipants)
                {
                    return false;
                }

                var chatName = accs.Select(x => x.Name).Aggregate((current, next) => current + ", " + next);
                if (chatName.Length > 100) //if chatname is too long given domain rules
                {
                    chatName = chatName.Substring(0, 97) + "...";
                }

                Chat newChat = new()
                {
                    Name = chatName,
                    //Pinboard = new(),
                    Participants = new List<ChatParticipancy>(),
                    MessageTrackers = new List<ChatAccountMessageTracker>()

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

                var trackers = participants.Select(e => new ChatAccountMessageTracker()
                {
                    OwnerId = e.ParticipantId,
                    CoOwnerId = e.SubjectId,
                    SubjectId = null
                });
                foreach (var tracker in trackers)
                {
                    //context.Set<ChatParticipancy>().Add(participant);
                    newChat.MessageTrackers.Add(tracker);
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
                if (chat == null)
                {
                    return false;
                }
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
        /// <summary>
        /// attempts to delete a chat (sender must be part of chat and have ownership and the chat must not be a direct message chat.)
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteChat(ulong senderId, ulong chatId)
        {
            try
            {
                //verify sender is member && owner
                var chat = await context.Set<Chat>()
                    .Include(e => e.Participants.Where(t => t.ParticipantId == senderId).Take(1)) //filter from db
                    .Include(e => e.DirectMessageRelation)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == chatId);
                if (chat == null || chat.DirectMessageRelation != null)
                {
                    //cant process empty chat or delete dm chat.
                    return false;
                }
                //sender must be part of chat and owner aswell.
                var isAllowed = chat.Participants.Any(e => e.IsOwner == true); //if any then sender is here since already filtered, only need to check if owner
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

        /// <summary>
        /// tries to hide a chat from the user effectively not showing the chat.
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="chatId"></param>
        /// <returns></returns>
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
                    .FirstOrDefaultAsync(e => e.ParticipantId == senderId && e.SubjectId == chatId);
                //var senderAcc = await context.Set<Account>().Include(e => e.Roles).ThenInclude(e=>e.Permissions).AsSplitQuery().FirstOrDefaultAsync();
                var isMember = participancy != null;
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
                    .Include(e => e.Muters.Where(t => t.MuterId == senderId).Take(1))
                    .Include(e => e.Participants) // need all participants here to make someone owner afterwards if possible
                    .Include(e => e.DirectMessageRelation)
                    .Include(e => e.Invites.Where(t => t.InviterId == senderId))
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == chatId);
                if (chat == null || chat.DirectMessageRelation != null)
                {
                    //cant process empty chat or leave dm chat.
                    return false;
                }
                //var senderAcc = await context.Set<Account>().Include(e => e.Roles).ThenInclude(e=>e.Permissions).AsSplitQuery().FirstOrDefaultAsync();
                var sender = chat.Participants.FirstOrDefault(e => e.ParticipantId == senderId);
                var isMember = sender!=null;
                if (!isMember)
                {
                    return false;
                }
                if (sender.IsOwner)
                {
                    //find participants other than sender if any and order them by join order (last in first out).
                    var newOwner = chat.Participants.Where(e => e.ParticipantId != senderId).OrderBy(e => e.TimeJoined).FirstOrDefault();
                    if (newOwner == null) //then chat is orphan and should be removed
                    {
                        context.Set<Chat>().Remove(chat);
                    }
                    newOwner.IsOwner = true;
                }
                //cleanup relations for sender account. (wait with invite cause we dont know how to invalidate it yet.)
                context.Set<ChatAccountMessageTracker>().RemoveRange(chat.MessageTrackers);
                //context.Set<ChatInvite>().RemoveRange(chat.Invites); //maybe wait with this one
                context.Set<ChatMute>().RemoveRange(chat.Muters);
                context.Set<ChatParticipancy>().Remove(sender);
                await context.SaveChangesAsync();

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
                    .Include(e => e.Messages.OrderByDescending(m => m.TimeSent).Take(1)) //get newest message
                    .Include(e => e.MessageTrackers.Where(t => t.OwnerId == senderId).Take(1)) //get users tracking relation
                    .Include(e => e.Participants.Where(t => t.ParticipantId == senderId).Take(1)) //get users participation
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == chatId);
                if (chat == null)
                {
                    //cant process non existant chat.
                    return false;
                }
                //var senderAcc = await context.Set<Account>().Include(e => e.Roles).ThenInclude(e=>e.Permissions).AsSplitQuery().FirstOrDefaultAsync();
                var isMember = chat.Participants.Any(); //if any then sender is here since already filtered
                if (!isMember)
                {
                    return false;
                }
                var tracker = chat.MessageTrackers.FirstOrDefault(e => e.OwnerId == senderId);
                if (tracker == null)
                {
                    tracker = new()
                    {
                        CoOwnerId = chat.Id,
                        OwnerId = senderId,
                    };
                    await context.Set<ChatAccountMessageTracker>().AddAsync(tracker);
                }
                var newestMsg = chat.Messages.FirstOrDefault(); //can be empty resulting in null which is ok

                //assign message if any or null if not.
                tracker.SubjectId = newestMsg != null ? newestMsg.Id : null;


                await context.SaveChangesAsync();

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
                if (!isMember)
                {
                    return false;
                }
               
                ChatMute relation = new()
                {
                    MuterId = senderId,
                    SubjectId = chatId,
                };

                await context.Set<ChatMute>().AddAsync(relation); //throws error if already muted fyi

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
                var isMember = await context.Set<ChatParticipancy>().AnyAsync(o => o.ParticipantId == senderId && o.SubjectId == chatId);
                //verify chat has message
                var canPin = await context.Set<ChatMessage>().AnyAsync(o => o.Id == messageId && o.MessageHolderId == chatId && o.AuthorId != null); //filter away sys msg as you cant pin them business wise
                //var senderAcc = await context.Set<Account>().Include(e => e.Roles).ThenInclude(e=>e.Permissions).AsSplitQuery().FirstOrDefaultAsync();
                if (!isMember || !canPin)
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
                    Content = "@" + senderId.ToString() + " pinned https://echo.chat/" + chatId.ToString() + "/" + messageId.ToString() + " to this chat.",
                    MessageHolderId = chatId,
                };
                await context.Set<ChatMessagePin>().AddAsync(relation);
                await context.Set<ChatMessage>().AddAsync(sysmsg);

                await context.SaveChangesAsync();

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
                    .Include(e => e.Messages.Where(e => e.Id == messageId))
                    .ThenInclude(e => e.Children) //load message replies cause else you cant delete parent appearently event though you could in efcore5 just via restrict and sql
                    .Include(e => e.MessageTrackers) //can i do this?
                    .Include(e => e.Participants.Where(e => e.ParticipantId == senderId)) //filter from db
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == chatId);
                if (chat == null || !chat.Messages.Any())
                {
                    //cant process nonexistant chat and cant remove nonexistant message
                    return false;
                }
                var senderAcc = chat.Participants.FirstOrDefault(); //only sender should be loaded in context.
                var isMember = senderAcc != null; //if not null then sender is here since already filtered
                var msg = chat.Messages.FirstOrDefault(e => e.Id == messageId);
                var canRemove = senderAcc.IsOwner || msg.AuthorId == senderId; //can only remove messages you own or in chats you own
                //var senderAcc = await context.Set<Account>().Include(e => e.Roles).ThenInclude(e=>e.Permissions).AsSplitQuery().FirstOrDefaultAsync();
                if (!isMember || !canRemove)
                {
                    return false;
                }
                //sadly have to make another roundtrip
                //take the newest message which has an id less than deleted message.
                var newestMessage = await context.Set<ChatMessage>().Where(e => e.MessageHolderId == chatId && e.Id < messageId).OrderByDescending(e => e.TimeSent).Take(1).FirstOrDefaultAsync();
                ulong? newestMessageId = newestMessage == null ? null : newestMessage.Id;
                if (msg.MessageTrackers!=null && msg.MessageTrackers.Any())
                {
                    foreach (var tracker in msg.MessageTrackers)
                    {
                        tracker.SubjectId = newestMessageId; //puts the previous message if any or null if none.
                    }
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
                if (requestDTO.ReplyId == 0) //converts defaulted int to null in case sender forgot it
                {
                    requestDTO.ReplyId = null;
                }
                //verify sender is member
                var chat = await context.Set<Chat>()
                    .Include(e => e.Participants)
                    .ThenInclude(e => e.Participant)
                    //.ThenInclude(e => e.BlockedAccounts
                    //.Where(e => e.BlockedId == senderId)) //find out if message should be ignored // this will be implemented later
                    .Include(e => e.DirectMessageRelation)
                    .Include(e => e.Messages.Take(0)) //make efcore init list kinda cheap but im lazy rn
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == chatId);
                if (chat == null)
                {
                    return false;
                }
                var isMember = chat.Participants.Any(e => e.ParticipantId == senderId); //check if participants contain sender
                if (!isMember)
                {
                    return false;
                }
                ChatMessage msg = new()
                {
                    AuthorId = senderId,
                    Content = requestDTO.Content,
                    //MessageHolderId = chatId,
                    ParentId = requestDTO.ReplyId,
                    Attachments = requestDTO.Attachments.Select(e => new ChatMessageAttachment
                    {
                        Description = e.Description,
                        FileLocationURL = e.FileLocationURL,
                        FileName = e.FileName,
                    }).ToList(),
                };
                chat.Messages.Add(msg);
                foreach (var participant in chat.Participants) //loop through participants
                {
                    participant.Hidden = false; //show chat if hidden since activity has been registered
                }

                //var msgchanges = context.ChangeTracker.Entries<ChatMessage>().Where(e=>e.State != EntityState.Unchanged);
                //var attachchanges = context.ChangeTracker.Entries<ChatMessageAttachment>().Where(e=>e.State != EntityState.Unchanged);
                //var particichanges = context.ChangeTracker.Entries<ChatParticipancy>().Where(e=>e.State != EntityState.Unchanged);

                //var msgEvents = msgchanges.Select(e => new() { e.Entity.Id, e.Entity, e.State });

                //var addedmsg = msgchanges.Where(e => e.State == EntityState.Added).Select(e=>e.Entity);
                //var addedattach = attachchanges.Where(e => e.State == EntityState.Added).Select(e=>e.Entity);
                //var updatedParts = particichanges.Where(e => e.State == EntityState.Modified).Select(e=>e.Entity);

                var res = await context.SaveChangesAsync();


                //publish changes to signalr
                 //await notificationService.MessagesAdded(chatId, addedmsg); //entitychanges delete("message(holderId, Id, content, timecreated,timeedited,attachments[])")

                

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
                    .Include(e => e.Participants.Where(e => e.ParticipantId == senderId)) // filter participants from db
                    .Include(e => e.DirectMessageRelation)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == chatId);
                if (chat == null || chat.DirectMessageRelation != null)
                {
                    //cant process nonexistant chat and you are not allowed to change dm image as it will use accountprofile image of friend in chat when getting user session data
                    return false;
                }
                var isMember = chat.Participants.Any(); //if any then sender is here since already filtered
                if (!isMember)
                {
                    return false;
                }
                chat.IconUrl = requestDTO.ImageFileURL;

                await context.SaveChangesAsync();

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
                var isMember = await context.Set<ChatParticipancy>().AnyAsync(o => o.ParticipantId == senderId && o.SubjectId == chatId);
                if (!isMember) //muting an account only mutes the volume so you should be able to mute direct message chats and chats both
                {
                    return false;
                }
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
                var isMember = await context.Set<ChatParticipancy>().AnyAsync(o => o.ParticipantId == senderId && o.SubjectId == chatId);
                //verify chat has message
                var canUnpin = await context.Set<ChatMessage>().AnyAsync(o => o.Id == messageId && o.MessageHolderId == chatId && o.AuthorId != null); //filter away sysmsg cause you cant pin them business wise
                if (!isMember || !canUnpin)
                {
                    return false;
                }
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
                    .Include(e => e.Participants.Where(e => e.ParticipantId == senderId)) //filter participants from db
                    .Include(e => e.DirectMessageRelation)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == chatId);
                if (chat == null || chat.DirectMessageRelation != null)
                {
                    //cant process nonexistant chat and you are not allowed to change dm name as it will use name / displayname of friend in chat when getting user session data
                    return false;
                }
                var isMember = chat.Participants.Any(); //if any then sender is here since already filtered
                if (!isMember)
                {
                    return false;
                }
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
                    //filter participants from db
                    .Include(e => e.Participants.Where(e => e.ParticipantId == senderId)) 
                    //to change message sender must be owner
                    .Include(e => e.Messages.Where(e => e.Id == messageId && e.AuthorId == senderId)) 
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.Id == chatId);
                if (chat == null)
                {
                    return false;
                }
                var isMember = chat.Participants.Any(); //if any then sender is here since already filtered
                if (!isMember)
                {
                    return false;
                }
                var msg = chat.Messages.FirstOrDefault(e => e.Id == messageId);
                if (msg == null)
                {
                    //cant update nonexistant message
                    return false;
                }
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