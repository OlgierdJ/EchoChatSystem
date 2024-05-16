namespace CoreLib.DTO.EchoCore.RequestCore
{
    public class SetStatusRequestDTO //changes status and pushes change to listeners.
    {
        //public ulong senderid { get; set; } get from jwt
        public byte Id { get; set; } //status id.
    }
}
