using System.Collections;
using UnityEngine;

namespace DodoWorkshop.GameKit.Demos.StateMachines
{
    public class SpeedUpState : DemoState
    {
        public override float ProgressSpeed => 3;


        private Coroutine coroutine;

        public override void OnEnter()
        {
            coroutine = stateMachine.StartCoroutine(SpeedUpFor2Seconds());
        }

        public override void OnExit()
        {
            stateMachine.StopCoroutine(coroutine);
        }

        private IEnumerator SpeedUpFor2Seconds()
        {
            yield return new WaitForSeconds(2);
            stateMachine.ChangeToPrevious();
        }
    }
}
