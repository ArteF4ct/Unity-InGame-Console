using System;

namespace MConsole
{
	[AttributeUsage(AttributeTargets.Method)]
	public class MCommandAttribute : Attribute
	{
		public readonly string command;
		public readonly int parameterLimit;
		public readonly string usage;

		public MCommandAttribute(string command, int parameterLimit, string usage)
		{
			this.command = command;
			this.parameterLimit = parameterLimit;
			this.usage = usage;
		}
	}
}