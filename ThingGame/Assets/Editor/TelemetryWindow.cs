using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class TelemetryWindow : EditorWindow {

	enum Filter {
		Standalone = 0, Editor = 1, All = 2
	}

	const string GET_ALL_ENDPOINT =
		"http://thingchoice.appspot.com/report/all";

	const string GET_STANDALONE_ENDPOINT =
		"http://thingchoice.appspot.com/report/0";

	const string GET_EDITOR_ENDPOINT =
		"http://thingchoice.appspot.com/report/1";

	Filter filter = Filter.Standalone;
	WWW www = null;

	long frinchfry = 0;
	long theThing = 0;
	string error = null;

	[MenuItem ("Window/Telemetry")]
  public static void ShowWindow () {
      EditorWindow.GetWindow(typeof(TelemetryWindow));
  }

	void OnGUI() {
		title = "Telemetry";
		GUILayout.Label("Filter", EditorStyles.boldLabel);
		Filter newFilter = (Filter)EditorGUILayout.EnumPopup("Category", filter);

		if (newFilter != filter) {
			CancelReport();
			filter = newFilter;
			GetReport();
		}

		// Display data.
		GUILayout.Label("Results", EditorStyles.boldLabel);
		if (error == null && www == null) {
			GUILayout.Label("Frinchfry: " + frinchfry.ToString());
			GUILayout.Label("The Thing: " + theThing.ToString());

			EditorGUILayout.HelpBox("Telemetry successfully downloaded",
				MessageType.None, true);
		} else if (error == null && www != null) {
			GUILayout.Label("Frinchfry:");
			GUILayout.Label("The Thing:");

			EditorGUILayout.HelpBox("Downloading telemetry",
				MessageType.None, true);
		} else {
			GUILayout.Label("Frinchfry:");
			GUILayout.Label("The Thing:");

			EditorGUILayout.HelpBox(error, MessageType.Error, true);
		}

		if (www != null) {
			// Loading data.
			const string t = "Downloading Telemetry";
			const string m = "Reticulating splines, please wait...";
			float p = www.progress;

			if (EditorUtility.DisplayCancelableProgressBar(t, m, p)) {
				CancelReport();
				EditorUtility.ClearProgressBar();
			} else if (www.isDone) {
				try {
					if (www.error != null) {
						throw new System.Exception("Could not download data");
					}

					var dict = Json.Deserialize(www.text) as Dictionary<string, object>;

					if (dict == null) {
						throw new System.Exception("Data received was malformed");
					}

					var endChoices = dict["endChoice"] as Dictionary<string, object>;

					frinchfry = (long)endChoices["frinchfry"];
					theThing = (long)endChoices["theThing"];

					error = null;
				} catch (System.Exception e) {
					frinchfry = 0;
					theThing = 0;
					error = e.Message;
				}

				www = null;
				EditorUtility.ClearProgressBar();
			}
		}
	}

	void OnEnable() {
		GetReport();
	}

	void GetReport() {
		string url;
		switch (filter) {
		case Filter.Standalone:
			url = GET_STANDALONE_ENDPOINT;
			break;
		case Filter.Editor:
			url = GET_EDITOR_ENDPOINT;
			break;
		case Filter.All:
			url = GET_ALL_ENDPOINT;
			break;
		default:
			throw new System.InvalidOperationException("Invalid filter");
		}

		www = new WWW(url);
	}

	void CancelReport() {
		if (www != null) {
			www.Dispose();
		}
		www = null;
	}

}
