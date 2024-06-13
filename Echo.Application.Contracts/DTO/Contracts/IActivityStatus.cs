namespace Echo.Application.Contracts.DTO.Contracts;

public interface IActivityStatus : IActivityStatusMinimal
{
    string? Description { get; set; }
}