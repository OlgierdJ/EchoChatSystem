﻿using CoreLib.Interfaces;

namespace CoreLib.Interfaces.Contracts
{
    public interface IMessageHolder<TMessage> //move name and timecreated into seperate interface
    {
        //public string Name { get; set; }
        //public DateTime TimeCreated { get; set; }
        public ICollection<TMessage>? Messages { get; set; }
    }
}
