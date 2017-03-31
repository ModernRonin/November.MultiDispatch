using System;

namespace November.MultiDispatch
{
    public interface ITypesToHandlerMap
    {
        CallContext GetFor(Type left, Type right);
        void Add(Type leftType, Type rightType, CallContext context);
    }
}