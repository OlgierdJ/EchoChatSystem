namespace CoreLib.DTO.EchoCore.RequestCore
{
    public class SetNicknameUserRequestDTO //adds personal nickname on user
    {
        //public ulong SenderId { get; set; } from jwt
        public ulong UserId { get; set; }
        public string Nickname { get; set; }
    }
}
