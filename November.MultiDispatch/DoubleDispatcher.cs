using System;
using System.Collections.Generic;
using static November.MultiDispatch.Predicates;

namespace November.MultiDispatch
{
    public class DoubleDispatcher<TCommonBase> : IDoubleReceiver, IDoubleDispatcher<TCommonBase>
    {
        readonly Dictionary<Type, Dictionary<Type, CallContext>> mHandlers =
            new Dictionary<Type, Dictionary<Type, CallContext>>();
        public Action<TCommonBase, TCommonBase> FallbackHandler { get; set; }
        public void AddHandler(
            Type leftType,
            Type rightType,
            Func<object, bool> leftPredicate,
            Func<object, bool> rightPredicate,
            Action<object, object> action)
        {
            if (!mHandlers.ContainsKey(leftType)) mHandlers[leftType] = new Dictionary<Type, CallContext>();
            var leftHandlers = mHandlers[leftType];
            leftHandlers[rightType] = new CallContext
            {
                Handler = action,
                LeftPredicate = leftPredicate,
                RightPredicate = rightPredicate
            };
        }
        public LeftContinuation<TCommonBase, TLeft> OnLeft<TLeft>() where TLeft : TCommonBase
        {
            return new LeftContinuation<TCommonBase, TLeft>(this);
        }
        public LeftContinuation<TCommonBase, TLeft> OnLeft<TLeft>(Func<TLeft, bool> predicate) where TLeft : TCommonBase
        {
            return new LeftContinuation<TCommonBase, TLeft>(this, predicate);
        }
        public void On<TLeft, TRight>(Action<TLeft, TRight> handler) where TLeft : TCommonBase
            where TRight : TCommonBase
        {
            AddHandler(typeof(TLeft), typeof(TRight), AlwaysTrue<object>(), AlwaysTrue<object>(), handler.ToUntyped());
        }
        public RightContinuation<TCommonBase, TRight> OnRight<TRight>() where TRight : TCommonBase
        {
            return new RightContinuation<TCommonBase, TRight>(this);
        }
        public RightContinuation<TCommonBase, TRight> OnRight<TRight>(Func<TRight, bool> predicate)
            where TRight : TCommonBase
        {
            return new RightContinuation<TCommonBase, TRight>(this, predicate);
        }
        public void Dispatch(TCommonBase left, TCommonBase right)
        {
            var leftType = left.GetType();
            var rightType = right.GetType();
            if (!mHandlers.ContainsKey(leftType)) InvokeFallbackHandler(left, right);
            else
            {
                var leftHandlers = mHandlers[leftType];
                if (!leftHandlers.ContainsKey(rightType)) InvokeFallbackHandler(left, right);
                else leftHandlers[rightType].Invoke(left, right);
            }
        }
        void InvokeFallbackHandler(TCommonBase left, TCommonBase right)
        {
            if (null == FallbackHandler)
                throw new InvalidOperationException(
                    $"You must either take care to define handlers for all permutations that might come in; or define '{nameof(FallbackHandler)}'.");
            FallbackHandler(left, right);
        }
    }
}