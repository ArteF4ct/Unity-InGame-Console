using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UniCLI
{ 
	public class ConsoleView : MonoBehaviour 
	{
		public Text textContainer;
		public InputField inputField;

		private void Start()
		{
			inputField.onEndEdit.AddListener(OnInputChanged);
			UCLILogger.GetInstance().OnLogReceived += UpdateTextContainer;
		}

		private void OnInputChanged(string inputText)
		{
			UCLILogger.Log(">" + inputText);
			UCLIConsole.Instance().ExecuteCommand(inputText);
			ClearInputField();
		}

		private void ClearInputField() 
		{
			inputField.Select();
			inputField.text = "";
		}

		private void UpdateTextContainer(string message)
		{
			textContainer.text = UCLILogger.GetInstance().GetLogQueue();
		}
	}
}