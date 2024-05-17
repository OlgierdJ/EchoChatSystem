namespace CoreLib.DTO.EchoCore.UserCore
{
    public class MembershipBadgeDTO
    {
        public int OrderingWeight { get; set; } //used for sorting
        public string IconURL { get; set; } //relative icon
        public string IconName { get; set; } //echo, server name, subscription name, etc
        public DateTime TimeJoined { get; set; } //date the user became a member of the specific thing
    }
}