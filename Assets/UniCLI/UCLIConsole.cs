using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace UniCLI
{
	public class UCLIConsole
	{
		public const string VERSION = "v0.0.1";

		private static UCLIConsole instance;
		private Commands mcommands = new Commands();

		public static UCLIConsole Instance()
		{
			if (instance == null)
			{
				instance = new UCLIConsole();
			}
			return instance;
		}

		private UCLIConsole() { }

		public void Show()
		{

		}

		public void Hide()
		{

		}

		public void ExecuteCommand(string cmdString)
		{
			if (string.IsNullOrEmpty(cmdString))
			{
				return;
			}

			List<string> splittedCmd = new List<string>(cmdString.Split(' '));
			string cmd = splittedCmd[0];
			splittedCmd.RemoveAt(0);

			System.Type type = typeof(Commands);
			foreach (MethodInfo m in type.GetMethods())
			{
				foreach (CommandAttribute a in m.GetCustomAttributes(typeof(CommandAttribute), false))
				{
					if (string.Equals(cmd, a.command) && splittedCmd.Count == a.parameterLimit)
					{
						m.Invoke(mcommands, new object[] { splittedCmd.ToArray() });
					}
				}
			}
		}

		public List<string> GetAllCommandUsages()
		{
			List<string> list = new List<string>();
			System.Type type = typeof(Commands);
			foreach (MethodInfo m in type.GetMethods())
			{
				foreach (CommandAttribute a in m.GetCustomAttributes(typeof(CommandAttribute), false))
				{
					list.Add(a.usage);
				}
			}
			return list;
		}

		public string GetUsageOf(string command)
		{
			System.Type type = typeof(Commands);
			foreach (MethodInfo m in type.GetMethods())
			{
				foreach (CommandAttribute a in m.GetCustomAttributes(typeof(CommandAttribute), false))
				{
					if (command.Equals(a.command))
					{
						return a.usage;
					}
				}
			}
			return null;
		}

		public string GetDescriptionOf(string command)
		{
			System.Type type = typeof(Commands);
			foreach (MethodInfo m in type.GetMethods())
			{
				foreach (CommandAttribute a in m.GetCustomAttributes(typeof(CommandAttribute), false))
				{
					if (command.Equals(a.command))
					{
						return a.description;
					}
				}
			}
			return null;
		}
	}
}