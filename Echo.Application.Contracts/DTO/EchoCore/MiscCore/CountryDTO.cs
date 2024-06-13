using Echo.Application.Contracts.DTO.Contracts;

namespace Echo.Application.Contracts.DTO.EchoCore.MiscCore;


public class CountryDTO : ICountry
{
    public uint Id { get; set; }
    public string Name { get; set; }
}
