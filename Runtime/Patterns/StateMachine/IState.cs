using UnityEngine;

namespace DodoWorkshop.GameKit
{
    /// <summary>
    /// The main class for every state used by <see cref="StateMachine{TState}"/>
    /// </summary>
    /// <typeparam name="TState">The type of the <see cref="IState{T}"/>(mostly self)</typeparam>
	public interface IState<T> where T : IState<T>
	{
        /// <summary>
        /// This method is called by the state machine when changing to this state
        /// </summary>
        /// <param name="stateMachine">The ref to the owning <see cref="IStateMachine{T}"/></param>
        /// <param name="previousState">The previous state if exists</param>
        void Enter(IStateMachine<T> stateMachine, T previousState = default);

        /// <summary>
        /// This method is called by the state machine before changing to another state
        /// </summary>
        void Exit();
	}
}
