namespace Echo.Application.Contracts.DTO.Contracts;

public interface IStatefulPermission : IPermissionMinimal
{
    //ulong Id { get; set; } inherit from iidentified or ientity instead
    
    bool? State { get; set; }
}
