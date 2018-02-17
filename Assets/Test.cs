using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UCLI;

public class Test : MonoBehaviour 
{

	private void Start()
	{
		UniCLI.Instance.Initialize();
	}

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.A)) 
		{
			UniCLI.Instance.Execute("test xd 5 2.3 true");
			UniCLI.Instance.Execute("test2 xd 5 2.3");
			UniCLI.Instance.Execute("tessdat xd 5 2.3 true");
			UniCLI.Instance.Execute("test xd 5 2.3 2");
			UniCLI.Instance.Execute("timescale");
			UniCLI.Instance.Execute("timescale 5");
			UniCLI.Instance.Execute("timescale");
			UniCLI.Instance.Execute("timescale 8");
			UniCLI.Instance.Execute("timescale");
			UniCLI.Instance.Execute("timescale asdasd");
			//UniCLI.Log("dasfsadf sad fasd fsdaf sdaf sdaf sadf sd fsadf sdaf sdaf sad fsda fsad fsad fsadf sadf sdaf sadf sadf sdaf sdaf sdf sdaf sdaf sdaf sdf ");
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			Debug.Log("test " + Random.Range(0, 100));	
		}
	}

	[ConsoleCommand("test")]
	public static void TestCmd(string arg0, int arg1, float arg2, bool arg3) 
	{
		UniCLI.LogFormat("<color=red>Test cmd</color> arg0: {0}, arg1: {1}, arg2: {2}, arg3: {3}, arg1+arg2: {4}", arg0, arg1, arg2, arg3, (arg1+arg2));
	}
	[ConsoleCommand("test2")]
	public static void TestCmd2(string arg0, int arg1, float arg2)
	{
		UniCLI.LogFormat("<color=red>Test cmd</color> arg0: {0}, arg1: {1}, arg2: {2}, arg1+arg2: {3}", arg0, arg1, arg2, (arg1 + arg2));
	}

	[ConsoleVariable("timescale")]
	public static float TimeScale 
	{ 
		get 
		{
			return Time.timeScale;
		}
		set 
		{
			Time.timeScale = value;
		}
	}
}
