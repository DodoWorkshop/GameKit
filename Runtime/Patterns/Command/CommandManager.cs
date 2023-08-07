using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace DodoWorkshop.GameKit
{
    /// <summary>
    /// This component has to be present in your scene in order
    /// to use <see cref="ICommand"/>s.
    /// </summary>
    [AddComponentMenu(PathConstants.ComponentMenu.BASE_PATH + "/Command/Command Manager")]
    public class CommandManager : Singleton<CommandManager>, ICommandResolver
    {
        [Header("Settings")]
        [SerializeField]
        private int maxCommands = 100;
        

        private readonly Queue<ICommand> commandQueue = new Queue<ICommand>();

        private Coroutine currentCoroutine;

        private ICommand currentCommand;

        private CommandExecutorState state = CommandExecutorState.STOPPED;


        public static CommandExecutorState State
        {
            get => Instance.state;
            set
            {
                Instance.state = value;
                OnStateChanged?.Invoke(value);
            }
        }

        public static bool IsRunning => State != CommandExecutorState.STOPPED;

        public static bool IsEmpty => Instance.commandQueue.Count == 0;

        public static ICommand CurrentCommand => Instance.currentCommand;

        public static ICommandResolver CommandResolver { get; set; }

        public static ICommand[] AwaitingCommands => Instance.commandQueue.ToArray();

        public static UnityAction<ICommand> OnNextCommand { get; set; }
        
        public static UnityAction<ICommand> OnCommandAdded { get; set; }

        public static UnityAction<CommandExecutorState> OnStateChanged { get; set; }
            

        public static void StartConsumption()
        {
            if (IsRunning)
            {
                throw new Exception("The CommandManager is already running");
            }

            Instance.NextCommand();
        }

        public static void StopConsumption(bool force = false)
        {
            if (force)
            {
                if (Instance.currentCoroutine != null)
                {
                    Instance.StopCoroutine(Instance.currentCoroutine);
                }
            }

            State = CommandExecutorState.STOPPED;
        }

        private void NextCommand()
        {
           
            if (IsEmpty)
            {
                State = CommandExecutorState.WAITING_COMMAND;
                currentCommand = null;
                OnNextCommand?.Invoke(CurrentCommand);
            }
            else
            {
                State = CommandExecutorState.EXECUTING_COMMAND;
                currentCommand = commandQueue.Dequeue();
                OnNextCommand?.Invoke(CurrentCommand);
                
                currentCommand.Execute(CommandResolver);
            }
        }

        public static void AddCommand(ICommand command)
        {
            Instance.commandQueue.Enqueue(command);

            OnCommandAdded?.Invoke(command);

            if (State == CommandExecutorState.WAITING_COMMAND)
            {
                Instance.NextCommand();
            }
        }

        public static void ClearQueue()
        {
            Instance.commandQueue.Clear();
        }

        public void ResolveWithCoroutine(IEnumerator coroutine)
        {
            currentCoroutine = StartCoroutine(ResolveCoroutine(coroutine));
        }

        public void Resolve()
        {
            if (State != CommandExecutorState.STOPPED)
            {
                NextCommand();
            }
        }

        private IEnumerator ResolveCoroutine(IEnumerator coroutine)
        {
            yield return coroutine;

            currentCoroutine = null;

            Resolve();
        }

        protected override void Awake()
        {
            base.Awake();

            CommandResolver = this;
        }
    }
}
