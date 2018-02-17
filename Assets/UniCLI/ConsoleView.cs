using UnityEngine;

namespace UCLI {
	public class ConsoleView : MonoBehaviour
	{
		private Rect windowRect = new Rect(20, 20, 500, 400);
		private Vector2 scrollPosition = new Vector2(0, 0);
		private string logView = "";
		private string commandArea = "";
		private bool isVisible = false;
		private GUISkin skin;
		private bool executeFlag = false;

		private void Start()
		{
			skin = Resources.Load<GUISkin>("UniCLISkin");
			DontDestroyOnLoad(gameObject);
		}

		void OnGUI()
		{
			if (!isVisible) return;
			GUI.skin = skin;
			windowRect = GUI.Window(0, windowRect, WindowContent, "UniCLI Console");

			if (Event.current.isKey && (Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.KeypadEnter))
			{
				executeFlag = true;
			}

			if (executeFlag) 
			{
				if (!string.IsNullOrEmpty(commandArea))
				{
					UniCLI.Instance.Execute(commandArea);
					commandArea = "";
				}
				executeFlag = false;
			}
		}

		void WindowContent(int windowID)
		{
			if (GUI.Button(new Rect(windowRect.width - 20, 0, 20, 20), "x", "box")) 
			{
				isVisible = false;
			}
			scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true);
			GUILayout.TextArea(logView);
			GUILayout.EndScrollView();
			GUILayout.BeginHorizontal();
			commandArea = GUILayout.TextField(commandArea);
			if (GUILayout.Button("Submit", GUILayout.MaxWidth(60)))
			{
				executeFlag = true;
			}
			GUILayout.EndHorizontal();

			GUI.DragWindow(new Rect(0, 0, 10000, 20));
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Tab)) 
			{
				isVisible = !isVisible;
			}
		}

		public void UpdateLogView(string logs)
		{
			logView = logs;
		}

		public void SetVisible(bool v) 
		{
			isVisible = v;
		}
	}
}