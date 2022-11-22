namespace DodoWorkshop.GameKit.Demos.StateMachines
{
    public class PlayState : DemoState
    {
        public override float ProgressSpeed => 1;

        public override void OnEnter()
        {
            Owner.ProgressBar.PauseButton.onClick.AddListener(OnPauseButtonClicked);
            Owner.ProgressBar.StopButton.onClick.AddListener(OnStopButtonClicked);
            Owner.ProgressBar.SpeedUpButton.onClick.AddListener(OnSpeedUpButtonClicked);

            Owner.ProgressBar.PauseButton.interactable = true;
            Owner.ProgressBar.StopButton.interactable = true;
            Owner.ProgressBar.SpeedUpButton.interactable = true;
        }

        public override void OnExit()
        {
            Owner.ProgressBar.PauseButton.interactable = false;
            Owner.ProgressBar.StopButton.interactable = false;
            Owner.ProgressBar.SpeedUpButton.interactable = false;

            Owner.ProgressBar.PauseButton.onClick.RemoveListener(OnPauseButtonClicked);
            Owner.ProgressBar.StopButton.onClick.RemoveListener(OnStopButtonClicked);
            Owner.ProgressBar.SpeedUpButton.onClick.RemoveListener(OnSpeedUpButtonClicked);
        }

        private void OnPauseButtonClicked() {
            stateMachine.ChangeState(new PauseState());
        }

        private void OnStopButtonClicked()
        {
            stateMachine.ChangeState(new StopState());
        }

        private void OnSpeedUpButtonClicked()
        {
            stateMachine.ChangeState(new SpeedUpState());
        }
    }
}
