using System.Collections.Generic;

namespace Ark.Collections {
    static class DictionaryExtensions {
        public static TValue GetOrCreateValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) where TValue : class, new() {
            TValue value;
            if (!dictionary.TryGetValue(key, out value)) {
                value = new TValue();
                dictionary.Add(key, value);
            }
            return value;
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) {
            TValue value;
            if (dictionary.TryGetValue(key, out value)) {
                return value;
            } else {
                return default(TValue);
            }
        }
    }
}
