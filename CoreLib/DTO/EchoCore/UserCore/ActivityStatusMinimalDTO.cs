using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.UserCore
{
    public class ActivityStatusMinimalDTO //used for displaying the status beside users
    {
        public byte Id { get; set; }
        public string Icon { get; set; }
        public string IconColor { get; set; }
        public string Name { get; set; } //online, offline, invisible, etc
    }
}
