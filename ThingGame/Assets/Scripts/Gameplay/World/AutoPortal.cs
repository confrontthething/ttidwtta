using UnityEngine;
using System.Collections;

public class AutoPortal : MonoBehaviour {

	public Scenes.SceneName levelName;
	public bool skipLoadingScreen = false;
	public bool isEndOfLevel = false;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag != Tags.player) {
			return;
		}

		TriggerManually();
	}

	public void TriggerManually() {
		UIFade fader = GameObject.FindGameObjectWithTag(Tags.ui).GetComponent<UIFade>();

		if (isEndOfLevel) {
			GlobalVariables.gameLevel = Scenes.GetLevelName(levelName);
			levelName = Scenes.SceneName.Otherworld;
		}

		if (skipLoadingScreen) {
			fader.LoadLevel(Scenes.GetLevelName(levelName));
		} else {
			fader.LoadLevelWithLoadingScreen(Scenes.GetLevelName(levelName));
		}
	}

}
