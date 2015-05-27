using UnityEngine;
using System.Collections;

public class HospitalCutscene : MonoBehaviour {

	public DPek.Raconteur.RenPy.Display.RenPyDisplay display;
	public Transform audioPlacement;
	public Credits credits;

	private UIController userInterface;

	void Start() {
		userInterface =
			GameObject.FindGameObjectWithTag(Tags.ui).GetComponent<UIController>();
	}

	void Update() {
		GameObject helpers = GameObject.Find(display.gameObject.name + " Helpers");
		helpers.transform.position = audioPlacement.position;
	}

	void StartDialogue() {
		display.StartDialog();
	}

	void Begin() {
		userInterface.EnterDialogueCutscene();
	}

	void End() {
		userInterface.ExitDialogueCutscene();
	}

	void ShowCredits() {
		credits.ShowHospitalEnding();
	}

}
