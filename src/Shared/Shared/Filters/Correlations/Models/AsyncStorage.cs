namespace Shared.Filters.Correlations.Models;

public static class AsyncStorage<T> where T : new()
{
    private static readonly AsyncLocal<T> s_asyncLocal = new AsyncLocal<T>();

    public static T Store(T val)
    {
        s_asyncLocal.Value = val;
        return s_asyncLocal.Value;
    }

    public static T? Retrieve()
    {
        return s_asyncLocal.Value;
    }
}
