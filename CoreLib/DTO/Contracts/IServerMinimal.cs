namespace CoreLib.DTO.Contracts
{
    public interface IServerMinimal
    {
        //ulong Id { get; set; } inherit from iidentified or ientity instead
        string ImageIconURL { get; set; }
        string Name { get; set; }
    }
}
