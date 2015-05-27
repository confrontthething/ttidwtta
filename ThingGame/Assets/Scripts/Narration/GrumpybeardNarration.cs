using UnityEngine;
using System.Collections;

public class GrumpybeardNarration : MonoBehaviour {

	public DPek.Raconteur.RenPy.Display.RenPyDisplay grumpybeardNarration;
	public DPek.Raconteur.RenPy.RenPyViewBasic grumpybeardView;

	public ThroneRoomNarration throneRoomController;
	public Transform audioLocation;

	bool didBegin = false;

	// Update is called once per frame
	void Update() {
		GameObject helpers =
			GameObject.Find(grumpybeardNarration.gameObject.name + " Helpers");
		helpers.transform.position = audioLocation.position;

		if (grumpybeardView.m_label == "end") {
			throneRoomController.enabled = true;
			enabled = false;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == Tags.player && !didBegin) {
			grumpybeardNarration.StartDialog();
			didBegin = true;
		}
	}

}
