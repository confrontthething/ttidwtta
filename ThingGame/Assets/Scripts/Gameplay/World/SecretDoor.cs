using UnityEngine;
using System.Collections;

public class SecretDoor : MonoBehaviour {

	public bool isActivated;
	public bool isOpen;

	private Animator animator;

	private bool prevIsOpen;
	private AudioSource audioSrc;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		audioSrc = GetComponent<AudioSource>();

		prevIsOpen = isOpen;
	}

	void OnTriggerStay(Collider other) {
		if (!isOpen && enabled && isActivated
				&& other.gameObject.tag == Tags.player) {
			isOpen = true;
		}
	}

	// Update is called once per frame
	void Update () {
		animator.SetBool("activated", isActivated);
		animator.SetBool("open", isOpen);

		if (isOpen != prevIsOpen) {
			audioSrc.Play();
			prevIsOpen = isOpen;
		}
	}

}
