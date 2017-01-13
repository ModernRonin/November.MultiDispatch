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
        /// <summary>
        /// Fluently specify the right argument type. To be followed by <see cref="DoContinuation{TLeft,TRight}.Do"/>.
        /// </summary>
        /// <typeparam name="TRight">the type of the right argument</typeparam>
        public DoContinuation<TLeft, TRight> OnRight<TRight>() where TRight : TCommonBase
        {
            return new DoContinuation<TLeft, TRight>(mDispatcher, mPredicate, _ => true);
        }
        /// <summary>
        /// Fluently specify the right argument type with an additional check that needs to be passed.
        /// To be followed by <see cref="DoContinuation{TLeft,TRight}.Do"/>.
        /// </summary>
        /// <typeparam name="TRight">the type of the right argument</typeparam>
        /// <param name="rightPredicate">the check to be passed</param>
        public DoContinuation<TLeft, TRight> OnRight<TRight>(Func<TRight, bool> rightPredicate) where TRight : TCommonBase
        {
            return new DoContinuation<TLeft, TRight>(mDispatcher, mPredicate, rightPredicate);
        }
    }
}