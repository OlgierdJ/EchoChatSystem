using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Interfaces.Providers;
public interface ITokenProvider
{
    string? AccessToken { get; set; }
    string? RefreshToken { get; set; }
}
