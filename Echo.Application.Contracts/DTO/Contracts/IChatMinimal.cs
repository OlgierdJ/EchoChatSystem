namespace Echo.Application.Contracts.DTO.Contracts;

public interface IChatMinimal
{
    //ulong Id { get; set; } inherit from iidentified or ientity instead
    string Name { get; set; }
    string? CategoryName { get; set; }
    int OrderWeight { get; set; }
}
