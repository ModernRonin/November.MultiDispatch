using System;

namespace November.MultiDispatch
{
    public class RightContinuation<TLeft, TRight>
    {
        readonly DoubleDispatcher mDispatcher;
        public RightContinuation(DoubleDispatcher dispatcher)
        {
            mDispatcher = dispatcher;
        }
        public void Do(Action<TLeft, TRight> handler)
        {
            mDispatcher.AddHandler(typeof(TLeft), typeof(TRight), handler.ToUntypedAction());
        }
    }

    public static class ActionExtensions
    {
        public static Action<object, object> ToUntypedAction<TLeft, TRight>(this Action<TLeft, TRight> self)
            => (l, r) => self((TLeft)l, (TRight)r);

    }
}