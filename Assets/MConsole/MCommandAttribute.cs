using System;

namespace MConsole
{
	[AttributeUsage(AttributeTargets.Method)]
	public class MCommandAttribute : Attribute
	{
		public readonly string command;

		public MCommandAttribute(string command)
		{
			this.command = command;
		}
	}
}