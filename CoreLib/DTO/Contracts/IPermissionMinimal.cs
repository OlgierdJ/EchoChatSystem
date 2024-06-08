namespace CoreLib.DTO.Contracts
{
    public interface IPermissionMinimal
    {
        //ulong Id { get; set; } inherit from iidentified or ientity instead
        string Name { get; set; }
    }
}
