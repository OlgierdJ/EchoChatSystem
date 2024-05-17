namespace CoreLib.DTO.EchoCore.RequestCore
{
    public class ChangeUserVolumeRequestDTO //mayb just alter volume locally?
        //used to change relative volume for specific user volume via slider
    {
        //public ulong SenderId { get; set; } get from jwt
        public ulong UserId { get; set; }
        public byte Volume { get; set; } //max 200 min 0
    }
}
