namespace DodoWorkshop.GameKit
{
    /// <summary>
    /// Interface for a <see cref="IState{T}"/> that can return to previous one
    /// </summary>
    /// <typeparam name="TState">The type of the <see cref="IState{T}"/>(mostly self)</typeparam>
	public interface IReversableState<TState> : IState<TState> where TState : IState<TState>
    {
        /// <summary>
        /// The previous state from this state (null if there was no state before)
        /// </summary>
	    TState PreviousState { get; }
	}
}
