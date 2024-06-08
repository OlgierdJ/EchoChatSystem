using CoreLib.Interfaces;

namespace CoreLib.Interfaces.Contracts
{
    public interface IRole<TRecipient, TPermission>
    {
        public string Name { get; set; }

        public ICollection<TRecipient>? Recipients { get; set; }
        public ICollection<TPermission>? Permissions { get; set; }
    }


}
