namespace CoreLib.DTO.Contracts
{
    public interface IActivityStatusMinimal
    {
        string Icon { get; set; }
        string IconColor { get; set; }
        //byte Id { get; set; } inherit from iidentified or ientity instead
        string Name { get; set; }
    }
}
