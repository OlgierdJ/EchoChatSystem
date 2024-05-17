namespace CoreLib.Entities.Base
{
    public class BasePermission<TId, TRole> : BaseEntity<TId>
    {
        public string Name { get; set; } //Example app_view_admin_userinterface
        public ICollection<TRole>? Roles { get; set; }
    }
}
