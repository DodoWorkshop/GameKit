namespace DodoWorkshop.GameKit
{
    /// <summary>
    /// The interface for every state machine
    /// </summary>
    /// <typeparam name="TState">The type of state used by the state machine</typeparam>
    public interface IStateMachine<TState> where TState : IState<TState>
    {
        /// <summary>
        /// The state machine's current state
        /// </summary>
        TState CurrentState { get; }

        /// <summary>
        /// Method to call to change the state of the state machine
        /// </summary>
        /// <param name="state">The new state</param>
        void ChangeState(TState state);
    }
}
