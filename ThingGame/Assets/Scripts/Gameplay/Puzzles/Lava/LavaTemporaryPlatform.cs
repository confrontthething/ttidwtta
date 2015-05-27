using UnityEngine;
using System.Collections;

public class LavaTemporaryPlatform : MonoBehaviour {

	public float delayTime = 0f;
	float delayTimer = 0f;
	bool didTrigger = false;

	public AudioClip breakingAudio;
	public AudioClip brokenAudio;
	AudioSource audioSource;

	Animator anim;

	void Start () {
		anim = GetComponent<Animator>();
		audioSource = GetComponentInChildren<AudioSource>();
	}

	void Update () {
		if (didTrigger) {
			delayTimer -= Time.deltaTime;
			if (delayTimer <= 0f) {
				didTrigger = false;
				anim.SetTrigger("break");
				PlayBreakingSound();
			}
		}
	}

	void OnTriggerEnter (Collider other) {
		if (!other.isTrigger && other.tag == Tags.player) {
			if (!didTrigger) {
				didTrigger = true;
				delayTimer = delayTime;
			}
		}
	}

	void PlayBreakingSound () {
		audioSource.PlayOneShot(breakingAudio);
	}

	void PlayerBrokenSound () {
		audioSource.PlayOneShot(brokenAudio);
	}
}
