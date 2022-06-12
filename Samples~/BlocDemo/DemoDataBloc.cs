using UnityEngine;
using System;

namespace DodoWorkshop.GameKit.Demos.Bloc
{
    [Serializable]
	public class DemoDataBloc : Bloc<IDemoDataBlocCommand, DemoData>
	{
        [Header("Demo")]
        [SerializeField]
        private int minimumNameSize = 3;

        protected override void OnCommandReceived(IDemoDataBlocCommand command)
        {
            switch (command)
            {
                case DemoDataBlocCommands.IncrementCount c:
                    IncrementCount(c);
                    break;
                case DemoDataBlocCommands.ChangeName c:
                    ChangeName(c);
                    break;
                default:
                    throw new UnknownCommandException<IDemoDataBlocCommand>(command);
            }
        }

        private void IncrementCount(DemoDataBlocCommands.IncrementCount command)
        {
            UpdateState(state =>
            {
                state.Count += 1;

                return state;
            }, command);
        }

        private void ChangeName(DemoDataBlocCommands.ChangeName command)
        {
            UpdateState(state =>
            {
                if (command.Name.Length <= minimumNameSize)
                {
                    throw new Exception($"The name should be longer than {minimumNameSize}");
                }
                state.Name = command.Name;

                return state;
            }, command);
        }
    }
}
