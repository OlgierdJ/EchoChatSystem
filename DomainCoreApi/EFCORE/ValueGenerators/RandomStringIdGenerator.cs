using CoreLib.Helpers;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DomainCoreApi.EFCORE.ValueGenerators;

public class RandomStringIdGenerator : ValueGenerator<string>
{
    public int MaxLength { get; set; }
    public override string Next(EntityEntry entry)
 => KeyGenerator.GetUniqueKey(MaxLength);

    override public bool GeneratesTemporaryValues => false;
}
