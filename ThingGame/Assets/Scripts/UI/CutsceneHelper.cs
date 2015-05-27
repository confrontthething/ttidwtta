using UnityEngine;
using System.Collections;

public class CutsceneHelper : MonoBehaviour {

  public Camera mainCamera;
	public bool mainCameraOn = true;
	UIController userInterface;

	// Use this for initialization
	void Start () {
		userInterface =
      GameObject.FindGameObjectWithTag(Tags.ui).GetComponent<UIController>();
	}

	void Update() {
		if (mainCamera != null) {
			mainCamera.enabled = mainCameraOn;
		}
	}

	void EnterCutscene() {
		userInterface.EnterCutscene();
	}

	public void ExitCutscene() {
		userInterface.ExitCutscene();
	}

	void EnterDialogueSequence() {
		userInterface.EnterDialogueSequence();
	}

	public void ExitDialogueSequence() {
		userInterface.ExitDialogueSequence();
	}

  void EnterDialogueCutscene() {
    userInterface.EnterDialogueCutscene();
  }

  public void ExitDialogueCutscene() {
    userInterface.ExitDialogueCutscene();
  }

  public void DipWhite() {
    userInterface.DipWhite();
  }

  public void DipBlack() {
    userInterface.DipBlack();
  }

}
