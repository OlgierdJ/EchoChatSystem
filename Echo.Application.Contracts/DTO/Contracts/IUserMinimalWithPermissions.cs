namespace Echo.Application.Contracts.DTO.Contracts;

public interface IUserMinimalWithPermissions<TPermission> : IUserMinimal
{
    ICollection<TPermission> Permissions { get; set; }
}
