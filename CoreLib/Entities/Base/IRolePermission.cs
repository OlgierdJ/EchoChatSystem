namespace CoreLib.Entities.Base
{
    public interface IRolePermission<TRole> : IPermission
    {
        public ICollection<TRole>? Roles { get; set; }
    }

    public interface IPermission
    {
        public string Name { get; set; } //Example app_view_admin_userinterface
    }
}
