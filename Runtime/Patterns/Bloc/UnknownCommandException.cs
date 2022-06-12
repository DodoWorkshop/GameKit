using System;

namespace DodoWorkshop.GameKit
{
	public class UnknownCommandException<TCommand> : Exception
	{
	    public UnknownCommandException(TCommand command)
            : base($"The command {command.GetType().Name} has been send to a bloc that can't consume it"){ }
	}
}
