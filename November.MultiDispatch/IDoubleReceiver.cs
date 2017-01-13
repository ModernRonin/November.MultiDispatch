using System;

namespace November.MultiDispatch
{
    interface IDoubleReceiver
    {
        void AddHandler(Type leftType, Type rightType, Action<object, object> action);
    }
}