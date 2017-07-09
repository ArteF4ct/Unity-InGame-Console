using System;

namespace UniCLI
{
	[AttributeUsage(AttributeTargets.Method)]
	public class CommandAttribute : Attribute
	{
		public readonly string command;
		public readonly int parameterLimit;
		public readonly string usage;
		public readonly string description;

		public CommandAttribute(string command, int parameterLimit, string usage, string description)
		{
			this.command = command;
			this.parameterLimit = parameterLimit;
			this.usage = usage;
			this.description = description;
		}
	}
}