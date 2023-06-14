﻿namespace PatchManager.Shared;

/// <summary>
/// Utility extension methods for various types.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Deconstructs a <see cref="KeyValuePair{TKey,TValue}"/> into its key and value.
    /// </summary>
    /// <param name="keyValuePair">Key-value pair to deconstruct.</param>
    /// <param name="key">The deconstructed key.</param>
    /// <param name="value">The deconstructed value.</param>
    /// <typeparam name="TKey">Type of the key.</typeparam>
    /// <typeparam name="TValue">Type of the value.</typeparam>
    public static void Deconstruct<TKey, TValue>(
        this KeyValuePair<TKey, TValue> keyValuePair,
        out TKey key,
        out TValue value
    )
    {
        key = keyValuePair.Key;
        value = keyValuePair.Value;
    }

    /// <summary>
    /// Adds the contents of an <see cref="IEnumerable{T}"/> of type <see cref="KeyValuePair{TKey,TValue}"/>
    /// to a <see cref="Dictionary{TKey,TValue}"/>.
    /// </summary>
    /// <param name="target">Target dictionary to add to.</param>
    /// <param name="source">Source enumerable to add from.</param>
    /// <typeparam name="TKey">Type of the key.</typeparam>
    /// <typeparam name="TValue">Type of the value.</typeparam>
    public static void AddRange<TKey, TValue>(
        this Dictionary<TKey, TValue> target,
        IEnumerable<KeyValuePair<TKey, TValue>> source
    )
    {
        foreach (var kvp in source)
        {
            target.Add(kvp.Key, kvp.Value);
        }
    }
}