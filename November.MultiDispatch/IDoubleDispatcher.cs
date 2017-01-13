using System;

namespace November.MultiDispatch
{
    /// <summary>
    /// Defines the contract for dispatching a combination of two argument types to a handler.
    /// </summary>
    /// <typeparam name="TCommonBase">the common base type all arguments derive from; use Object if you do not want to constrain at all</typeparam>
    public interface IDoubleDispatcher<TCommonBase>
    {
        /// <summary>
        /// This is called if <see cref="Dispatch"/> is called for a combination of argument types for which there
        /// has been no specific handler defined.
        /// </summary>
        Action<TCommonBase, TCommonBase> FallbackHandler { get; set; }
        /// <summary>
        /// Fluent definition of the left argument type. Must be followed by <see cref="LeftContinuation{TCommonBase,TLeft}.OnRight{TRight}()"/>
        /// and <see cref="DoContinuation{TLeft,TRight}.Do"/>.
        /// </summary>
        /// <typeparam name="TLeft">the type of the left argument</typeparam>
        LeftContinuation<TCommonBase, TLeft> OnLeft<TLeft>() where TLeft : TCommonBase;
        /// <summary>
        /// Fluent definition of the left argument type with an additional check that must be passed for the
        /// handler to be called.
        /// Must be followed by <see cref="LeftContinuation{TCommonBase,TLeft}.OnRight{TRight}()"/>
        /// and <see cref="DoContinuation{TLeft,TRight}.Do"/>.
        /// </summary>
        /// <typeparam name="TLeft">the type of the left argument</typeparam>
        /// <param name="predicate">check that needs to be passed for the handler to be called</param>
        LeftContinuation<TCommonBase, TLeft> OnLeft<TLeft>(Func<TLeft, bool> predicate) where TLeft : TCommonBase;
        /// <summary>
        /// Registers a handler to be called for a certain combination of types.
        /// </summary>
        void On<TLeft, TRight>(Action<TLeft, TRight> handler) where TLeft : TCommonBase where TRight : TCommonBase;
        /// <summary>
        /// Fluent definition of the right argument type.
        /// Must be followed by <see cref="RightContinuation{TCommonBase,TRight}.OnLeft{TLeft}()"/>
        /// and <see cref="DoContinuation{TLeft,TRight}.Do"/>.
        /// </summary>
        /// <typeparam name="TRight">the type of the right argument</typeparam>
        RightContinuation<TCommonBase, TRight> OnRight<TRight>() where TRight : TCommonBase;
        /// <summary>
        /// Fluent definition of the right argument type with an additional check that must be passed for the
        /// handler to be called.
        /// Must be followed by <see cref="RightContinuation{TCommonBase,TRight}.OnLeft{TLeft}()"/>
        /// and <see cref="DoContinuation{TLeft,TRight}.Do"/>.
        /// </summary>
        /// <typeparam name="TRight">the type of the right argument</typeparam>
        /// <param name="predicate">check that needs to be passed for the handler to be called</param>
        RightContinuation<TCommonBase, TRight> OnRight<TRight>(Func<TRight, bool> predicate) where TRight : TCommonBase;
        /// <summary>
        /// Dispatch a combination of arguments.
        /// </summary>
        void Dispatch(TCommonBase left, TCommonBase right);
    }
}