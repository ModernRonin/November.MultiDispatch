namespace November.MultiDispatch
{
    public class LeftContinuation<TLeft>
    {
        readonly IDoubleReceiver mDispatcher;
        internal LeftContinuation(IDoubleReceiver dispatcher)
        {
            mDispatcher = dispatcher;
        }
        public DoContinuation<TLeft, TRight> OnRight<TRight>()
        {
            return new DoContinuation<TLeft, TRight>(mDispatcher);
        }
    }
}