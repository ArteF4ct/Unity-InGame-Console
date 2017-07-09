using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MConsole;

public class MConsoleTest : MonoBehaviour
{
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			MLogger.Log("Test message. Sent through MLogger");
		}

		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			Debug.Log("Test message. Sent through Unity Debug.Log");
		}
	}
}
