using UnityEngine;
using System.Collections;

public class OtherworldDialogue : MonoBehaviour {

	public DPek.Raconteur.RenPy.Display.RenPyDisplay[] displays;
	public DPek.Raconteur.RenPy.RenPyViewBasic[] views;
	public bool debug = false;

	private DPek.Raconteur.RenPy.Display.RenPyDisplay activeDisplay;
	private DPek.Raconteur.RenPy.RenPyViewBasic activeView;
	private bool didTrigger = false;

	// Use this for initialization
	void Start() {
		if (debug) {
			GlobalVariables.gameLevel = Scenes.gameCourtyard;
		}

		switch (GlobalVariables.gameLevel) {
			case Scenes.gameCourtyard:
				activeDisplay = displays[0];
				activeView = views[0];
				break;
			case Scenes.gameThroneRoom:
				activeDisplay = displays[1];
				activeView = views[1];
				break;
			default:
				throw new System.InvalidOperationException(
					"Did not come from Castle Nova game world. Did you want Debug Mode?"
				);
		}
	}

	// Update is called once per frame
	void Update() {
		foreach (Transform child in transform) {
			child.localPosition = Vector3.zero;
		}

		if (activeView != null && activeView.m_label == "end") {
			this.enabled = false;
			UIFade fader =
				GameObject.FindGameObjectWithTag(Tags.ui).GetComponent<UIFade>();
			fader.LoadLevel(Scenes.hubWorld);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (!didTrigger) {
			activeDisplay.StartDialog();
			didTrigger = true;
		}
	}

}
