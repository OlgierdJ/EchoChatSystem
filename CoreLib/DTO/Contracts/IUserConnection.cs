using CoreLib.DTO.EchoCore.MiscCore;

namespace CoreLib.DTO.Contracts
{
    public interface IUserConnection
    {
        bool DisplayOnProfile { get; set; }
        //ulong Id { get; set; } inherit from iidentified or ientity instead
        string Name { get; set; }
        ConnectionDTO Type { get; set; }
    }
}
