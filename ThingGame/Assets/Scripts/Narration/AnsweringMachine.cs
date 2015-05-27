using UnityEngine;
using System.Collections;

public class AnsweringMachine : MonoBehaviour {

	public DPek.Raconteur.RenPy.Display.RenPyDisplay display;
	public DPek.Raconteur.RenPy.RenPyViewBasic view;
	public DPek.Raconteur.RenPy.Display.RenPyDisplay yesDisplay;
	public DPek.Raconteur.RenPy.RenPyViewBasic yesView;
	public DPek.Raconteur.RenPy.Display.RenPyDisplay noDisplay;
	public DPek.Raconteur.RenPy.RenPyViewBasic noView;
	public CharacterMotor charMotor;
	public SmoothMouseLook fpcMouseLook;
	public SmoothMouseLook camMouseLook;
	public Credits credits;
	public Animator promptAnimator;
	bool didBegin = false;
	bool didEndMain = false;
	bool didEndAll = false;
	bool didMakeChoice = false;

	void Update() {
		GameObject helpers = GameObject.Find(display.gameObject.name + " Helpers");
		GameObject helpersY =
			GameObject.Find(yesDisplay.gameObject.name + " Helpers");
		GameObject helpersN =
			GameObject.Find(noDisplay.gameObject.name + " Helpers");
		if (helpers != null) {
			helpers.transform.position = gameObject.transform.position;
		}
		if (helpersY != null) {
			helpersY.transform.position = gameObject.transform.position;
		}
		if (helpersN != null) {
			helpersN.transform.position = gameObject.transform.position;
		}

		if (!didEndMain && view.m_label == "end") {
			// Show the yes/no prompt.
			promptAnimator.SetBool("show", true);
			didEndMain = true;
		}

		if (didEndMain && !didMakeChoice) {
			// Wait for player prompt.
			if (Input.GetKeyDown(KeyCode.Alpha1) ||
					Input.GetKeyDown(KeyCode.Keypad1)) {
				// Yes
				Invoke("StartYes", 0.6f);
				promptAnimator.SetBool("show", false);
				GlobalVariables.telemetry.PlayerSavedVoicemail();
				didMakeChoice = true;
			} else if (Input.GetKeyDown(KeyCode.Alpha2) ||
								 Input.GetKeyDown(KeyCode.Keypad2)) {
				// No
				Invoke("StartNo", 0.6f);
				promptAnimator.SetBool("show", false);
				GlobalVariables.telemetry.PlayerDeletedVoicemail();
				didMakeChoice = false;
			}
		}

		if (!didEndAll && (yesView.m_label == "end" || noView.m_label == "end")) {
			charMotor.enabled = false;
			fpcMouseLook.enabled = false;
			camMouseLook.enabled = false;

			credits.ShowHouseEnding();
			didEndAll = true;
		}
	}

	void StartYes() {
		yesDisplay.StartDialog();
	}

	void StartNo() {
		noDisplay.StartDialog();
	}

	void OnTriggerEnter(Collider other) {
		if (!didBegin && other.gameObject.tag == Tags.player) {
			display.StartDialog();
			didBegin = true;
		}
	}

}
