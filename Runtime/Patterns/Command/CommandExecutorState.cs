namespace DodoWorkshop.GameKit
{
    public enum CommandExecutorState
    {
        /// <summary>
        /// In this state, the <see cref="CommandManager"/> is 
        /// fully stopped and can't run <see cref="ICommand"/>s.
        /// You have to call the <see cref="CommandManager.StartConsumption"/>
        /// first.
        /// </summary>
        STOPPED,

        /// <summary>
        /// In this state, the <see cref="CommandManager"/> is 
        /// executing a <see cref="ICommand"/>.
        /// </summary>
        EXECUTING_COMMAND,

        /// <summary>
        /// In this state, the <see cref="CommandManager"/> is 
        /// is running but has no enqueued <see cref="ICommand"/>.
        /// </summary>
        WAITING_COMMAND
    }
}
