using System;
using static November.MultiDispatch.Predicates;

namespace November.MultiDispatch
{
    public class LeftContinuation<TCommonBase, TLeft> where TLeft : TCommonBase
    {
        readonly IDoubleReceiver mDispatcher;
        readonly Func<TLeft, bool> mPredicate;
        internal LeftContinuation(IDoubleReceiver dispatcher) : this(dispatcher, AlwaysTrue<TLeft>()) {}
        internal LeftContinuation(IDoubleReceiver dispatcher, Func<TLeft, bool> predicate)
        {
            mDispatcher = dispatcher;
            mPredicate = predicate;
        }
        public DoContinuation<TLeft, TRight> OnRight<TRight>() where TRight : TCommonBase
        {
            return new DoContinuation<TLeft, TRight>(mDispatcher, mPredicate, _ => true);
        }
        public DoContinuation<TLeft, TRight> OnRight<TRight>(Func<TRight, bool> predicate) where TRight : TCommonBase
        {
            return new DoContinuation<TLeft, TRight>(mDispatcher, mPredicate, predicate);
        }
    }
}