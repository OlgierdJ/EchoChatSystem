namespace CoreLib.DTO.EchoCore.MiscCore
{
    public interface ICurrency
    {
        uint Id { get; set; }
        string Name { get; set; }
    }

    public class CurrencyDTO : ICurrency
    {
        public uint Id { get; set; }
        public string Name { get; set; }
    }
}
