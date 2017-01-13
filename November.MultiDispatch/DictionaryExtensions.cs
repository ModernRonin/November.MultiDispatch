using System;
using System.Collections.Generic;

namespace November.MultiDispatch
{
    public static class DictionaryExtensions
    {
        public static TValue GetOr<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, Action or) where TValue: class
        {
            if (self.ContainsKey(key)) return self[key];
            or();
            return null;
        }
    }
}