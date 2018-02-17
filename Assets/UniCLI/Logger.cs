using System.Collections.Generic;
using System.Text;

namespace UCLI
{
	public class Logger
	{
		public const int LOG_QUEUE_CAPACITY = 99;
		private Queue<string> logQueue = new Queue<string>();
		public bool isUnityLogsEnabled { private set; get; }

		public delegate void OnLogReceivedHandler(string log);
		public event OnLogReceivedHandler OnLogReceived;

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
			string format = "{0}";
			switch (type) 
			{
				case UnityEngine.LogType.Log:
					break;
				case UnityEngine.LogType.Exception:
				case UnityEngine.LogType.Error:
					format = "<color=red>{0}</color>";
					break;
				case UnityEngine.LogType.Assert:
					format = "<color=blue>{0}</color>";
					break;
				case UnityEngine.LogType.Warning:
					format = "<color=yellow>{0}</color>";
					break;
			}
			Log(string.Format(format, condition));
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

		public void Log(string message)
		{
			AddToLogQueue(message);
		}

		public void LogFormat(string message, params object[] args)
		{
			AddToLogQueue(string.Format(message, args));
		}
	}
}