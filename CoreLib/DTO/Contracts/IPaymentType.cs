namespace CoreLib.DTO.Contracts
{
    public interface IPaymentType
    {
        string Icon { get; set; }
        //uint Id { get; set; } inherit from iidentified or ientity instead
        string Name { get; set; }
    }
}
