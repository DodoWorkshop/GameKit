using UnityEngine;
using UnityEngine.Events;

namespace DodoWorkshop.GameKit
{
    [CreateAssetMenu(menuName = PathConstants.AssetMenu.BASE_PATH + "/State Machine/Event Container")]
    public class StateMachineEventContainer : EventContainer<StateMachine>
    {
        /// <inheritdoc cref="StateMachine.OnStateChanged"/>
        public UnityAction<StateMachine, State, State> OnStateChanged { get; set; }

        /// <inheritdoc cref="StateMachine.OnStateMachineStarted"/>
        public UnityAction<StateMachine> OnStateMachineStarted { get; set; }

        /// <inheritdoc cref="StateMachine.OnStateMachineStopped"/>
        public UnityAction<StateMachine> OnStateMachineStopped { get; set; }
        

        protected override void OnBind(StateMachine source)
        {
            source.OnStateChanged += (previous, current) => OnStateChanged?.Invoke(source, previous, current);
            source.OnStateMachineStarted += () => OnStateMachineStarted?.Invoke(source);
            source.OnStateMachineStopped += () => OnStateMachineStopped?.Invoke(source);
        }

        protected override void OnUnbind(StateMachine source)
        {
            source.OnStateChanged -= (previous, current) => OnStateChanged?.Invoke(source, previous, current);
            source.OnStateMachineStarted -= () => OnStateMachineStarted?.Invoke(source);
            source.OnStateMachineStopped -= () => OnStateMachineStopped?.Invoke(source);
        }
    }
}
