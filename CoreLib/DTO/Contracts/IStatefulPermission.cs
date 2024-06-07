namespace CoreLib.DTO.Contracts
{
    public interface IStatefulPermission : IPermissionMinimal
    {
        //ulong Id { get; set; } inherit from iidentified or ientity instead
        string Name { get; set; }
        bool? State { get; set; }
    }
}
