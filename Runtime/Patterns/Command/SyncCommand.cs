namespace DodoWorkshop.GameKit
{
    /// <summary>
    /// A simpler implementation of the <see cref="ICommand"/>
    /// interface dedicated for synchronized commands.
    /// </summary>
    public abstract class SyncCommand : ICommand
    {
        public void Execute(ICommandResolver resolver)
        {
            Execute();
            resolver.Resolve();
        }

        protected abstract void Execute();
    }
}
