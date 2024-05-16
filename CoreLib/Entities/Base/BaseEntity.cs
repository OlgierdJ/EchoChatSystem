using CoreLib.Interfaces;

namespace CoreLib.Entities.Base
{
    public abstract class BaseEntity<TId> : IEntity<TId> // done
    {
        public TId Id { get; set; }
        object IEntity.Id
        {
            get { return this.Id; }
            set { this.Id = (TId)value; }
        }
    }
}
