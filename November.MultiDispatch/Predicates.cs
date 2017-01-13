using System;

namespace November.MultiDispatch
{
    public static class Predicates
    {
        public static Func<T, bool> AlwaysTrue<T>() => _ => true;
    }
}