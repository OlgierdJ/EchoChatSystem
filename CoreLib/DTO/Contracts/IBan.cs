using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.Contracts
{
    public interface IBan
    {
        DateTime? ExpirationTime { get; set; }
        //ulong Id { get; set; } inherit from iidentified or ientity instead
        string? Reason { get; set; }

    }

    public interface IBan<TUser> : IBan
    {
        TUser User { get; set; }
    }
}
