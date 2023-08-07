using UnityEngine;

namespace DodoWorkshop.GameKit.Demos.Commands
{
    public class DemoCommandController : MonoBehaviour
    {
        [SerializeField]
        private Color defaultColor = Color.gray;
        

        public void AddWaitCommand(float seconds)
        {
            CommandManager.AddCommand(new WaitCommand(seconds));
        }

        public void AddChangeTextCommand(string text) {
            CommandManager.AddCommand(new ChangeTextCommand(text));
        }

        public void AddChangeColorCommand(Color color)
        {
            CommandManager.AddCommand(new ChangeColorCommand(color));
        }

        public void AddChangeColorToRedCommand()
        {
            CommandManager.AddCommand(new ChangeColorCommand(Color.red));
        }

        public void AddChangeColorToDefaultCommand()
        {
            CommandManager.AddCommand(new ChangeColorCommand(defaultColor));
        }

        public void AddChangeColorToGreenAndReturnCommand()
        {
            CommandManager.AddCommand(new ChangeColorAndReturnCommand(Color.green, defaultColor));
        }
    }
}
