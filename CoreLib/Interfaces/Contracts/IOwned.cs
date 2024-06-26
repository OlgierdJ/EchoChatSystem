﻿namespace CoreLib.Interfaces.Contracts
{
    public interface IOwned<TOwner, TOwnerId>
    {
        public TOwnerId? OwnerId { get; set; }
        public TOwner? Owner { get; set; }
    }
}
