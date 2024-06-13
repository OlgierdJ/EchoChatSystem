using Bogus;
using Echo.Application.Contracts.DTO.RequestCore.UserCore;
using Echo.Application.Contracts.RequestCore.FriendCore;
using Echo.Application.Contracts.RequestCore.MessageCore;
using Echo.Domain.Shared.Entities.EchoCore.ChatCore;

namespace Echo.Domain.Shared.Handlers;

public class PopulateDatahandler
{

    public ICollection<RegisterRequestDTO> GetRandomData(int NumberOfDataSet)
    {
        var dataset = new Faker<RegisterRequestDTO>()
            .RuleFor(e => e.Email, f => f.Internet.Email())
            .RuleFor(u => u.Username, f => f.Internet.UserNameCustome())
            .RuleFor(d => d.DisplayName, f => f.Internet.UserNameCustome())
            .RuleFor(p => p.Password, f => "Passw0rd")
            .RuleFor(d => d.DateOfBirth, f => f.Date.Past(19, DateTime.UtcNow))
            .RuleFor(a => a.AllowEchoMails, f => true);

        return dataset.Generate(NumberOfDataSet);
    }

    public ICollection<SendMessageRequestDTO> GetRandomDateForSendMessageRequest(int NumberOfDataSet)
    {
        var dataset = new Faker<SendMessageRequestDTO>()
            .RuleFor(c => c.Content, f => f.Random.Words(f.Random.Number(10)));
        return dataset.Generate(NumberOfDataSet);
    }

    public IEnumerable<Chat> GetRandomDateforchat(int NumberOfDataSet, Chat chat)
    {
        var dataset = new Faker<Chat>()
            .RuleFor(e => e.Messages, f => GetRandomDateformessages(chat.Participants.ToList(), f.Random.Number(NumberOfDataSet)));
        return dataset.Generate(NumberOfDataSet);
    }


    public IEnumerable<ChatMessage> GetRandomDateformessages(List<ChatParticipancy> account, int NumberOfDataSet)
    {
        var dataset = new Faker<ChatMessage>()
            .RuleFor(c => c.Content, f => f.WaffleText(f.Random.Number(1, 2), false))
            .RuleFor(c => c.TimeSent, f => f.Date.Past(1, DateTime.UtcNow))
            .RuleFor(c => c.AuthorId, f => account[f.Random.Number()].ParticipantId);
        return dataset.Generate(NumberOfDataSet);
    }

    public ICollection<AddFriendRequestDTO> GetRandomFriendRequest(int NumberOfDataSet, List<string> username)
    {
        var dataset = new Faker<AddFriendRequestDTO>()
            .RuleFor(c => c.Name, f => username[f.Random.Number(username.Count)]);
        return dataset.Generate(NumberOfDataSet);
    }

    public ICollection<holderUserId> GetRandomUserid(int NumberOfDataSet, List<ulong> userid)
    {
        var dataset = new Faker<holderUserId>()
            .RuleFor(c => c.userid, f => userid[f.Random.Number(userid.Count)]);
        return dataset.Generate(NumberOfDataSet);
    }

}

public class holderUserId
{
    public ulong userid { get; set; }
}
