using Echo.Application.Contracts.DTO.Contracts;

namespace Echo.Application.Contracts.DTO.EchoCore.UserCore;


public class ActivityStatusDTO : IActivityStatusMinimal, IActivityStatus
//used for loading the actual possible status upon loading list for selection.
{
    //public byte Id { get; set; }
    //public string Name { get; set; }
    public string? Description { get; set; }
    public string Icon { get; set; }
    public string IconColor { get; set; }
    public byte Id { get; set; }
    public string Name { get; set; }
    //public string Icon { get; set; }
    //public string IconColor { get; set; }
}