namespace CoreLib.Interfaces.Contracts;

public interface IReport
{
    public string Message { get; set; }
    public DateTime TimeSent { get; set; }
}
