using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MConsole
{ 
	public class MConsoleView : MonoBehaviour 
	{
		public Text textContainer;
		public InputField inputField;

		private void Start()
		{
			inputField.onEndEdit.AddListener(OnInputChanged);
			MLogger.GetInstance().OnLogReceived += UpdateTextContainer;
		}

		private void OnInputChanged(string inputText)
		{
			MLogger.Log(">" + inputText);
			MConsole.Instance().ExecuteCommand(inputText);
			ClearInputField();
		}

		private void ClearInputField() 
		{
			inputField.Select();
			inputField.text = "";
		}

		private void UpdateTextContainer(string message)
		{
			textContainer.text = MLogger.GetInstance().GetLogQueue();
		}
	}
}