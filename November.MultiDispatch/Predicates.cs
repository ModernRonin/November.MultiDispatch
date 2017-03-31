using System;

namespace November.MultiDispatch
{
    static class Predicates
    {
        public static Func<T, bool> AlwaysTrue<T>() => _ => true;
        public static Func<object, bool> ToUntyped<T>(this Func<T, bool> typed) => o => typed((T) o);
    }
}