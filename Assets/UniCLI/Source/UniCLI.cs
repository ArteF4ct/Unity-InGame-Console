using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace UCLI
{
	public class UniCLI
	{
		#region Variables
		private Dictionary<string, ICVar> commands = new Dictionary<string, ICVar>();
		private Logger logger = new Logger();
		public ConsoleView View { private set; get; }
		#endregion

		#region Singleton
		public static UniCLI Instance 
		{ 
			get 
			{
				if (instance == null) 
				{
					instance = new UniCLI();
				}
				return instance;
			}
		}

		private static UniCLI instance;
		#endregion

		#region Constructor
		private UniCLI() {}
		#endregion

		#region Initialization
		public void Initialize() 
		{
			InitializeCommands();
			logger.OnLogReceived += Logger_OnLogReceived;
			var g = new UnityEngine.GameObject("UniCLI View");
			View = g.AddComponent<ConsoleView>();
		}

		private void InitializeCommands() 
		{
			foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
			{
				foreach (MethodInfo method in type.GetMethods())
				{
					foreach (ConsoleCommandAttribute attribute in method.GetCustomAttributes(typeof(ConsoleCommandAttribute), false))
					{
						ParameterInfo[] parameters = method.GetParameters();
						List<Type> parameterTypeList = new List<Type>();
						List<string> parameterNameList = new List<string>();
						foreach (ParameterInfo pi in parameters) 
						{
							parameterTypeList.Add(pi.ParameterType);
							parameterNameList.Add(pi.Name);
						}
						Command cmd = new Command()
						{
							name = attribute.command,
							description = attribute.description,
							parameterTypes = parameterTypeList,
							parameterNames = parameterNameList,
							method = method
						};
						commands.Add(attribute.command, cmd);
					}
				}

				foreach (PropertyInfo property in type.GetProperties())
				{
					foreach (ConsoleVariableAttribute attribute in property.GetCustomAttributes(typeof(ConsoleVariableAttribute), false))
					{
						Type t = property.PropertyType;
						Variable cmd = new Variable()
						{
							name = attribute.command,
							description = attribute.description,
							type = t,
							property = property
						};
						commands.Add(attribute.command, cmd);
					}
				}
			}
		}
		#endregion

		private void Logger_OnLogReceived(string log)
		{
			View.UpdateLogView(logger.GetLogQueue());
		}

		public void Execute(string str) 
		{
			string[] splittedStr = str.Split(' ');
			if (splittedStr.Length == 0) return;
			string cmdName = splittedStr[0];
			var temp = new List<string>(splittedStr);
			temp.RemoveAt(0);
			string[] args = temp.ToArray();

			logger.LogFormat("] {0}", str);

			if (commands.ContainsKey(cmdName))
			{
				ICVar cmd = commands[cmdName];
				if (cmd.IsCmd())
				{
					cmd.Set(args);
				}
				else 
				{
					if (args.Length > 0)
					{
						cmd.Set(args);
					}
					else 
					{
						var v = cmd.Get();
						logger.LogFormat("{0}: {1}", cmdName, v);
					}
				}
			}
			else 
			{
				logger.LogFormat("Cannot find command '{0}'", splittedStr[0]);
			}
		}

		private string GetUsage(string cmd) 
		{
			if (Instance.commands.ContainsKey(cmd)) 
			{
				if (Instance.commands[cmd].IsCmd())
				{
					Command c = (Command)Instance.commands[cmd];
					string p = string.Join("> <", c.parameterNames.ToArray());
					if (string.IsNullOrEmpty(p))
					{
						return string.Format("Usage: {0}", c.name);
					}
					else 
					{ 
						return string.Format("Usage: {0} <{1}>", c.name, p);
					}
				}
				else 
				{
					Variable v = (Variable)Instance.commands[cmd];
					var usage = string.Format("Usage: {0} <{1}>", v.name, v.type.ToString());
					return usage;
				}
			}
			return "";
		}

		public static void Log(string message)
		{
			Instance.logger.Log(message);
		}

		public static void LogFormat(string message, params object[] args)
		{
			Instance.logger.LogFormat(message, args);
		}

		[ConsoleCommand("close")]
		public static void CloseWindow()
		{
			Instance.View.SetVisible(false);
		}


		[ConsoleVariable("unity_logs")]
		public static int UnityLogs
		{
			get 
			{
				return Instance.logger.isUnityLogsEnabled ? 1 : 0;
			}
			set 
			{
				if (value == 1)
				{
					Instance.logger.EnableUnityLogs();
				}
				else 
				{
					Instance.logger.DisableUnityLogs();
				}
			}
		}

		[ConsoleCommand("help")]
		public static void Help(string cmd) 
		{
			if (Instance.commands.ContainsKey(cmd)) 
			{
				if (Instance.commands[cmd].IsCmd())
				{
					Command c = (Command)Instance.commands[cmd];
					string p = string.Join("> <", c.parameterNames.ToArray());
					var usage = string.Format("Usage: {0} <{1}>", c.name, p);
					Instance.logger.LogFormat("{0} : {1} : {2}", c.name, usage, c.description);
				}
				else
				{
					Variable v = (Variable)Instance.commands[cmd];
					var usage = string.Format("Usage: {0} <{1}>", v.name, v.type.ToString());
					Instance.logger.LogFormat("{0} : {1} : {2}", v.name, usage, v.description);
				}
			}
			else 
			{
				Instance.logger.LogFormat("Cannot find command '{0}'", cmd);
			}
		}

		[ConsoleCommand("cmdlist")]
		public static void CmdList()
		{
			foreach (var cmd in Instance.commands) 
			{
				if (cmd.Value.IsCmd())
				{
					Command c = (Command)cmd.Value;
					Instance.logger.LogFormat("{0} \t: {1}", c.name, "cmd");
				}
				else
				{
					Variable v = (Variable)cmd.Value;
					Instance.logger.LogFormat("{0} \t: {1}", v.name, v.Get().ToString());
				}
			}
		}

	}

	public class Command : ICVar
	{
		public string name;
		public string description;
		public List<Type> parameterTypes;
		public List<string> parameterNames;
		public MethodInfo method;

		public object Get()
		{
			return null;
		}

		public void Set(params string[] args)
		{
			if (args.Length == parameterTypes.Count)
			{
				object[] parameters = new object[parameterTypes.Count];
				bool isParametersCorrect = false;
				try
				{
					for (int i = 0; i < args.Length; i++)
					{
						parameters[i] = Convert.ChangeType(args[i], parameterTypes[i]);
					}
					isParametersCorrect = true;
				}
				catch
				{
					string p = string.Join("> <", parameterNames.ToArray());
					UniCLI.LogFormat("Usage: {0} <{1}>", name, p);
					isParametersCorrect = false;
				}
				if (isParametersCorrect)
				{
					method.Invoke(null, parameters);
				}
			}
			else
			{
				string p = string.Join("> <", parameterNames.ToArray());
				UniCLI.LogFormat("Usage: {0} <{1}>", name, p);
			}
		}

		public bool IsCmd()
		{
			return true;
		}

	}

	public class Variable : ICVar
	{
		public string name;
		public string description;
		public Type type;
		public PropertyInfo property;

		public object Get()
		{
			return property.GetValue(null, null);
		}

		public void Set(params string[] args)
		{
			object value = null;
			bool isParameterCorrect = false;
			try
			{
				value = Convert.ChangeType(args[0], type);
				isParameterCorrect = true;
			}
			catch
			{
				UniCLI.LogFormat("Wrong type: {0} <{1}>", name, type);
				isParameterCorrect = false;
			}
			if (isParameterCorrect)
			{
				property.SetValue(null, value, null);
			}
		}

		public bool IsCmd()
		{
			return false;
		}
	}

	public interface ICVar
	{
		void Set(params string[] args);
		object Get();
		bool IsCmd();
	}
}