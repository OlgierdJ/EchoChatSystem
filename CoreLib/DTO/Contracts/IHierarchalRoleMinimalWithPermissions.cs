namespace CoreLib.DTO.Contracts
{
    public interface IHierarchalRoleMinimalWithPermissions<TPermission> : IHierarchalRole
    {
        ICollection<TPermission> Permissions { get; set; }
    }
}
