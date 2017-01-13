namespace November.MultiDispatch
{
    public class LeftContinuation<TLeft>
    {
        readonly Dispatcher mDispatcher;
        public LeftContinuation(Dispatcher dispatcher)
        {
            mDispatcher = dispatcher;
        }
        public RightContinuation<TLeft, TRight> OnRight<TRight>()
        {
            return new RightContinuation<TLeft, TRight>(mDispatcher);
        }
    }
}