namespace CoreLib.DTO.Contracts;

public interface ICustomizableRole : IRoleMinimal, IMentionableRole, ISeggregatableRole
{

    public string Colour { get; set; }
    public string IconURL { get; set; }

}
