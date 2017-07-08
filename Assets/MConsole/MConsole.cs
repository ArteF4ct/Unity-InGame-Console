namespace MConsole
{
	public class MConsole
	{
		private static MConsole instance;

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

		public void ExecuteCommand(string cmd) 
		{ 
			
		}
	}
}