namespace Echo.Application.Contracts.DTO.Contracts;

public interface IPermission : IPermissionMinimal
{
    string? Description { get; set; }
    string? GroupingName { get; set; }
}
