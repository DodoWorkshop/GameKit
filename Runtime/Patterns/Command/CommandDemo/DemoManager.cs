using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DodoWorkshop.GameKit.Demos.Commands
{
    public class DemoManager : Singleton<DemoManager>
    {
        [Header("Relations")]
        [SerializeField]
        private Text demoText;

        [SerializeField]
        private Image demoImage;

        [SerializeField]
        private Slider demoSlider;


        public static void ChangeDemoText(string text)
        {
            Instance.demoText.text = text;
        }

        public static void ChangeDemoImageColor(Color color)
        {
            Instance.demoImage.color = color;
        }

        public static void ChangeDemoSliderValue(float value)
        {
            Instance.demoSlider.value = value;
        }

        public static void PlayTestSequence()
        {
            
        }

        private void Start()
        {
            CommandManager.StartConsumption();
        }
    }
}
