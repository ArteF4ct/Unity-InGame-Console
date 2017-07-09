using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MConsole;

public class MConsoleTest : MonoBehaviour
{
	public Text text;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			Debug.Log(Random.Range(0, 100) + "T");
		}

		if (Input.GetKeyDown(KeyCode.A))
		{
			MLogger.Log("TEST");
		}

		if (Input.GetKeyDown(KeyCode.A))
		{
			MConsole.MConsole.Instance().ExecuteCommand("close_console");
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			MConsole.MConsole.Instance().ExecuteCommand("example gg 1 aaaa");
		}

		if (Input.GetKeyDown(KeyCode.D))
		{
			MConsole.MConsole.Instance().ExecuteCommand("close_console test");
		}

		if (Input.GetKeyDown(KeyCode.F))
		{
			MConsole.MConsole.Instance().ExecuteCommand("example gg 1");
		}

		if (Input.GetKeyDown(KeyCode.G))
		{
			MConsole.MConsole.Instance().ExecuteCommand("example gg 1 aaa test");
		}

		if (Input.GetKeyDown(KeyCode.Q))
		{
			MConsole.MConsole.Instance().ExecuteCommand("help");
		}

		if (Input.GetKeyDown(KeyCode.W))
		{
			MConsole.MConsole.Instance().ExecuteCommand("unity_logs 1");
		}

		if (Input.GetKeyDown(KeyCode.X))
		{
			MConsole.MConsole.Instance().ExecuteCommand("help unity_logs");
		}
		UpdateView();
	}

	void UpdateView()
	{
		text.text = MLogger.GetInstance().GetLogQueue();
	}
}
