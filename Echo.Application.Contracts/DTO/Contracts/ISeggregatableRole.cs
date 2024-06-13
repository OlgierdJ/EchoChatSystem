namespace Echo.Application.Contracts.DTO.Contracts;

public interface ISeggregatableRole : IRoleMinimal
{
    public bool DisplaySeperatelyFromOnlineMembers { get; set; }
}
