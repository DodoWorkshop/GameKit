using UnityEngine.Events;

namespace DodoWorkshop.GameKit
{
    /// <summary>
    /// Base implementation for <see cref="IState{TState}"/>
    /// </summary>
    /// <typeparam name="TState">The type of the <see cref="IState{T}"/>(mostly self)</typeparam>
    public abstract class State<TState> : IState<TState> where TState : State<TState>
    {
        protected IStateMachine<TState> stateMachine;

        /// <summary>
        /// This event is thrown when entering in this state
        /// </summary>
        public UnityAction<TState> OnStateEnter { get; set; }

        /// <summary>
        /// This event is thrown when exiting this state
        /// </summary>
        public UnityAction<TState> OnStateExit { get; set; }

        /// <inheritdoc cref="IState{TState}.Enter(IStateMachine{TState}, TState)"/>
        public virtual void Enter(IStateMachine<TState> stateMachine, TState previousState = null)
        {
            this.stateMachine = stateMachine;
            OnEnter();
            OnStateEnter?.Invoke(this as TState);
        }

        /// <inheritdoc cref="IState{TState}.Exit()"/>
        public virtual void Exit()
        {
            OnExit();
            OnStateExit?.Invoke(this as TState);
        }

        /// <summary>
        /// This method is called when entering in this state. It
        /// has to be implemented to customize children behavior.
        /// </summary>
        public abstract void OnEnter();

        /// <summary>
        /// This method is called when exiting this state. It
        /// has to be implemented to customize children behavior.
        /// </summary>
        public abstract void OnExit();
    }
}
