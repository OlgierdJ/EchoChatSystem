namespace CoreLib.DTO.Contracts;

public interface IActivityStatus : IActivityStatusMinimal
{
    string? Description { get; set; }
}