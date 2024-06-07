namespace CoreLib.DTO.Contracts
{
    public interface ILanguage
    {
        //uint Id { get; set; } inherit from iidentified or ientity instead
        string LanguageCode { get; set; }
        string Name { get; set; }
    }
}
