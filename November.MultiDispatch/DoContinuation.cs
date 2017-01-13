using System;

namespace November.MultiDispatch
{
    public class DoContinuation<TLeft, TRight>
    {
        readonly DoubleDispatcher mDispatcher;
        public DoContinuation(DoubleDispatcher dispatcher)
        {
            mDispatcher = dispatcher;
        }
        public void Do(Action<TLeft, TRight> handler)
        {
            mDispatcher.AddHandler(typeof(TLeft), typeof(TRight), handler.ToUntypedAction());
        }
    }
}