namespace Echo.Application.Contracts.DTO.Contracts;

public interface IUserMinimal
{
    string DisplayName { get; set; }
    //ulong Id { get; set; } inherit from iidentified or ientity instead
    string ImageIconURL { get; set; }
}
