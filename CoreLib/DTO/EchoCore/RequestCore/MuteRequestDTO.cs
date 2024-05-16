namespace CoreLib.DTO.EchoCore.RequestCore
{
    public class MuteRequestDTO
    //sent to many different controllers such as textchannel, voicechannel, server, servermute, serverdeafen, chat, account, soundboard etc. to create a mute or deafen relation for the sender towards the subject.
    {
        //public ulong MuterId { get; set; } //owner from jwt
        public ulong SubjectId { get; set; } //external subject (maybe make id generic, but generally subject always has ulong id.)
        public DateTime? ExpirationTime { get; set; }
    }
}
