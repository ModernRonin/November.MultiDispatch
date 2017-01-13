using System;
using static November.MultiDispatch.Predicates;

namespace November.MultiDispatch
{
    public class RightContinuation<TCommonBase, TRight> where TRight : TCommonBase
    {
        readonly IDoubleReceiver mDispatcher;
        readonly Func<TRight, bool> mPredicate;
        internal RightContinuation(IDoubleReceiver dispatcher) : this(dispatcher, AlwaysTrue<TRight>()) {}
        internal RightContinuation(IDoubleReceiver dispatcher, Func<TRight, bool> predicate)
        {
            mDispatcher = dispatcher;
            mPredicate = predicate;
        }
        public DoContinuation<TLeft, TRight> OnLeft<TLeft>() where TLeft : TCommonBase
        {
            return new DoContinuation<TLeft, TRight>(mDispatcher, AlwaysTrue<TLeft>(), mPredicate);
        }
        public DoContinuation<TLeft, TRight> OnLeft<TLeft>(Func<TLeft, bool> leftPredicate) where TLeft : TCommonBase
        {
            return new DoContinuation<TLeft, TRight>(mDispatcher, leftPredicate, mPredicate);
        }
    }
}