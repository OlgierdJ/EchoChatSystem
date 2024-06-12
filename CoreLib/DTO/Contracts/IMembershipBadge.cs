namespace CoreLib.DTO.Contracts;

public interface IMembershipBadge
{
    string IconName { get; set; }
    string IconURL { get; set; }
    int OrderingWeight { get; set; }
    DateTime TimeJoined { get; set; }
}