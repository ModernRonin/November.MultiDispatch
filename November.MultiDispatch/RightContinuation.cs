using System;
using static November.MultiDispatch.Predicates;

namespace November.MultiDispatch
{
    public class RightContinuation<TCommonBase, TRight> where TRight : TCommonBase
    {
        readonly DoubleDispatcher<TCommonBase> mDispatcher;
        readonly Func<TRight, bool> mPredicate;
        internal RightContinuation(DoubleDispatcher<TCommonBase> dispatcher) : this(dispatcher, AlwaysTrue<TRight>()) {}
        internal RightContinuation(DoubleDispatcher<TCommonBase> dispatcher, Func<TRight, bool> predicate)
        {
            mDispatcher = dispatcher;
            mPredicate = predicate;
        }
        /// <summary>
        /// Fluently specify the left argument type. To be followed by <see cref="DoContinuation{TCommonBase, TLeft,TRight}.Do"/>.
        /// </summary>
        /// <typeparam name="TLeft">the type of the left argument</typeparam>
        public DoContinuation<TCommonBase, TLeft, TRight> OnLeft<TLeft>() where TLeft : TCommonBase
        {
            return new DoContinuation<TCommonBase, TLeft, TRight>(mDispatcher, AlwaysTrue<TLeft>(), mPredicate);
        }
        /// <summary>
        /// Fluently specify the left argument type with an additional check that needs to be passed.
        /// To be followed by <see cref="DoContinuation{TCommonBase, TLeft,TRight}.Do"/>.
        /// </summary>
        /// <typeparam name="TLeft">the type of the left argument</typeparam>
        /// <param name="leftPredicate">the check to be passed</param>
        public DoContinuation<TCommonBase, TLeft, TRight> OnLeft<TLeft>(Func<TLeft, bool> leftPredicate) where TLeft : TCommonBase
        {
            return new DoContinuation<TCommonBase, TLeft, TRight>(mDispatcher, leftPredicate, mPredicate);
        }
    }
}