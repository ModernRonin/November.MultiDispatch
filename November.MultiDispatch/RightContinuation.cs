using System;

namespace November.MultiDispatch
{
    public class RightContinuation<TLeft, TRight>
    {
        readonly Dispatcher mDispatcher;
        public RightContinuation(Dispatcher dispatcher)
        {
            mDispatcher = dispatcher;
        }
        public void Do(Action<TLeft, TRight> handler)
        {
            mDispatcher.AddHandler(typeof(TLeft), typeof(TRight), ToUntypedAction(handler));
        }
    }
}