namespace CoreLib.DTO.RequestCore.UserCore;

public class SetStatusRequestDTO //changes status and pushes change to listeners.
{
    //public ulong senderid { get; set; } get from jwt
    public byte Id { get; set; } //status id.
}
