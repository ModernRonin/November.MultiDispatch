using System;
using static November.MultiDispatch.Predicates;

namespace November.MultiDispatch
{
    public class LeftContinuation<TCommonBase, TLeft> where TLeft : TCommonBase
    {
        readonly DoubleDispatcher<TCommonBase> mDispatcher;
        readonly Func<TLeft, bool> mPredicate;
        internal LeftContinuation(DoubleDispatcher<TCommonBase> dispatcher) : this(dispatcher, AlwaysTrue<TLeft>()) {}
        internal LeftContinuation(DoubleDispatcher<TCommonBase> dispatcher, Func<TLeft, bool> predicate)
        {
            mDispatcher = dispatcher;
            mPredicate = predicate;
        }
        /// <summary>
        /// Fluently specify the right argument type. To be followed by <see cref="DoContinuation{TCommonBase, TLeft,TRight}.Do"/>.
        /// </summary>
        /// <typeparam name="TRight">the type of the right argument</typeparam>
        public DoContinuation<TCommonBase, TLeft, TRight> OnRight<TRight>() where TRight : TCommonBase
        {
            return new DoContinuation<TCommonBase, TLeft, TRight>(mDispatcher, mPredicate, _ => true);
        }
        /// <summary>
        /// Fluently specify the right argument type with an additional check that needs to be passed.
        /// To be followed by <see cref="DoContinuation{TCommonBase, TLeft,TRight}.Do"/>.
        /// </summary>
        /// <typeparam name="TRight">the type of the right argument</typeparam>
        /// <param name="rightPredicate">the check to be passed</param>
        public DoContinuation<TCommonBase, TLeft, TRight> OnRight<TRight>(Func<TRight, bool> rightPredicate)
            where TRight : TCommonBase
        {
            return new DoContinuation<TCommonBase, TLeft, TRight>(mDispatcher, mPredicate, rightPredicate);
        }
    }
}