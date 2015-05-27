using UnityEngine;
using System.Collections;

public class GameMachineManager : MonoBehaviour {

	public DPek.Raconteur.RenPy.Display.RenPyDisplay[] ffRenPyDisplays;

	public GameMachine[] machines;

	private bool frinchfrySpeaking = false;

	void Update() {
		bool newFrinchfrySpeaking = false;
		foreach (var disp in ffRenPyDisplays) {
			newFrinchfrySpeaking |= disp.Running;
		}

		if (frinchfrySpeaking != newFrinchfrySpeaking) {
			if (newFrinchfrySpeaking) {
				FrinchfryDidStartSpeaking();
			} else {
				FrinchfryDidEndSpeaking();
			}
			frinchfrySpeaking = newFrinchfrySpeaking;
		}
	}

	public bool RequestActivationPermission() {
		if (frinchfrySpeaking) {
			return false;
		} else {
			foreach (GameMachine gm in machines) {
				gm.Deactivate();
			}
			return true;
		}
	}

	void FrinchfryDidStartSpeaking() {
		foreach (GameMachine gm in machines) {
			gm.Deactivate();
			gm.Disable();
		}
	}

	void FrinchfryDidEndSpeaking() {
		foreach (GameMachine gm in machines) {
			gm.Enable();
		}
	}

}
