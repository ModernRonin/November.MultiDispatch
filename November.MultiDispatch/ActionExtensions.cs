using System;

namespace November.MultiDispatch
{
    static class ActionExtensions
    {
        public static Action<object, object> ToUntyped<TLeft, TRight>(this Action<TLeft, TRight> self)
            => (l, r) => self((TLeft) l, (TRight) r);
    }
}