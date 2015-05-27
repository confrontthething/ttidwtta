using UnityEngine;
using UnityEditor;
using System.Collections;

public class Screenshot : ScriptableObject {

	[MenuItem("File/Take Screenshot 4x")]
	static void TakeScreenshot4x() {
		Application.CaptureScreenshot("/Users/Steve/Desktop/game.png", 4);
	}

	[MenuItem("File/Take Screenshot 1x")]
	static void TakeScreenshot1x() {
		Application.CaptureScreenshot("/Users/Steve/Desktop/game.png", 1);
	}

}
