using UnityEngine;
using UnityEngine.Events;
using System;

namespace DodoWorkshop.GameKit
{
    /// <summary>
    /// A bloc is a class that controls a state. This state can only
    /// be updated by the bloc and the bloc has to send update events
    /// when the data has been updated.
    /// </summary>
    /// <typeparam name="TState">The type of the state</typeparam>
    /// <typeparam name="TCommand">The type of commands used by the bloc</typeparam>
    [Serializable]
    public abstract class Bloc<TCommand, TState> : MonoBehaviour, IBloc<TCommand, TState> where TState : new()
	{
        protected delegate TState StateUpdateFunction(TState state);

        [SerializeField]
        [Tooltip("If true, the provided initial state will be used to initiatlize this bloc")]
        private bool initializeState;

        [SerializeField]
        [Tooltip("The initial value of the state")]
        private TState initialState;

        [SerializeField]
        private TState state;
        
        
        /// <summary>
        /// The current value of the state
        /// </summary>
        public TState State { get => state; }

        /// <summary>
        /// This event is called each time the state is updated
        /// </summary>
        public UnityAction<BlocUpdate<TCommand, TState>> OnStateUpdated { get; set; }

        /// <summary>
        /// Call this method to initialize the state with the provided
        /// value
        /// </summary>
        /// <param name="initialState">The initial state value</param>
        public virtual void InitializeState(TState initialState)
        {
            this.initialState = initialState;
            UpdateState(state => this.initialState);
        }

        /// <summary>
        /// Resets the state to its last initial value
        /// </summary>
        public virtual void ResetState()
        {
            UpdateState(state => initialState);
        }

        /// <summary>
        /// Tells the <see cref="Bloc{TCommand, TState}"/> to execute the provided command
        /// </summary>
        /// <param name="command">The command to execute</param>
        public virtual void Send(TCommand command)
        {
            OnCommandReceived(command);
        }

        protected virtual void UpdateState(StateUpdateFunction updateFunction, TCommand command = default)
        {
            try {
                state = updateFunction(state);
                OnStateUpdated?.Invoke(BlocUpdate<TCommand,TState>.Ok(command, state));
            }
            catch (Exception e)
            {
                OnStateUpdated?.Invoke(BlocUpdate <TCommand, TState>.Ko(command, state, e));
            }
        }

        protected abstract void OnCommandReceived(TCommand command);

        protected virtual void Awake()
        {
            InitializeState(initializeState ? initialState : new TState());
        }
    }
}
