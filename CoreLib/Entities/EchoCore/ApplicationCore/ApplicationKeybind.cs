using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ApplicationCore
{
    public class ApplicationKeybind : BaseEntity<byte>
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Keybind>? Keybinds { get; set; }
    }
}
