namespace CoreLib.DTO.EchoCore.RequestCore
{
    public class NoteUserRequestDTO //adds personal note to user
    {
        //public ulong SenderId { get; set; } from jwt
        public ulong UserId { get; set; }
        public string Note { get; set; }
    }
}
