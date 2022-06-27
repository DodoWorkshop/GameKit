using UnityEngine.Events;

namespace DodoWorkshop.GameKit
{
    /// <summary>
    /// The main class for every state used by a <see cref="StateMachine"/>
    /// </summary>
    public abstract class State
    {
        protected StateMachine stateMachine;

        /// <summary>
        /// This event is thrown when entering in this state
        /// </summary>
        public UnityAction<State> OnStateEnter { get; set; }

        /// <summary>
        /// This event is thrown when exiting this state
        /// </summary>
        public UnityAction<State> OnStateExit { get; set; }

        /// <summary>
        /// This method is called by the <see cref="StateMachine"/> when changing to this <see cref="State"/>
        /// </summary>
        /// <param name="stateMachine">The ref to the owning <see cref="StateMachine"/></param>
        /// <param name="previousState">The previous <see cref="State"/> if exists</param>
        public virtual void Enter(StateMachine stateMachine, State previousState = null)
        {
            this.stateMachine = stateMachine;
            OnEnter();
            OnStateEnter?.Invoke(this);
        }

        /// <summary>
        /// This method is called by the state machine before changing to another state
        /// </summary>
        public virtual void Exit()
        {
            OnExit();
            OnStateExit?.Invoke(this);
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
