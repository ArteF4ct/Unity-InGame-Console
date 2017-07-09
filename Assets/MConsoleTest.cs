using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MConsole;

public class MConsoleTest : MonoBehaviour 
{
	void Update () 
	{
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
	}
}
