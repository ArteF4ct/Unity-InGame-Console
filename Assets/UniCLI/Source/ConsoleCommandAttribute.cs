using System;

namespace UCLI
{
	[AttributeUsage(AttributeTargets.Method)]
	public class ConsoleCommandAttribute : Attribute
	{
		public readonly string command;
		public readonly string description;

		public ConsoleCommandAttribute(string command) 
		{
			this.command = command;
		}

		public ConsoleCommandAttribute(string command, string description)
		{
			this.command = command;
			this.description = description;
		}
	}
}