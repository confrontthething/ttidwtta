using UnityEngine;
using System.Collections;

public class NarrationSync : MonoBehaviour {

	public DPek.Raconteur.RenPy.RenPyViewBasic[] renPyViews;

	public void ForceStopDialogue() {
		foreach (var view in renPyViews) {
			var display = view.m_display;
			if (display.Running) {
				display.StopDialog();
				view.ClearDialogArea();
			}
		}
	}

}
