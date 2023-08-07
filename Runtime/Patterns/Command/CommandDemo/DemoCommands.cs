using System.Collections;
using UnityEngine;

namespace DodoWorkshop.GameKit.Demos.Commands
{
    public class WaitCommand : AsyncCommand
    {
        private float duration;
        private int steps;
        private int currentStep;

        public WaitCommand(float duration = 1.0f, int steps = 100)
        {
            this.duration = duration;
            this.steps = steps;
        }

        protected override IEnumerator Execute()
        {
            currentStep = 0;

            while (currentStep < steps)
            {
                currentStep++;

                float factor = duration / steps;

                DemoManager.ChangeDemoSliderValue(factor * currentStep / duration);

                yield return new WaitForSeconds(factor);
            }
        }
    }

    public class ChangeTextCommand : SyncCommand
    {
        private string text;

        public ChangeTextCommand(string text)
        {
            this.text = text;
        }

        protected override void Execute()
        {
            DemoManager.ChangeDemoText(text);
        }
    }

    public class ChangeColorCommand : ICommand
    {
        private Color color;

        public ChangeColorCommand(Color color)
        {
            this.color = color;
        }

        public void Execute(ICommandResolver resolver)
        {
            DemoManager.ChangeDemoImageColor(color);

            resolver.Resolve();
        }
    }

    public class ChangeColorAndReturnCommand : ICommand
    {
        private Color startColor;
        private Color endColor;
        private float duration;

        public ChangeColorAndReturnCommand(Color startColor, Color endColor, float duration = 1.0f)
        {
            this.startColor = startColor;
            this.endColor = endColor;
            this.duration = duration;
        }

        public void Execute(ICommandResolver resolver)
        {
            resolver.ResolveWithCoroutine(Coroutine());
        }

        private IEnumerator Coroutine()
        {
            DemoManager.ChangeDemoImageColor(startColor);

            yield return new WaitForSeconds(duration);

            DemoManager.ChangeDemoImageColor(endColor);
        }
    }
}
