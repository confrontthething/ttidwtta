using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

	IEnumerator Start() {
		// Wait for the loading screen to actually fade in before continuing.
		yield return new WaitForSeconds(2.0f);
		UIFade fader = GameObject.FindGameObjectWithTag(Tags.ui).GetComponent<UIFade>();
		fader.LoadNextLevelAsync();
	}

}
