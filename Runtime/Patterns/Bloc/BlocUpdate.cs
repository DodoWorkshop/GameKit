using System;

namespace DodoWorkshop.GameKit
{
    /// <summary>
    /// This object represents an update of an <see cref="IBloc"/>. From this
    /// update you can get the used command, the new state and if a exception occurred.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command</typeparam>
    /// <typeparam name="TState">The type of the state</typeparam>
	public struct BlocUpdate<TCommand, TState> where TState : new()
    {
        /// <summary>
        /// The state ater the update
        /// </summary>
        public TState State { get; private set; }

        /// <summary>
        /// The command used to update the state
        /// </summary>
        public TCommand Command { get; private set; }

        /// <summary>
        /// The exception that occurred during the update (null if none)
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// True if an exception occurred
        /// </summary>
        public bool IsErrored => Exception != null;

        private BlocUpdate(TCommand command, TState state, Exception exception)
        {
            State = state;
            Command = command;
            Exception = exception;
        }

	    public static BlocUpdate<TCommand, TState> Ok(TCommand command, TState state)
        {
            return new BlocUpdate<TCommand, TState>(command, state, null);
        }

        public static BlocUpdate<TCommand, TState> Ko(TCommand command, TState state, Exception exception)
        {
            return new BlocUpdate<TCommand, TState>(command, state, exception);
        }
    }
}
