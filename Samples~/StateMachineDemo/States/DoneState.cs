using UnityEngine.UI;

namespace DodoWorkshop.GameKit.Demos.StateMachines
{
    public class DoneState : DemoState
    {
        public override bool CanBeReversed => false;

        public override float ProgressSpeed => 0;


        private Text stopButtonText;
        

        public override void OnEnter()
        {
            stopButtonText = Owner.ProgressBar.StopButton.GetComponentInChildren<Text>();
            stopButtonText.text = "Reset";

            Owner.ProgressBar.StopButton.onClick.AddListener(OnStopButtonclicked);
            Owner.ProgressBar.StopButton.interactable = true;
            
            Owner.ProgressBar.ProgressBarFillImage.color = Owner.ProgressBar.FinishedColor;
        }

        public override void OnExit()
        {
            stopButtonText.text = "Stop";

            Owner.ProgressBar.StopButton.interactable = false;
            Owner.ProgressBar.StopButton.onClick.RemoveListener(OnStopButtonclicked);
            
            Owner.ProgressBar.ProgressBarFillImage.color = Owner.ProgressBar.NormalColor;
        }

        private void OnStopButtonclicked()
        {
            stateMachine.ChangeState(new StopState());
        }
    }
}
