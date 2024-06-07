using CoreLib.Interfaces;

namespace CoreLib.Entities.Base
{
    public interface IAuditableMessage : IMessage
    {
        public DateTime? TimeEdited { get; set; }
    }
}
