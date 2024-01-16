using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Interfaces
{
    public interface IEntity<TId> : IEntity
    {
        new TId Id { get; set; }
    }

    public interface IEntity
    {
        object Id { get; set; }
    }
}
