namespace November.MultiDispatch
{
    public class LeftContinuation<TLeft>
    {
        readonly DoubleDispatcher mDispatcher;
        public LeftContinuation(DoubleDispatcher dispatcher)
        {
            mDispatcher = dispatcher;
        }
        public RightContinuation<TLeft, TRight> OnRight<TRight>()
        {
            return new RightContinuation<TLeft, TRight>(mDispatcher);
        }
    }
}