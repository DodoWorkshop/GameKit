using System;
using UnityEngine;
using UnityEngine.Events;

namespace DodoWorkshop.GameKit
{
    [AddComponentMenu(PathConstants.ComponentMenu.BASE_PATH + "/State Machine/State Machine")]
    public class StateMachine : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField]
        [Tooltip("If provided, the container's events will be bound to this state machine events")]
        protected StateMachineEventContainer eventContainer;


        /// <summary>
        /// The current <see cref="State"/> of the <see cref="StateMachine"/>
        /// </summary>
        public State CurrentState { get; private set; }

        /// <summary>
        /// This event is called when the <see cref="StateMachine"/>'s <see cref="State"/> has changed.
        /// The first argument is the previous <see cref="State"/>, the second one is the new <see cref="State"/>
        /// </summary>
        public UnityAction<State, State> OnStateChanged { get; set; }

        /// <summary>
        /// This event is called when the <see cref="StateMachine"/> enters in its first <see cref="State"/>
        /// </summary>
        public UnityAction OnStateMachineStarted { get; set; }

        /// <summary>
        /// This event is called when the <see cref="StateMachine"/> is stopped
        /// </summary>
        public UnityAction OnStateMachineStopped { get; set; }

        /// <summary>
        /// The type of state used by the state machine
        /// </summary>
        public virtual Type StateType => CurrentState.GetType();

        /// <summary>
        /// If true, you can use the <see cref="ChangeToPrevious"/> method from this <see cref="State"/>
        /// </summary>
        public bool CanBeReversed => CurrentState is ReversableState reversable && reversable.CanBeReversed;

        /// <summary>
        /// True if the <see cref="StateMachine"/> has no current <see cref="State"/>
        /// </summary>
        public bool IsStopped => CurrentState == null;

        /// <summary>
        /// Changes the current <see cref="State"/> of this <see cref="StateMachine"/>
        /// </summary>
        /// <param name="state">The new <see cref="State"/></param>
        public virtual void ChangeState(State state)
        {
            if (CurrentState != null)
            {
                CurrentState.Exit();
            }

            State lastState = CurrentState;
            CurrentState = state;

            if (CurrentState != null)
            {
                if (lastState == null)
                {
                    OnStateMachineStarted?.Invoke();
                }

                CurrentState.Enter(this, lastState);
            }
            else
            {
                OnStateMachineStopped?.Invoke();
            }

            OnStateChanged(lastState, CurrentState);
        }

        /// <summary>
        /// Go to previous <see cref="State"/> if the current <see cref="State"/> is reversable
        /// </summary>
        /// <exception cref="Exception">Thrown if the <see cref="State"/> can't be reversed</exception>
        public virtual void ChangeToPrevious()
        {
            if (CurrentState is ReversableState reversable && reversable.CanBeReversed)
            {
                ChangeState(reversable.PreviousState);
            }
            else
            {
                throw new Exception($"A state of type {CurrentState.GetType()} cannot be reversed");
            }
        }

        /// <summary>
        /// Stops the current state machine
        /// </summary>
        public void Stop()
        {
            ChangeState(null);
        }

        protected virtual void Awake()
        {
            eventContainer?.Bind(this);
        }
    }
    
    /// <summary>
    /// A constrained implementation of <see cref="StateMachine"/>
    /// </summary>
    /// <typeparam name="TState">The type of <see cref="State"/> used by the state machine</typeparam>
    public class StateMachine<TState> : StateMachine where TState : State
    {
        /// <inheritdoc cref="StateMachine.CurrentState"/>
        public new TState CurrentState { get; private set; }

        /// <inheritdoc cref="StateMachine.OnStateChanged"/>
        public new UnityAction<TState, TState> OnStateChanged { get; set; }

        /// <inheritdoc cref="StateMachine.StateType"/>
        public override Type StateType => typeof(TState);

        /// <inheritdoc cref="StateMachine.ChangeState(State)"/>
        public void ChangeState(TState state)
        {
            base.ChangeState(state);
        }

        /// <inheritdoc cref="StateMachine.ChangeState(State)"/>
        public override void ChangeState(State state)
        {
            if (state is TState)
            {
                ChangeState((TState) state);
            }
            else
            {
                throw new Exception($"Can't change state to state of type {state.GetType().Name}, this StateMachine only accepts {StateType.Name}");
            }
        }
    }
}
