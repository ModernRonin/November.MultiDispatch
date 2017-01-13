using System;
using static November.MultiDispatch.Predicates;

namespace November.MultiDispatch
{
    public class DoContinuation<TLeft, TRight>
    {
        readonly IDoubleReceiver mDispatcher;
        readonly Func<TLeft, bool> mLeftPredicate;
        readonly Func<TRight, bool> mRightPredicate;
        internal DoContinuation(IDoubleReceiver dispatcher)
            : this(dispatcher, AlwaysTrue<TLeft>(), AlwaysTrue<TRight>()) {}
        internal DoContinuation(
            IDoubleReceiver dispatcher,
            Func<TLeft, bool> leftPredicate,
            Func<TRight, bool> rightPredicate)
        {
            mDispatcher = dispatcher;
            mLeftPredicate = leftPredicate;
            mRightPredicate = rightPredicate;
        }
        public void Do(Action<TLeft, TRight> handler)
        {
            mDispatcher.AddHandler(typeof(TLeft), typeof(TRight), handler.ToUntypedAction());
        }
    }
}