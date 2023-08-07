using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace DodoWorkshop.GameKit
{
    public interface ICommandExecutor : ICommandResolver
    {
        void AddCommand(ICommand command);

        ICommand CurrentCommand { get; }
    }

    [Serializable]
    public class CommandExecutor : ICommandExecutor
    {
        [Header("Settings")]
        [SerializeField]
        private int maxCommands = 100;


        private readonly Queue<ICommand> commandQueue = new Queue<ICommand>();

        private Coroutine currentCoroutine;

        private ICommand currentCommand;

        private CommandExecutorState state = CommandExecutorState.STOPPED;


        public CommandExecutorState State
        {
            get => state;
            set
            {
                state = value;
                OnStateChanged?.Invoke(value);
            }
        }

        public bool IsRunning => State != CommandExecutorState.STOPPED;

        public bool IsEmpty => commandQueue.Count == 0;

        public ICommand CurrentCommand => currentCommand;

        public ICommandResolver CommandResolver { get; set; }

        public ICommand[] AwaitingCommands => commandQueue.ToArray();

        public UnityAction<ICommand> OnNextCommand { get; set; }

        public UnityAction<ICommand> OnCommandAdded { get; set; }

        public UnityAction<CommandExecutorState> OnStateChanged { get; set; }


        public void Start()
        {
            if (IsRunning)
            {
                throw new Exception("The Command Executor is already running");
            }

            NextCommand();
        }

        public void Stop(bool force = false)
        {
            if (force)
            {
                if (currentCoroutine != null)
                {
                    StopCoroutine(currentCoroutine);
                }
            }

            State = CommandExecutorState.STOPPED;
        }


        public void AddCommand(ICommand command)
        {
            throw new System.NotImplementedException();
        }

        public void Resolve()
        {
            throw new System.NotImplementedException();
        }

        public void ResolveWithCoroutine(IEnumerator coroutine)
        {
            throw new System.NotImplementedException();
        }
    }
}
