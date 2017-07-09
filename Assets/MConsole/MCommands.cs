namespace MConsole
{
	public partial class MCommands
	{
		[MCommand("close_console", 0)]
		public void CloseConsole(params string[] args) 
		{
			MConsole.Instance().Hide();
		}

		[MCommand("example", 3)]
		public void ExampleCommand(params string[] args)
		{
			UnityEngine.Debug.Log(string.Format("CMD: example / arg0: {0} / arg1: {1} / arg2: {2}", args[0], args[1], args[2]));
		}
	}
}
