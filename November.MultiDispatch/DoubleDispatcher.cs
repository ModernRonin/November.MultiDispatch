using System;
using static November.MultiDispatch.Predicates;

namespace November.MultiDispatch
{
    /// <summary>
    /// An implementation of <see cref="IDoubleDispatcher{TCommonBase}"/>.
    /// In this implementation, the order in which handlers are defined, is relevant:
    /// the first match wins, in calls to <see cref="Dispatch"/>.
    /// </summary>
    /// <typeparam name="TCommonBase"></typeparam>
    public sealed class DoubleDispatcher<TCommonBase> : IDoubleDispatcher<TCommonBase>
    {
        readonly ITypesToHandlerMap mTypesToHandlers;
        /// <summary>
        /// Constructs an instance with a custom storage/lookup mechanism for type-pairs to handlers.
        /// </summary>
        /// <param name="typesToHandlers"></param>
        public DoubleDispatcher(ITypesToHandlerMap typesToHandlers)
        {
            mTypesToHandlers = typesToHandlers;
        }
        /// <summary>
        /// Constructs an instance with the default storage/lookup mechanism for type-pairs to handlers.
        /// </summary>
        public DoubleDispatcher() : this(new TypesToContextMap()) {}
        /// <summary>
        /// This is called if <see cref="Dispatch"/> is called for a combination of argument types for which there
        /// has been no specific handler defined.
        /// </summary>
        public Action<TCommonBase, TCommonBase> FallbackHandler { get; set; }
        /// <summary>
        /// Fluent definition of the left argument type. Must be followed by <see cref="LeftContinuation{TCommonBase,TLeft}.OnRight{TRight}()"/>
        /// and <see cref="DoContinuation{TCommonBase, TLeft,TRight}.Do"/>.
        /// </summary>
        /// <typeparam name="TLeft">the type of the left argument</typeparam>
        public LeftContinuation<TCommonBase, TLeft> OnLeft<TLeft>() where TLeft : TCommonBase
        {
            return new LeftContinuation<TCommonBase, TLeft>(this);
        }
        /// <summary>
        /// Fluent definition of the left argument type with an additional check that must be passed for the
        /// handler to be called.
        /// Must be followed by <see cref="LeftContinuation{TCommonBase,TLeft}.OnRight{TRight}()"/>
        /// and <see cref="DoContinuation{TCommonBase, TLeft,TRight}.Do"/>.
        /// </summary>
        /// <typeparam name="TLeft">the type of the left argument</typeparam>
        /// <param name="predicate">check that needs to be passed for the handler to be called</param>
        public LeftContinuation<TCommonBase, TLeft> OnLeft<TLeft>(Func<TLeft, bool> predicate) where TLeft : TCommonBase
        {
            return new LeftContinuation<TCommonBase, TLeft>(this, predicate);
        }
        /// <summary>
        /// Registers a handler to be called for a certain combination of types.
        /// </summary>
        public void On<TLeft, TRight>(Action<TLeft, TRight> handler) where TLeft : TCommonBase
            where TRight : TCommonBase
        {
            AddHandler(typeof(TLeft), typeof(TRight), AlwaysTrue<object>(), AlwaysTrue<object>(), handler.ToUntyped());
        }
        /// <summary>
        /// Fluent definition of the right argument type.
        /// Must be followed by <see cref="RightContinuation{TCommonBase,TRight}.OnLeft{TLeft}()"/>
        /// and <see cref="DoContinuation{TCommonBase, TLeft,TRight}.Do"/>.
        /// </summary>
        /// <typeparam name="TRight">the type of the right argument</typeparam>
        public RightContinuation<TCommonBase, TRight> OnRight<TRight>() where TRight : TCommonBase
        {
            return new RightContinuation<TCommonBase, TRight>(this);
        }
        /// <summary>
        /// Fluent definition of the right argument type with an additional check that must be passed for the
        /// handler to be called.
        /// Must be followed by <see cref="RightContinuation{TCommonBase,TRight}.OnLeft{TLeft}()"/>
        /// and <see cref="DoContinuation{TCommonBase, TLeft,TRight}.Do"/>.
        /// </summary>
        /// <typeparam name="TRight">the type of the right argument</typeparam>
        /// <param name="predicate">check that needs to be passed for the handler to be called</param>
        public RightContinuation<TCommonBase, TRight> OnRight<TRight>(Func<TRight, bool> predicate)
            where TRight : TCommonBase
        {
            return new RightContinuation<TCommonBase, TRight>(this, predicate);
        }
        /// <summary>
        /// Dispatch a combination of arguments. If no handler matching the arguments can be found,
        /// <see cref="FallbackHandler"/> is used. If multiple handlers are found -for example, because there
        /// were two calls to <see cref="On{TLeft,TRight}"/>, one with concrete types and one with interfaces implemented by these concrete types-, 
        /// then the first registered handler is used. 
        /// </summary>
        /// <exception cref="InvalidOperationException">thrown if no matching handlers are found and <see cref="FallbackHandler"/> is not set</exception>
        public void Dispatch(TCommonBase left, TCommonBase right)
        {
            void invokeFallback() => InvokeFallbackHandler(left, right);
            var leftType = left.GetType();
            var rightType = right.GetType();

            var context = mTypesToHandlers.GetFor(leftType, rightType);
            if (null == context) invokeFallback();
            else context.Invoke(left, right);
        }
        internal void AddHandler<TLeft, TRight>(
            Func<TLeft, bool> leftPredicate,
            Func<TRight, bool> rightPredicate,
            Action<TLeft, TRight> action) where TLeft : TCommonBase where TRight : TCommonBase
        {
            AddHandler(typeof(TLeft),
                typeof(TRight),
                leftPredicate.ToUntyped(),
                rightPredicate.ToUntyped(),
                action.ToUntyped());
        }
        void AddHandler(
            Type leftType,
            Type rightType,
            Func<object, bool> leftPredicate,
            Func<object, bool> rightPredicate,
            Action<object, object> action)
        {
            mTypesToHandlers.Add(leftType,
                rightType,
                new CallContext {Handler = action, LeftPredicate = leftPredicate, RightPredicate = rightPredicate});
        }
        void InvokeFallbackHandler(TCommonBase left, TCommonBase right)
        {
            if (null == FallbackHandler)
                throw new InvalidOperationException(
                    $"You must either take care to define handlers for all permutations that might come in; or define '{nameof(FallbackHandler)}'.");
            FallbackHandler(left, right);
        }
    }
}