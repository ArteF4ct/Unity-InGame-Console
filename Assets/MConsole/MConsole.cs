using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace MConsole
{
	public class MConsole
	{
		private static MConsole instance;
		private MCommands mcommands = new MCommands();

		public static MConsole Instance() 
		{
			if (instance == null)
			{
				instance = new MConsole();
			}
			return instance;
		}

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

			System.Type type = typeof(MCommands);
			foreach (MethodInfo m in type.GetMethods())
			{
				foreach (MCommandAttribute a in m.GetCustomAttributes(typeof(MCommandAttribute), false))
				{
					if (string.Equals(cmd, a.command) && splittedCmd.Count == a.parameterLimit)
					{
						m.Invoke(mcommands, new object[] { splittedCmd.ToArray() });
					}
					else if (string.Equals(cmd, a.command) && (splittedCmd.Count != a.parameterLimit))
					{
						Debug.Log("Invalid parameters");
					}
				}
			}
		}

		public List<string> GetAllCommandUsages() 
		{
			List<string> list = new List<string>();
			System.Type type = typeof(MCommands);
			foreach (MethodInfo m in type.GetMethods())
			{
				foreach (MCommandAttribute a in m.GetCustomAttributes(typeof(MCommandAttribute), false))
				{
					list.Add(a.usage);
				}
			}
			return list;
		}
	}
}