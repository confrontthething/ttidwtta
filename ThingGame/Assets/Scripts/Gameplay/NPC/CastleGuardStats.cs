using UnityEngine;
using System.Collections;

public class CastleGuardStats : MonoBehaviour {

	Animator anim;
	AudioSource deathAudio;

	bool wasHit = false;

	void Start () {
		anim = GetComponent<Animator>();
		deathAudio = GetComponentInChildren<AudioSource>();
	}

	public void Hit () {
		if (wasHit) {
			return;
		}

		GlobalVariables.telemetry.PlayerPoofedCastleGuard();
		anim.SetTrigger("hit");
		deathAudio.Play();
		wasHit = true;
	}

	void Disable () {
		Debug.Log("disabled " + gameObject.name);
		gameObject.SetActive(false);
	}
}
