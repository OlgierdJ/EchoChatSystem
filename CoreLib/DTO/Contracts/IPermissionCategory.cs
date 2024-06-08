namespace CoreLib.DTO.Contracts
{
    public interface IPermissionCategory<TPermission>
    {
        string? Description { get; set; }
        //byte Id { get; set; } inherit from iidentified or ientity instead
        string Name { get; set; }
        ICollection<TPermission>? Permissions { get; set; }
    }
}
