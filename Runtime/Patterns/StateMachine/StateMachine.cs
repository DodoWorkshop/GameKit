using System;
using UnityEngine.Events;

namespace DodoWorkshop.GameKit
{
    /// <summary>
    /// Base implementation for <see cref="IStateMachine{TState}"/>
    /// </summary>
    /// <typeparam name="TState">The type of state used by the state machine</typeparam>
    public class StateMachine<TState> : IStateMachine<TState> 
        where TState : IState<TState>
    {
        /// <inheritdoc cref="IStateMachine{TState}.CurrentState"/>
        public TState CurrentState { get; private set; }

        /// <summary>
        /// This event is called when the state machine's state has changed.
        /// The first argument is the previous state, the second one is the
        /// new state
        /// </summary>
        public UnityAction<TState, TState> OnStateChanged { get; set; }

        /// <summary>
        /// The type of state used by the state machine
        /// </summary>
        public Type StateType => typeof(TState);

        /// <inheritdoc cref="IStateMachine{TState}.ChangeState(TState)"/>
        public void ChangeState(TState state)
        {
            if (CurrentState != null)
            {
                CurrentState.Exit();
            }

            TState lastState = CurrentState;
            CurrentState = state;

            if (CurrentState != null)
            {
                CurrentState.Enter(this, lastState);
            }

            OnStateChanged(lastState, CurrentState);
        }
    }
}
