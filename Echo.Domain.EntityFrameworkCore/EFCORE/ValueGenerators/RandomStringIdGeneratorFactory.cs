using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.ValueGenerators;

public class RandomStringIdGeneratorFactory : ValueGeneratorFactory
{
    public override ValueGenerator Create(IProperty property, ITypeBase typeBase)
    {
        return new RandomStringIdGenerator() { MaxLength = property.GetMaxLength().GetValueOrDefault() };
    }
}
