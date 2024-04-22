using CoreLib.Entities.EchoCore.UserCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Interfaces
{
    public interface IPasswordHandler
    {
        Task<bool> CheckPassword(string Password, ulong UserId);
        Task<SecurityCredentials> CreatePassword(string Password, ulong UserId);
    }
}
