using System;

namespace November.MultiDispatch
{
    /// <summary>
    /// Defines the contract for storing and looking up mappings to type-pairs to <see cref="CallContext"/>s.
    /// </summary>
    public interface ITypesToHandlerMap
    {
        CallContext GetFor(Type left, Type right);
        void Add(Type leftType, Type rightType, CallContext context);
    }
}