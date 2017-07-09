using System;

namespace MConsole
{
	[AttributeUsage(AttributeTargets.Method)]
	public class MCommandAttribute : Attribute
	{
		public readonly string command;
		public readonly int parameterLimit;

		public MCommandAttribute(string command, int parameterLimit)
		{
			this.command = command;
			this.parameterLimit = parameterLimit;
		}
	}
}