using CoreLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

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
