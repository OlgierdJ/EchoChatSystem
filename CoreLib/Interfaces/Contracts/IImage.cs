using CoreLib.Interfaces;

namespace CoreLib.Interfaces.Contracts
{
    public interface IImage
    {

        public string ImageURL { get; set; }
        public uint Importance { get; set; }

    }
}