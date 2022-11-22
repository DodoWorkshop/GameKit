namespace DodoWorkshop.GameKit.Demos.StateMachines
{
    public class PauseState : DemoState
    {
        public override float ProgressSpeed => 0;

        public override void OnEnter()
        {
            Owner.ProgressBar.PlayButton.onClick.AddListener(OnPlayButtonClicked);
            Owner.ProgressBar.StopButton.onClick.AddListener(OnStopButtonClicked);

            Owner.ProgressBar.PlayButton.interactable = true;
            Owner.ProgressBar.StopButton.interactable = true;


            Owner.ProgressBar.ProgressBarFillImage.color = Owner.ProgressBar.PausedColor;
        }

        public override void OnExit()
        {
            Owner.ProgressBar.PlayButton.interactable = false;
            Owner.ProgressBar.StopButton.interactable = false;

            Owner.ProgressBar.PlayButton.onClick.RemoveListener(OnPlayButtonClicked);
            Owner.ProgressBar.StopButton.onClick.RemoveListener(OnStopButtonClicked);

            Owner.ProgressBar.ProgressBarFillImage.color = Owner.ProgressBar.NormalColor;
        }

        private void OnPlayButtonClicked()
        {
            stateMachine.ChangeToPrevious();
        }

        private void OnStopButtonClicked()
        {
            stateMachine.ChangeState(new StopState());
        }
    }
}
