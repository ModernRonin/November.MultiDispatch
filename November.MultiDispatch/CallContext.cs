using System;

namespace November.MultiDispatch
{
    /// <summary>
    /// The internal representation of a specific handler target.
    /// </summary>
    public sealed class CallContext
    {
        internal Action<object, object> Handler { get; set; }
        internal Func<object, bool> LeftPredicate { get; set; }
        internal Func<object, bool> RightPredicate { get; set; }
        internal void Invoke(object lhs, object rhs)
        {
            if (!LeftPredicate(lhs)) return;
            if (!RightPredicate(rhs)) return;
            Handler(lhs, rhs);
        }
    }
}