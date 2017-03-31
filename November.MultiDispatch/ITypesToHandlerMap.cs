using System;

namespace November.MultiDispatch
{
    /// <summary>
    /// Defines the contract for storing and looking up mappings to type-pairs to <see cref="CallContext"/>s.
    /// </summary>
    public interface ITypesToHandlerMap
    {
        /// <summary>
        /// Gets the callcontext that was stored for a certain type-pair.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        CallContext GetFor(Type left, Type right);
        /// <summary>
        /// Adds a mapping of a type-pair to a callcontext.
        /// </summary>
        /// <param name="leftType"></param>
        /// <param name="rightType"></param>
        /// <param name="context"></param>
        void Add(Type leftType, Type rightType, CallContext context);
    }
}