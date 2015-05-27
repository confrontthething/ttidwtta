using UnityEngine;
using System.Collections;

public class ThroneRoomNarration : MonoBehaviour {

	public DPek.Raconteur.RenPy.Display.RenPyDisplay frinchfryNarration;
	public DPek.Raconteur.RenPy.RenPyViewBasic frinchfryView;
	public ThroneRoomThing throneRoomThing;
	public ThroneRoomCutscene throneRoomCutscene;
	public Transform frinchfryAudioLocation;

	public AudioSource backgroundMusic;

	bool didEnter = false;
	bool didBegin = false;

	// Update is called once per frame
	void Update() {
		GameObject helpers =
			GameObject.Find(frinchfryNarration.gameObject.name + " Helpers");
		helpers.transform.position = frinchfryAudioLocation.position;

		if (didEnter && !didBegin) {
			frinchfryNarration.StartDialog();
			StartCoroutine(FadeOutBGMusic());
			throneRoomThing.attackable = true;
			didBegin = true;
		}
	}

	public void ThingHit() {
		frinchfryNarration.StopDialog();
		frinchfryView.ClearDialogArea();
		throneRoomCutscene.BeginCutscene();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == Tags.player) {
			didEnter = true;
		}
	}
	
	IEnumerator FadeOutBGMusic () {
		while (backgroundMusic.volume > 0.0f) {
			backgroundMusic.volume = Mathf.Max(backgroundMusic.volume - 0.05f, 0.0f);
			yield return new WaitForSeconds(0.1f);
		}
	}

}
