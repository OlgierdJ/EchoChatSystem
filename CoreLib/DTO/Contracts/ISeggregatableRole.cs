namespace CoreLib.DTO.Contracts
{
    public interface ISeggregatableRole : IRoleMinimal
    {
        public bool DisplaySeperatelyFromOnlineMembers { get; set; }
    }
}
