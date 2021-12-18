namespace Kian;

public static class ObjectExtensions
{
    public static T? Convert<T>(this object obj)
    {
        if (obj is DBNull)
        {
            return default;
        }

        return (T)obj;
    }
}