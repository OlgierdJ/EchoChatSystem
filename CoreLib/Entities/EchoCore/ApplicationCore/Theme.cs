using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ApplicationCore
{
    public class Theme : BaseEntity<uint>
    {
        public string Name { get; set; }
        public IEnumerable<AppearanceSettings>?  AppearanceSettings { get; set; }
    }
}
