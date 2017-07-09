namespace UniCLI
{
	public partial class Commands
	{
		[Command("help", 0, "help", "Help command")]
		public void Help(params string[] args)
		{
			var commands = UCLIConsole.Instance().GetAllCommandUsages();
			string output = "UniCLI " + UCLIConsole.VERSION + "\n";
			foreach (string s in commands)
			{
				output += s + "\n";
			}
			UCLILogger.Log(output);
		}

		[Command("help", 1, "help <command>", "To look usage and description of a command.")]
		public void HelpMethod(params string[] args)
		{
			var usage = UCLIConsole.Instance().GetUsageOf(args[0]);
			var description = UCLIConsole.Instance().GetDescriptionOf(args[0]);
			UCLILogger.Log(usage);
			UCLILogger.Log(description);
		}

		[Command("close_console", 0, "close_console", "Closes the console window.")]
		public void CloseConsole(params string[] args)
		{
			UCLIConsole.Instance().Hide();
		}

		[Command("example", 3, "example <some_arg1> <some_arg2> <some_arg3>", "Example command")]
		public void ExampleCommand(params string[] args)
		{
			UCLILogger.Log(string.Format("CMD: example / arg0: {0} / arg1: {1} / arg2: {2}", args[0], args[1], args[2]));
		}

		[Command("unity_logs", 1, "unity_logs <value>", "Show Unity Debug logs at MConsole (value = 1) / default value = 0")]
		public void UnityLogs(params string[] args)
		{
			if (int.Parse(args[0]) == 0)
			{
				UCLILogger.GetInstance().DisableUnityLogs();
			}
			else if (int.Parse(args[0]) == 1)
			{
				UCLILogger.GetInstance().EnableUnityLogs();
			}
		}
	}
}
