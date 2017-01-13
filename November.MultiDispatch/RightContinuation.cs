namespace November.MultiDispatch
{
    public class RightContinuation<TCommonBase, TRight>
    {
        readonly IDoubleReceiver mDispatcher;
        internal RightContinuation(IDoubleReceiver dispatcher)
        {
            mDispatcher = dispatcher;
        }
        public DoContinuation<TLeft, TRight> OnLeft<TLeft>() where TLeft :TCommonBase
        {
            return new DoContinuation<TLeft, TRight>(mDispatcher);
        }
    }
}