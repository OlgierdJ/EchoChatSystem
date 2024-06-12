namespace CoreLib.DTO.RequestCore.UserCore;

public class UpdatePasswordRequestDTO
{
    //public ulong UserId { get; set; } //get from jwt
    public string NewPassword { get; set; } //raw text via https encryption will be further hashed on server.
}
