using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.Enums
{
    public enum EventFrequency
    {
        //will be deleted when its over
        NonRepeating,
        //rest will change date once its over
        Weekly,
        BiWeekly,
        Monthly,
        Anually,
        DailyNonWeekend,
    }
}
