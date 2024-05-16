namespace CoreLib.DTO.EchoCore.UserCore
{
    public class UserMinimalDTO
    {
        public ulong Id { get; set; } //their unique id for mapping interactions to api.
        public string DisplayName { get; set; } //unique handle or displayname if present.
        public string ImageIconURL { get; set; }
    }
}
