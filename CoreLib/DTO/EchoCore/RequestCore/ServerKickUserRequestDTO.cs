namespace CoreLib.DTO.EchoCore.RequestCore
{
    public class ServerKickUserRequestDTO //kicks user from server
    {
        //public ulong adminid { get; set; } //from jwt
        public ulong UserId { get; set; }
        public string? Reason { get; set; } // for audit log
    }
}
