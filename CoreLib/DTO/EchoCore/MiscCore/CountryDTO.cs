﻿using CoreLib.DTO.Contracts;

namespace CoreLib.DTO.EchoCore.MiscCore
{

    public class CountryDTO : ICountry
    {
        public uint Id { get; set; }
        public string Name { get; set; }
    }
}
