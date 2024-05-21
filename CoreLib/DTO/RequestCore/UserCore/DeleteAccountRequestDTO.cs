namespace CoreLib.DTO.RequestCore.UserCore
{
    public class DeleteAccountRequestDTO
    {
        //public ulong Id { get; set; } get from jwt.
        public string Password { get; set; } //for confirmation
    }
}
