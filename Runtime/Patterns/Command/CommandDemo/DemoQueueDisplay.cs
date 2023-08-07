using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace DodoWorkshop.GameKit.Demos.Commands
{
    public class DemoQueueDisplay : MonoBehaviour
    {
        [SerializeField]
        private Text queueText;


        private void Start()
        {
            CommandManager.OnNextCommand += OnNextCommand;
            CommandManager.OnCommandAdded += OnCommandAdded;

            UpdateDisplay();
        }

        private void OnCommandAdded(ICommand arg0)
        {
            UpdateDisplay();
        }

        private void OnNextCommand(ICommand command)
        {
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            switch (CommandManager.State)
            {
                case CommandExecutorState.STOPPED:
                    queueText.text = "The queue is stopped";
                    break;
                case CommandExecutorState.EXECUTING_COMMAND:
                    queueText.text = "=> " + string.Join(
                        "\n-> ", 
                        CommandManager.AwaitingCommands
                        .Prepend(CommandManager.CurrentCommand)
                        .Select(c => c.GetType().Name)
                    );
                    break;
                case CommandExecutorState.WAITING_COMMAND:
                    queueText.text = "The queue is empty";
                    break;
                default:
                    break;
            }
        }
    }
}
