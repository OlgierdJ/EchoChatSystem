namespace CoreLib.Entities.Base
{
    public abstract class BaseRole<TId, TRecipient, TPermission> : BaseEntity<TId>
    {
        public string Name { get; set; }

        public ICollection<TRecipient>? Recipients { get; set; }
        public ICollection<TPermission>? Permissions { get; set; }
    }
}
