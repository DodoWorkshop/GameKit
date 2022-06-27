using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DodoWorkshop.GameKit.Demos.StateMachines
{
    public class DemoProgressBar : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private Slider progressBar;

        [SerializeField]
        private Image progressBarFillImage;

        [SerializeField]
        private Button playButton;

        [SerializeField]
        private Button stopButton;

        [SerializeField]
        private Button pauseButton;

        [SerializeField]
        private Button speedUpButton;


        [Header("Settings")]
        [SerializeField]
        private Color normalColor;

        [SerializeField]
        private Color finishedColor;

        [SerializeField]
        private Color pausedColor;


        public Slider ProgressBar => progressBar;

        public Image ProgressBarFillImage => progressBarFillImage;

        public Button PlayButton => playButton;

        public Button StopButton => stopButton;

        public Button PauseButton => pauseButton;

        public Button SpeedUpButton => speedUpButton;

        public Color NormalColor => normalColor;

        public Color FinishedColor => finishedColor;

        public Color PausedColor => pausedColor;

        public UnityAction OnProgressBarDone { get; set; }
        

        public void ResetAll() {
            // Disables every buttons
            playButton.interactable = false;
            stopButton.interactable = false;
            pauseButton.interactable = false;
            speedUpButton.interactable = false;
            
            // Configure progress bar
            progressBar.value = 0;
            progressBarFillImage.color = normalColor;
        }

        private void OnProgressBarValueChanged(float value)
        {
            if(value >= 1)
            {
                OnProgressBarDone?.Invoke();
            }
        }

        private void Awake()
        {
            progressBar.onValueChanged.AddListener(OnProgressBarValueChanged);
        }
    }
}
