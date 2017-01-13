namespace November.MultiDispatch
{
    public class LeftContinuation<TCommonBase, TLeft>
    {
        readonly IDoubleReceiver mDispatcher;
        internal LeftContinuation(IDoubleReceiver dispatcher)
        {
            mDispatcher = dispatcher;
        }
        public DoContinuation<TLeft, TRight> OnRight<TRight>() where TRight: TCommonBase
        {
            return new DoContinuation<TLeft, TRight>(mDispatcher);
        }
    }
}