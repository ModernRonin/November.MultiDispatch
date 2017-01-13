using System;

namespace November.MultiDispatch
{
    interface IDoubleReceiver
    {
        void AddHandler(
            Type leftType,
            Type rightType,
            Func<object, bool> leftPredicate,
            Func<object, bool> rightPredicate,
            Action<object, object> action);
    }
}