namespace SoftwareEngineeringProject;

public static class GenericConverter
{
    public static T Parse<T>(in string sourceValue) where T : IConvertible
    {
        return (T)Convert.ChangeType(sourceValue, typeof(T));
    }
}
