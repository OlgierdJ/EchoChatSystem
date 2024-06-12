namespace CoreLib.Interfaces.Contracts;

public interface IRolePermission<TRole> : IPermission
{
    public ICollection<TRole>? Roles { get; set; }
}
