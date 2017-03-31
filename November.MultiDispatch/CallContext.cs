using System;

namespace November.MultiDispatch
{
    public class CallContext
    {
        public Action<object, object> Handler { get; set; }
        public Func<object, bool> LeftPredicate { get; set; }
        public Func<object, bool> RightPredicate { get; set; }
        public void Invoke(object lhs, object rhs)
        {
            if (!LeftPredicate(lhs)) return;
            if (!RightPredicate(rhs)) return;
            Handler(lhs, rhs);
        }
    }
}