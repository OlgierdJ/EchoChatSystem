namespace Echo.Application.Contracts.DTO.Contracts;

public interface IRoleMinimal
{
    //ulong Id { get; set; } inherit from iidentified or ientity instead

    string Name { get; set; }
}
