using CoreLib.Interfaces;

namespace CoreLib.Entities.Base
{
    public interface IImage
    {
       
        public string ImageURL { get; set; }
        public uint Importance { get; set; }
        
    }
}