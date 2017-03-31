using System;

namespace November.MultiDispatch
{
    public class TypesToContextMap : ITypesToHandlerMap
    {
        public CallContext GetFor(Type left, Type right)
        {
            return null;
        }
        public void Add(Type leftType, Type rightType, CallContext context)
        {
            
        }
    }
}