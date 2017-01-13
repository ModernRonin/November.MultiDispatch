namespace November.MultiDispatch
{
    public class RightContinuation<TRight>
    {
        readonly IDoubleReceiver mDispatcher;
        internal RightContinuation(IDoubleReceiver dispatcher)
        {
            mDispatcher = dispatcher;
        }
        public DoContinuation<TLeft, TRight> OnLeft<TLeft>()
        {
            return new DoContinuation<TLeft, TRight>(mDispatcher);
        }
    }
}