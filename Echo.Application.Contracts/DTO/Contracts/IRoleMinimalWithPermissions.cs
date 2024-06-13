namespace Echo.Application.Contracts.DTO.Contracts;

public interface IRoleMinimalWithPermissions<TPermission> : IRoleMinimal
{
    ICollection<TPermission> Permissions { get; set; }
}
