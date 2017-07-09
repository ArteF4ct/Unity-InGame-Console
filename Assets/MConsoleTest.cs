using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MConsole;

public class MConsoleTest : MonoBehaviour
{
	public Text text;
	public InputField inputField;

	private void Start()
	{
		inputField.onEndEdit.AddListener(OnInputChanged);
	}

	void OnInputChanged(string inputText) 
	{
		MLogger.Log(">" + inputText);
		MConsole.MConsole.Instance().ExecuteCommand(inputText);
		inputField.Select();
		inputField.text = "";
		UpdateView();
	}

	void UpdateView()
	{
		text.text = MLogger.GetInstance().GetLogQueue();
	}
}
