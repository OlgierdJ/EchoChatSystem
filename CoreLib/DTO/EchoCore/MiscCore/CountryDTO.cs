namespace CoreLib.DTO.EchoCore.MiscCore
{
    public interface ICountry
    {
        uint Id { get; set; }
        string Name { get; set; }
    }

    public class CountryDTO : ICountry
    {
        public uint Id { get; set; }
        public string Name { get; set; }
    }
}
