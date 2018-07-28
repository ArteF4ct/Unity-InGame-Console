using System;

namespace UCLI
{
	[AttributeUsage(AttributeTargets.Property)]
	public class ConsoleVariableAttribute : Attribute
	{
		public readonly string command;
		public readonly string description;

		public ConsoleVariableAttribute(string command) 
		{
			this.command = command;
		}

		public ConsoleVariableAttribute(string command, string description)
		{
			this.command = command;
			this.description = description;
		}
	}
}