using System.Collections;

namespace DodoWorkshop.GameKit
{
    /// <summary>
    /// A simpler implementation of the <see cref="ICommand"/>
    /// interface dedicated for asynchrone commands.
    /// </summary>
    public abstract class AsyncCommand : ICommand
    {
        public void Execute(ICommandResolver resolver)
        {
            resolver.ResolveWithCoroutine(Execute());
        }

        protected abstract IEnumerator Execute();
    }
}
