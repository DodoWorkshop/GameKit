using UnityEngine.Events;

namespace DodoWorkshop.GameKit
{
    public interface IBloc : IComponent
    {
        void ResetState();
    }

    public interface IBloc<TCommand> : IBloc
    {
        void Send(TCommand command);
    }

    public interface IBloc<TCommand, TState> : IBloc<TCommand> where TState : new()
    {
        TState State { get; }

        UnityAction<BlocUpdate<TCommand, TState>> OnStateUpdated { get; set; }

        void InitializeState(TState state);
    }
}
