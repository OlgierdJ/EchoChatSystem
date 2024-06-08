namespace CoreLib.DTO.Contracts
{
    public interface IBadge
    {
        string Description { get; set; }
        string IconURL { get; set; }
        //ulong Id { get; set; } inherit from iidentified or ientity instead
        int OrderingWeight { get; set; }
    }
}