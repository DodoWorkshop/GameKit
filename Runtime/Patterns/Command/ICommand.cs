using System.Collections;

namespace DodoWorkshop.GameKit
{
    /// <summary>
    /// The base interface for every <see cref="ICommand"/>
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// The method called to execute the command. You have
        /// to call the <see cref="ICommandResolver.Resolve"/> method
        /// or the <see cref="ICommandResolver.ResolveWithCoroutine(IEnumerator)"/>
        /// for async method when the execution of your command is
        /// done.
        /// </summary>
        /// <param name="resolver">The resolver used to manage commands</param>
        public void Execute(ICommandResolver resolver);
    }
}
