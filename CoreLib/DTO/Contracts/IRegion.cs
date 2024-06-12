namespace CoreLib.DTO.Contracts;

public interface IRegion
{
    //uint Id { get; set; } inherit from iidentified or ientity instead
    string Name { get; set; }
    public string Icon { get; set; }
    string RegionServerURL { get; set; }
}