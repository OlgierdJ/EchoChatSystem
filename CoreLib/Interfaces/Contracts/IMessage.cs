namespace CoreLib.Interfaces.Contracts
{
    public interface IMessage
    {
        public string Content { get; set; }
        public DateTime TimeSent { get; set; }
    }
}
