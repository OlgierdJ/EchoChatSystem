﻿namespace CoreLib.DTO.Contracts
{
    public interface IInvite : IInviteMinimal
    {
        string Description { get; set; }
        string ImageIconURL { get; set; }
        string Title { get; set; }
    }


}
