namespace Echo.Domain.Shared.Helpers;

public static class RandomHelpers
{
    /// <summary>
    /// fixed length
    /// </summary>
    /// <returns></returns>
    public static string GenerateRandom8LenStringIdFromGuid()
    {
        return Guid.NewGuid().ToString().Substring(0, 8);
    }
    /// <summary>
    /// max 32 length
    /// </summary>
    /// <param name="len"></param>
    /// <returns></returns>
    public static string GenerateRandomNLengthStringIdFromGuid(int len)
    {
        return Guid.NewGuid().ToString().Substring(0, len).Remove('-');
    }
}
