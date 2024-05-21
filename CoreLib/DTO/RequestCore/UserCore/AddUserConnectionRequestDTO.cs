namespace CoreLib.DTO.RequestCore.UserCore
{
    public class AddUserConnectionRequestDTO //used for adding connection to the user
    {
        //make addconnectionrequest which takes userid, name, token, displayonprofile, typeid.
        //public ulong UserId { get; set; } //get from jwt
        public uint TypeId { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public bool DisplayOnProfile { get; set; }
    }
}
