﻿using System;

namespace November.MultiDispatch
{
    public static class ActionExtensions
    {
        public static Action<object, object> ToUntypedAction<TLeft, TRight>(this Action<TLeft, TRight> self)
            => (l, r) => self((TLeft) l, (TRight) r);
    }
}