using UnityEngine;
using System.Collections;

public class GameMachine : MonoBehaviour {

	public DPek.Raconteur.RenPy.Display.RenPyDisplay renPyDisplay;
	public DPek.Raconteur.RenPy.RenPyViewBasic renPyView;
	public GameMachineManager manager;
	public AudioSource machineMusic;
	public DreamController dreamController;
	public int dreamNumber = 1;

	bool activatedOnce = false;
	bool doingStuff = false;
	InteractableObject interact = null;
	bool interactAvailable = true;

	void Start() {
		interact = GetComponent<InteractableObject>();
	}

	void Update() {
		if (doingStuff && !machineMusic.isPlaying) {
			machineMusic.Play();
		}
		else if (!doingStuff && machineMusic.isPlaying) {
			machineMusic.Stop();
		}

		if (doingStuff && renPyView.m_label == "end") {
			renPyView.m_label = "obviously_fake";
			dreamController.ExitDream();
			doingStuff = false;
		}

		interact.enabled = !dreamController.IsInDream && interactAvailable;
	}

	public void Activate() {
		if (!doingStuff) {
			if (manager.RequestActivationPermission()) {
				if (!activatedOnce) {
					GlobalVariables.telemetry.IncrementMachineActivations();
					activatedOnce = true;
				}
				Invoke("StartDialog", 1.0f);
				dreamController.EnterDream(dreamNumber);
				doingStuff = true;
			}
		}
	}

	void StartDialog() {
		renPyDisplay.StartDialog();
	}

	public void Deactivate() {
		if (doingStuff) {
			renPyDisplay.StopDialog();
			renPyView.ClearDialogArea();
			dreamController.ExitDream();
			doingStuff = false;
		}
	}

	public void Disable() {
		interact.Deactivate();
		interactAvailable = false;
	}

	public void Enable() {
		interactAvailable = true;
	}

}
