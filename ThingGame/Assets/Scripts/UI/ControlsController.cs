using UnityEngine;
using System.Collections;

public class ControlsController : MonoBehaviour {

	void Update() {
		if (Input.anyKeyDown) {
			GetComponent<Animator>().SetBool("running", false);
		}
	}

	void CompleteScene() {
		Application.LoadLevel(Scenes.intro);
	}

}
