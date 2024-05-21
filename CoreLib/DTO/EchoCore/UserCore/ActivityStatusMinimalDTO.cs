namespace CoreLib.DTO.EchoCore.UserCore
{
    public interface IActivityStatusMinimal
    {
        string Icon { get; set; }
        string IconColor { get; set; }
        byte Id { get; set; }
        string Name { get; set; }
    }

    public class ActivityStatusMinimalDTO : IActivityStatusMinimal
    //used for displaying the status beside users
    {
        public byte Id { get; set; }
        public string Icon { get; set; }
        public string IconColor { get; set; }
        public string Name { get; set; } //online, offline, invisible, etc
    }
}
