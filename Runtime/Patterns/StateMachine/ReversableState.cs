namespace DodoWorkshop.GameKit
{
    /// <summary>
    /// Base implementation for <see cref="IReversableState{TState}"/>
    /// </summary>
    /// <typeparam name="TState">The type of the <see cref="IState{T}"/>(mostly self)</typeparam>
    public abstract class ReversableState<TState> : State<TState>, IReversableState<TState> 
        where TState : ReversableState<TState>
    {
        /// <inheritdoc cref="IReversableState{TState}.PreviousState"/>
        public TState PreviousState { get; private set; }

        /// <inheritdoc cref="IState{TState}.Enter(IStateMachine{TState}, TState)"/>
        public override void Enter(IStateMachine<TState> stateMachine, TState previousState = null)
        {
            PreviousState = previousState;

            base.Enter(stateMachine, previousState);
        }
    }
}
