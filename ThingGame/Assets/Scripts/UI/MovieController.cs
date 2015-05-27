using UnityEngine;
using System.Collections;

public class MovieController : MonoBehaviour {

	public Scenes.SceneName nextLevelName;
	public bool preventSkip = false;

	void ExitMovie() {
		UIFade fader = GameObject.FindGameObjectWithTag(Tags.ui).GetComponent<UIFade>();
		fader.LoadLevel(Scenes.GetLevelName(nextLevelName));
	}

	// Update is called once per frame
	void Update () {
		if (!preventSkip && Input.GetKey(KeyCode.Escape)) {
			ExitMovie();
		}
	}

}
