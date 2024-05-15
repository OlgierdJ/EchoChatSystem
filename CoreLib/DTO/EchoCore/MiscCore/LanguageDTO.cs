using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.MiscCore
{
    public class LanguageDTO
    {
        public uint Id { get; set; }
        public string Name { get; set; } //English (United States), 普通话, Dansk
        public string LanguageCode { get; set; } //en-US, zh-CN, da-DK
    }
}
