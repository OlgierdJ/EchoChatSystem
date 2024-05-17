namespace CoreLib.DTO.EchoCore.RequestCore
{
    public class AddFriendRequestDTO //sends request to handle owner
    {
        //public ulong SenderId { get; set; } // from jwt
        public string Name { get; set; } //unique handle for creating request
    }
}
