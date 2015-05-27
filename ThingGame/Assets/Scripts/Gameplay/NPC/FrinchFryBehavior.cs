using UnityEngine;
using System.Collections;

public class FrinchFryBehavior : MonoBehaviour {

	public DPek.Raconteur.RenPy.Display.RenPyDisplay[] renPyDisplays;
	public DPek.Raconteur.RenPy.RenPyViewBasic[] renPyViews;
	public GameObject m_cellDoor;
	public Animator m_gameLight;
	private bool m_mainDialogPlayed;
	public bool debugMode;

	private Animator animator;

	void Start () {
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
				//Position the audio helpers
				GameObject levOneHelpers = GameObject.Find ("Level one Helpers");
				GameObject levTwoHelpers = GameObject.Find ("Level two Helpers");
				GameObject levThrHelpers = GameObject.Find ("Level three Helpers");

				if (levOneHelpers != null) {
						levOneHelpers.transform.position = gameObject.transform.position;
				}
				if (levTwoHelpers != null) {
						levTwoHelpers.transform.position = gameObject.transform.position;
				}
				if (levThrHelpers != null) {
						levThrHelpers.transform.position = gameObject.transform.position;
				}

				if (animator != null) {
						bool displayRunning = false;
						foreach (var display in renPyDisplays) {
							displayRunning |= display.Running;
						}
						animator.SetBool ("talking", displayRunning);
				}

				if (m_gameLight != null &&
						((GlobalVariables.gameLevel != Scenes.gameDungeon) ||
						 (renPyViews[0].m_label == "end"))) {
						m_gameLight.SetBool("on", true);
				}

				//AUTOSTART DIALOUGUE
				if (GlobalVariables.gameLevel == Scenes.gameCourtyard) {
						if (!m_mainDialogPlayed) {
								m_mainDialogPlayed = true;
								renPyDisplays [1].StartDialog ();
						}
				}
				if (GlobalVariables.gameLevel == Scenes.gameThroneRoom) {
						// Welcome (default)Script
						if (!m_mainDialogPlayed) {
								m_mainDialogPlayed = true;
								renPyDisplays [2].StartDialog ();
						}
				}
		}

		void OnTriggerStay(Collider other) {
		//If PLAYER enters range
		if (other.CompareTag("Player") && !debugMode) {
			if(GlobalVariables.gameLevel == Scenes.gameDungeon){
				// Welcome (default)Script
				if(!m_mainDialogPlayed){
					m_mainDialogPlayed = true;
					renPyDisplays[0].StartDialog();
				}
			}
			/*if(GlobalVariables.gameLevel == Scenes.gameCourtyard){
				if(!m_mainDialogPlayed){
					m_mainDialogPlayed = true;
					renPyDisplays[1].StartDialog();
				}
			}
			if(GlobalVariables.gameLevel == Scenes.gameThroneRoom){
				// Welcome (default)Script
				if(!m_mainDialogPlayed){
					m_mainDialogPlayed = true;
					renPyDisplays[2].StartDialog();
				}
			}*/
		}
	}
}
