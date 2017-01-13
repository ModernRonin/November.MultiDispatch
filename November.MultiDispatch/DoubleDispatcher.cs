using System;
using System.Collections.Generic;

namespace November.MultiDispatch
{
    public class DoubleDispatcher
    {
        readonly Dictionary<Type, Dictionary<Type, Action<object, object>>> mHandlers =
            new Dictionary<Type, Dictionary<Type, Action<object, object>>>();
        public Action<object, object> FallbackHandler { get; set; }
        public LeftContinuation<TLeft> OnLeft<TLeft>()
        {
            return new LeftContinuation<TLeft>(this);
        }
        public void On<TLeft, TRight>(Action<TLeft, TRight> handler)
        {
            AddHandler(typeof(TLeft), typeof(TRight), handler.ToUntypedAction());
        }
        public RightContinuation<TRight> OnRight<TRight>()
        {
            return new RightContinuation<TRight>(this);
        }
        public void Dispatch(object left, object right)
        {
            var leftType = left.GetType();
            var rightType = right.GetType();
            if (!mHandlers.ContainsKey(leftType)) InvokeFallbackHandler(left, right);
            else
            {
                var leftHandlers = mHandlers[leftType];
                if (!leftHandlers.ContainsKey(rightType)) InvokeFallbackHandler(left, right);
                else leftHandlers[rightType](left, right);
            }
        }
        void InvokeFallbackHandler(object left, object right)
        {
            if (null == FallbackHandler)
                throw new InvalidOperationException(
                    $"You must either take care to define handlers for all permutations that might come in; or define '{nameof(FallbackHandler)}'.");
            FallbackHandler(left, right);
        }
        internal void AddHandler(Type leftType, Type rightType, Action<object, object> action)
        {
            if (!mHandlers.ContainsKey(leftType)) mHandlers[leftType] = new Dictionary<Type, Action<object, object>>();
            var leftHandlers = mHandlers[leftType];
            leftHandlers[rightType] = action;
        }
    }
}