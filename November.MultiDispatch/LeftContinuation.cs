namespace November.MultiDispatch
{
    public class LeftContinuation<TLeft>
    {
        readonly DoubleDispatcher mDispatcher;
        public LeftContinuation(DoubleDispatcher dispatcher)
        {
            mDispatcher = dispatcher;
        }
        public DoContinuation<TLeft, TRight> OnRight<TRight>()
        {
            return new DoContinuation<TLeft, TRight>(mDispatcher);
        }
    }
}