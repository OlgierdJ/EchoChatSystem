﻿using Bogus;
using Bogus.DataSets;
using CoreLib.Entities.EchoCore.ChatCore;
using Microsoft.VisualBasic;

namespace CoreLib.Handlers
{
    public static class MyExtensions
    {
        public static string Password(this Internet internet, int minLength, int maxLength)
        {
            var r = internet.Random;

            var lowercase = r.Char('a', 'z').ToString();
            var uppercase = r.Char('A', 'Z').ToString();
            var number = r.Char('0', '9').ToString();
            var symbol = r.Char('!', '/').ToString();
            var padding = r.String2(minLength - 4);
            var padding2 = r.String2(r.Number(0, maxLength - minLength));  // random extra padding between min and max

            var chars = (lowercase + uppercase + number + symbol + padding + padding2).ToArray();
            var shuffledChars = r.Shuffle(chars).ToArray();

            return new string(shuffledChars);
        }

        public static string UserNameCustome(this Internet internet)
        {
            var r = internet.Random;
            int len = r.Int(0, 1);
            string username = internet.UserName();
            var holder = username.Split('.','_');
            if (holder.Length >= 2)
            {
                username = holder[len];
            }
            return username;
        }
    }
   
}
