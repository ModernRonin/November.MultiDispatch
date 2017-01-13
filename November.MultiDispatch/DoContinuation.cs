using System;

namespace November.MultiDispatch
{
    public class DoContinuation<TCommonBase, TLeft, TRight>
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
            mDispatcher.AddHandler(typeof(TLeft),
                typeof(TRight),
                mLeftPredicate.ToUntyped(),
                mRightPredicate.ToUntyped(),
                handler.ToUntyped());
        }
    }
}