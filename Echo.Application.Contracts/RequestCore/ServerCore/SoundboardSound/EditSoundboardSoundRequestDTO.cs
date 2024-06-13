namespace Echo.Application.Contracts.DTO.RequestCore.ServerCore.SoundboardSound;

public class EditSoundboardSoundRequestDTO
{

    //public ulong uploaderId { get; set; } //get from jwt
    public ulong? EmoteId { get; set; }
    public string Name { get; set; }
    public byte Volume { get; set; } //maybe ignored???

}
