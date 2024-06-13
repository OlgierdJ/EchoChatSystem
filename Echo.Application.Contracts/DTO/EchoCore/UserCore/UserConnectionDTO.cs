using Echo.Application.Contracts.DTO.Contracts;
using Echo.Application.Contracts.DTO.EchoCore.MiscCore;

namespace Echo.Application.Contracts.DTO.EchoCore.UserCore;


public class UserConnectionDTO : IUserConnection
//this does not contain tokens or other functionality cause this only represents the relation that exists on the server.
//All functionality regarding the linked platform is performed by the server on the users behalf.
{

    public ulong Id { get; set; } //id of the connection relative to the user.
    public string Name { get; set; } //connection account name
    public bool DisplayOnProfile { get; set; } //shows badge on userprofile.
    public ConnectionDTO Type { get; set; }
}
