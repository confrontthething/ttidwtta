using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DreamController : MonoBehaviour {

	public Blur blurEffect;
	public bool blurEnabled;
	public Camera dreamCamera;
	public bool dreamCameraEnabled;

	public Image dreamImage1;
	public Image dreamImage2;
	public Sprite[] dreams;

	bool isInDream = false;

	// Update is called once per frame
	void Update() {
		if (blurEffect != null) {
			blurEffect.enabled = blurEnabled;
		}
		if (dreamCamera != null) {
			dreamCamera.enabled = dreamCameraEnabled;
		}
	}

	public void EnterDream(int number) {
		if (isInDream) {
			return;
		}

		if (number < 0 || number >= dreams.Length) {
			return;
		}

		dreamImage1.sprite = dreams[number];
		dreamImage2.sprite = dreams[number];

		GetComponent<Animator>().SetBool("dream1", true);
		isInDream = true;
	}

	public void ExitDream() {
		if (!isInDream) {
			return;
		}

		GetComponent<Animator>().SetBool("dream1", false);
		// Don't set isInDream = true here. Wait for animation to do it.
	}

	void FinalizeExitDream() {
		isInDream = false;
	}

	public bool IsInDream {
		get {
			return isInDream;
		}
	}

}
