using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.Enums
{
    public enum TextToSpeechNotificationMode
    {
        ForAllChannels = 0,
        ForCurrentSelectedChannel = 1,
        Never = 2,
    }
}
