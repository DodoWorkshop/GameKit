using UnityEngine;
using UnityEngine.UI;

namespace DodoWorkshop.GameKit.Demos.StateMachines
{
    public class ProgressController : MonoBehaviour
    {
        [Header("Relations")]
        [SerializeField]
        private StateMachineEventContainer stateMachineEventContainer;

        [SerializeField]
        private Slider progressBar;
        

        [Header("Settings")]
        private float barDurationInSec = 10;


        private float progressMultiplier = 0;


        private void OnStateChanged(StateMachine stateMachine, State previous, State current)
        {
            if(current is DemoState demoState)
            {
                progressMultiplier = demoState.ProgressSpeed;
            }
        }

        private void UpdateProgressBar()
        {
            if(progressBar.value < 1)
            {
                float progress = (Time.deltaTime / barDurationInSec) * progressMultiplier;
                if (progress > 0)
                {
                    if (progressBar.value + progress >= 1)
                    {
                        progressBar.value = 1;
                    }
                    else
                    {
                        progressBar.value += progress;
                    }
                }
            }
        }

        private void Awake()
        {
            stateMachineEventContainer.OnStateChanged += OnStateChanged;
        }

        private void Update()
        {
            UpdateProgressBar();
        }
    }
}
