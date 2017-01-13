namespace November.MultiDispatch
{
    public class RightContinuation<TRight>
    {
        readonly DoubleDispatcher mDispatcher;
        public RightContinuation(DoubleDispatcher dispatcher)
        {
            mDispatcher = dispatcher;
        }
        public DoContinuation<TLeft, TRight> OnLeft<TLeft>()
        {
            return new DoContinuation<TLeft, TRight>(mDispatcher);
        }
    }
}