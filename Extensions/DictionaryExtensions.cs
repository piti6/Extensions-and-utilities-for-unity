using System.Collections.Generic;

public static class DictionaryExtensions
{
    public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
    {
        return GetOrAdd(dictionary, key, () => new TValue());
    }

    public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, System.Func<TValue> valueFactory)
    {
        if (!dictionary.TryGetValue(key, out var value))
        {
            value = dictionary[key] = valueFactory.Invoke();
        }

        return value;
    }
}