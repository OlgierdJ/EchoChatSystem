namespace CoreLib.DTO.Contracts;

public interface IMentionableRole : IRoleMinimal
{
    bool AllowAnyoneToMention { get; set; }
}
