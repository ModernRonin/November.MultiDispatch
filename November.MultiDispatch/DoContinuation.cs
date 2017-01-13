using System;

namespace November.MultiDispatch
{
    public class DoContinuation<TLeft, TRight>
    {
        readonly IDoubleReceiver mDispatcher;
        internal DoContinuation(IDoubleReceiver dispatcher)
        {
            mDispatcher = dispatcher;
        }
        public void Do(Action<TLeft, TRight> handler)
        {
            mDispatcher.AddHandler(typeof(TLeft), typeof(TRight), handler.ToUntypedAction());
        }
    }
}