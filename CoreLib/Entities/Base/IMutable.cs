namespace CoreLib.Entities.Base
{
    public interface IMutable<TMute>
    {
        public ICollection<TMute>? Muters { get; set; }
    }

}
