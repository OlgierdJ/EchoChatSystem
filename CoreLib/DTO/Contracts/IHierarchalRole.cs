namespace CoreLib.DTO.Contracts;

public interface IHierarchalRole : IRoleMinimal
{
    int Importance { get; set; }
    bool IsAdmin { get; set; }
}
