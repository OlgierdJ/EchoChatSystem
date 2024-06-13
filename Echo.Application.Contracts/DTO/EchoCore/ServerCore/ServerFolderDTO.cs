namespace Echo.Application.Contracts.DTO.EchoCore.ServerCore;

public class ServerFolderDTO
{
    public ulong Id { get; set; }
    public int Importance { get; set; } //used for ordering folders
    public string Name { get; set; } //name of the folder
    public string? Colour { get; set; } //null = default, else set in hex string
    public ICollection<ServerDTO>? Servers { get; set; } //servers within this folder.
}
