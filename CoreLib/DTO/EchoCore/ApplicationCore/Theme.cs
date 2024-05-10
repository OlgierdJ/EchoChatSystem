using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.ApplicationCore
{
    public class ThemeDTO //: BaseEntity<uint>
    {
        public string Name { get; set; }
        public IEnumerable<AppearanceSettingsDTO>?  AppearanceSettings { get; set; }
    }
}
