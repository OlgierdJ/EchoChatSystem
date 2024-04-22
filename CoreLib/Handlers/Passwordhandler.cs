using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Handlers
{
    public class Passwordhandler : IPasswordHandler
    {
        //https://code-maze.com/csharp-hashing-salting-passwords-best-practices/
        public Task<bool> CheckPassword(string Password, ulong UserId)
        {
            throw new NotImplementedException();
        }

        public Task<SecurityCredentials> CreatePassword(string Password, ulong UserId)
        {
            throw new NotImplementedException();
        }
    }
}
