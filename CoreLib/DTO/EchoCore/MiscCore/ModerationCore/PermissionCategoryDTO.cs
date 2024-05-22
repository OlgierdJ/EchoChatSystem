namespace CoreLib.DTO.EchoCore.MiscCore.ModerationCore
{
    public interface IPermissionCategory<TPermission>
    {
        string? Description { get; set; }
        byte Id { get; set; }
        string Name { get; set; }
        ICollection<TPermission>? Permissions { get; set; }
    }

    public class PermissionCategoryDTO : IPermissionCategory<StatefulPermissionExtendedDTO>
    //used to display possible permissions for a role when editing them.
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<StatefulPermissionExtendedDTO>? Permissions { get; set; }
    }
}
