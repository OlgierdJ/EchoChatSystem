using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Handlers
{
    public class TokenHandler
    {
        private const string TokenKey = "WeLoveBigBeautifulWomanAndWeCantLie";
        private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(8);
    }
}
