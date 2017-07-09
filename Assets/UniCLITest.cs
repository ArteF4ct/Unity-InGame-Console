using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniCLI;

public class UniCLITest : MonoBehaviour
{
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			UCLILogger.Log("Test message. Sent through MLogger");
		}

		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			Debug.Log("Test message. Sent through Unity Debug.Log");
		}
	}
}
