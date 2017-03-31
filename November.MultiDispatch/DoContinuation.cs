using System;

namespace November.MultiDispatch
{
    /// <summary>
    /// Part of the fluent interface of <see cref="DoubleDispatcher{TCommonBase}"/>.
    /// </summary>
    /// <typeparam name="TCommonBase"></typeparam>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    public sealed class DoContinuation<TCommonBase, TLeft, TRight> where TLeft : TCommonBase where TRight : TCommonBase
    {
        readonly DoubleDispatcher<TCommonBase> mDispatcher;
        readonly Func<TLeft, bool> mLeftPredicate;
        readonly Func<TRight, bool> mRightPredicate;
        internal DoContinuation(
            DoubleDispatcher<TCommonBase> dispatcher,
            Func<TLeft, bool> leftPredicate,
            Func<TRight, bool> rightPredicate)
        {
            mDispatcher = dispatcher;
            mLeftPredicate = leftPredicate;
            mRightPredicate = rightPredicate;
        }
        /// <summary>
        /// Specifies the handler
        /// </summary>
        /// <param name="handler"></param>
        public void Do(Action<TLeft, TRight> handler)
        {
            mDispatcher.AddHandler(mLeftPredicate, mRightPredicate, handler);
        }
    }
}