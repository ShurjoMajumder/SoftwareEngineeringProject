namespace SoftwareEngineeringProject;

public class GenericConverter
{
    public static T Parse<T>(string sourceValue) where T : IConvertible
    {
        return (T)Convert.ChangeType(sourceValue, typeof(T));
    }

    public static T Parse<T>(string sourceValue, IFormatProvider provider) where T : IConvertible
    {
        return (T)Convert.ChangeType(sourceValue, typeof(T), provider);
    }
}