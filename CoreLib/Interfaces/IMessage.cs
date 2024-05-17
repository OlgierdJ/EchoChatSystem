namespace CoreLib.Interfaces
{
    public interface IMessage : IEntity
    {
        public string Content { get; set; }
        public DateTime TimeSent { get; set; }
    }
}
