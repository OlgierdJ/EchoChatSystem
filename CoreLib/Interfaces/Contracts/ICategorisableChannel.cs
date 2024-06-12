﻿namespace CoreLib.Interfaces.Contracts;

public interface ICategorisableChannel<TChannelCategory, TChannelCategoryId> : IChannel, ICategorisable<TChannelCategory, TChannelCategoryId>
{
    //integrations? webhooks? invites? channelfollows?
}