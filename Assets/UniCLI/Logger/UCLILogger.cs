using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace UniCLI
{
	public class UCLILogger
	{
		public const int LOG_QUEUE_CAPACITY = 99;
		private Queue<string> logQueue = new Queue<string>();
		private bool isUnityLogsEnabled = false;

		public delegate void OnLogReceivedHandler(string log);
		public event OnLogReceivedHandler OnLogReceived;

		private static UCLILogger instance;
		private UCLILogger() { }

		public static UCLILogger GetInstance()
		{
			if (instance == null)
			{
				instance = new UCLILogger();
			}
			return instance;
		}

		public void EnableUnityLogs()
		{
			if (!isUnityLogsEnabled)
			{
				UnityEngine.Application.logMessageReceived += OnUnityLogRecived;
				isUnityLogsEnabled = true;
			}
		}

		public void DisableUnityLogs()
		{
			if (isUnityLogsEnabled)
			{
				UnityEngine.Application.logMessageReceived -= OnUnityLogRecived;
				isUnityLogsEnabled = false;
			}
		}

		private void OnUnityLogRecived(string condition, string stackTrace, UnityEngine.LogType type)
		{
			Log(condition);
		}

		private void AddToLogQueue(string message)
		{
			if (logQueue.Count > LOG_QUEUE_CAPACITY)
			{
				logQueue.Dequeue();
			}
			logQueue.Enqueue(message);

			if (OnLogReceived != null) 
			{
				OnLogReceived(message);
			}
		}

		public string GetLogQueue()
		{
			StringBuilder builder = new StringBuilder();
			foreach (string s in logQueue)
			{
				builder.Append(s);
				builder.Append("\n");
			}
			return builder.ToString();
		}

		public static void Log(string message)
		{
			GetInstance().AddToLogQueue(message);
		}
	}
}