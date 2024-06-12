namespace CoreLib.DTO.Contracts;

public interface ICountry
{
    //uint Id { get; set; } inherit from iidentified or ientity instead
    string Name { get; set; }
}
