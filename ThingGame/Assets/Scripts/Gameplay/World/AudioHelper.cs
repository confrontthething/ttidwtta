using UnityEngine;
using System.Collections;

public class AudioHelper : MonoBehaviour {

	public AudioClip clip;
	public float volume = 1.0f;

	public void PlayUISound() {
		UIController userInterface =
			GameObject.FindGameObjectWithTag(Tags.ui).GetComponent<UIController>();
		userInterface.PlayUISoundOneShot(clip, volume);
	}

}
