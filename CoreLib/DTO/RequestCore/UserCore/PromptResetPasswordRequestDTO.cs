namespace CoreLib.DTO.RequestCore.UserCore
{
    public class PromptResetPasswordRequestDTO //idk make different such that it sends mails to usermail or prompt user to send security question answer or smth
    {
        //public ulong UserId { get; set; } //get from jwt
        public string? Email { get; set; } //raw text via https encryption will be further hashed on server.
        public string? Username { get; set; } //raw text via https encryption will be further hashed on server.
    }
}
