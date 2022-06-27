namespace DodoWorkshop.GameKit
{
    /// <summary>
    /// Base class for a <see cref="State"/> that can return to previous one
    /// </summary>
    public abstract class ReversableState : State
    {
        /// <summary>
        /// The previous <see cref="State"/> from this state (null if there was no state before)
        /// </summary>
        public State PreviousState { get; private set; }

        /// <summary>
        /// True if this <see cref="State"/> can be reversed (override it with your own implementation)
        /// </summary>
        public virtual bool CanBeReversed => true;

        /// <inheritdoc cref="State.Enter(StateMachine, State)"/>
        public override void Enter(StateMachine stateMachine, State previousState = null)
        {
            PreviousState = previousState;

            base.Enter(stateMachine, previousState);
        }
    }
}
