namespace CoreLib.DTO.Contracts;

public interface IUserMinimalWithPermissions<TPermission> : IUserMinimal
{
    ICollection<TPermission> Permissions { get; set; }
}
