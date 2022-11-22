namespace DodoWorkshop.GameKit.Demos.StateMachines
{
    public abstract class DemoState : ReversableState
    {

        public abstract float ProgressSpeed { get; }

        // Cast state machine
        protected DemoStateMachine Owner => stateMachine as DemoStateMachine;
    }
}
