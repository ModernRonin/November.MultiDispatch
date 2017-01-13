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
        /// <summary>
        /// Fluently specify the left argument type. To be followed by <see cref="DoContinuation{TLeft,TRight}.Do"/>.
        /// </summary>
        /// <typeparam name="TLeft">the type of the left argument</typeparam>
        public DoContinuation<TLeft, TRight> OnLeft<TLeft>() where TLeft : TCommonBase
        {
            return new DoContinuation<TLeft, TRight>(mDispatcher, AlwaysTrue<TLeft>(), mPredicate);
        }
        /// <summary>
        /// Fluently specify the left argument type with an additional check that needs to be passed.
        /// To be followed by <see cref="DoContinuation{TLeft,TRight}.Do"/>.
        /// </summary>
        /// <typeparam name="TLeft">the type of the left argument</typeparam>
        /// <param name="leftPredicate">the check to be passed</param>
        public DoContinuation<TLeft, TRight> OnLeft<TLeft>(Func<TLeft, bool> leftPredicate) where TLeft : TCommonBase
        {
            return new DoContinuation<TLeft, TRight>(mDispatcher, leftPredicate, mPredicate);
        }
    }
}