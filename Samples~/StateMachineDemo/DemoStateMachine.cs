using UnityEngine;

namespace DodoWorkshop.GameKit.Demos.StateMachines
{
    public class DemoStateMachine : StateMachine<DemoState>
    {
        [Header("References")]
        [SerializeField]
        private DemoProgressBar progressBar;


        public DemoProgressBar ProgressBar => progressBar;


        protected override void Awake()
        {
            base.Awake();

            OnStateMachineStarted += progressBar.ResetAll;
            progressBar.OnProgressBarDone += OnProgressBarDone;
        }

        private void OnProgressBarDone()
        {
            ChangeState(new DoneState());
        }

        private void Start()
        {
            // Starts the state machine
            ChangeState(new StopState());
        }
    }
}
