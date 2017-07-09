namespace MConsole
{
	public partial class MCommands
	{
		[MCommand("help", 0, "help")]
		public void Help(params string[] args)
		{
			var commands = MConsole.Instance().GetAllCommandUsages();
			string output = "MConsole " + MConsole.VERSION + "\n";
			foreach (string s in commands)
			{
				output += s + "\n";
			}
			MLogger.Log(output);
		}

		[MCommand("help", 1, "help <command>")]
		public void HelpMethod(params string[] args)
		{
			var usage = MConsole.Instance().GetUsageOf(args[0]);
			MLogger.Log(usage);
		}

		[MCommand("close_console", 0, "close_console")]
		public void CloseConsole(params string[] args)
		{
			MConsole.Instance().Hide();
		}

		[MCommand("example", 3, "example <some_arg1> <some_arg2> <some_arg3>")]
		public void ExampleCommand(params string[] args)
		{
			MLogger.Log(string.Format("CMD: example / arg0: {0} / arg1: {1} / arg2: {2}", args[0], args[1], args[2]));
		}

		[MCommand("unity_logs", 1, "unity_logs <value>")]
		public void UnityLogs(params string[] args)
		{
			if (int.Parse(args[0]) == 0)
			{
				MLogger.GetInstance().DisableUnityLogs();
			}
			else if (int.Parse(args[0]) == 1)
			{
				MLogger.GetInstance().EnableUnityLogs();
			}
		}
	}
}
