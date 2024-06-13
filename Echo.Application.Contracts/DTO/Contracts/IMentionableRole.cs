namespace Echo.Application.Contracts.DTO.Contracts;

public interface IMentionableRole : IRoleMinimal
{
    bool AllowAnyoneToMention { get; set; }
}
