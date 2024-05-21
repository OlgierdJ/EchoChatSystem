namespace CoreLib.DTO.RequestCore.RelationCore
{
    public class SetNoteUserRequestDTO //adds personal note to user
    {
        //public ulong SenderId { get; set; } from jwt
        public ulong UserId { get; set; }
        public string Note { get; set; }
    }
}
