using System;

namespace November.MultiDispatch
{
    public interface IDoubleDispatcher<TCommonBase>
    {
        Action<TCommonBase, TCommonBase> FallbackHandler { get; set; }
        LeftContinuation<TCommonBase, TLeft> OnLeft<TLeft>() where TLeft : TCommonBase;
        LeftContinuation<TCommonBase, TLeft> OnLeft<TLeft>(Func<TLeft, bool> predicate) where TLeft : TCommonBase;
        void On<TLeft, TRight>(Action<TLeft, TRight> handler) where TLeft : TCommonBase where TRight : TCommonBase;
        RightContinuation<TCommonBase, TRight> OnRight<TRight>() where TRight : TCommonBase;
        RightContinuation<TCommonBase, TRight> OnRight<TRight>(Func<TRight, bool> predicate) where TRight : TCommonBase;
        void Dispatch(TCommonBase left, TCommonBase right);
    }
}