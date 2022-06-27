namespace DodoWorkshop.GameKit.Demos.StateMachines
{
    public class StopState : DemoState
    {
        public override bool CanBeReversed => false;
        
        public override float ProgressSpeed => 0;

        public override void OnEnter()
        {
            Owner.ProgressBar.PlayButton.onClick.AddListener(OnPlayButtonclicked);
            Owner.ProgressBar.PlayButton.interactable = true;
            Owner.ProgressBar.ProgressBar.value = 0;
        }

        public override void OnExit()
        {
            Owner.ProgressBar.PlayButton.interactable = false;
            Owner.ProgressBar.PlayButton.onClick.RemoveListener(OnPlayButtonclicked);
        }

        private void OnPlayButtonclicked()
        {
            stateMachine.ChangeState(new PlayState());
        }
    }
}
