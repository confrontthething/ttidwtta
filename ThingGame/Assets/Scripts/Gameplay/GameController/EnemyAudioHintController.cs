using UnityEngine;
using System.Collections;

public class EnemyAudioHintController : MonoBehaviour {

	public int numEnemiesDefeated;
	private int maxEnemiesDefeated = 3;
	public DPek.Raconteur.RenPy.RenPyViewBasic m_view;
	public DPek.Raconteur.RenPy.RenPyViewBasic[] m_views;
	private int m_encounterNum;
	private bool m_mainDialogPlayed;
	public NarrationSync narrationSync;
	public bool debugMode;

	GameObject mainCamera;

	private string currentScene;

	void Start () {
		m_view = GetComponent<DPek.Raconteur.RenPy.RenPyViewBasic> ();
		m_mainDialogPlayed = false;
		numEnemiesDefeated = 0;

		mainCamera = GameObject.FindGameObjectWithTag(Tags.mainCamera);

		currentScene = Application.loadedLevelName;
	}

	void Update () {

		// Position the helpers on the main camera
		if (mainCamera != null) {
			GameObject gcHelpers = GameObject.Find ("GameController Helpers");
			if (gcHelpers != null) {
				gcHelpers.transform.position = mainCamera.transform.position;
			}
			foreach (var view in m_views) {
				GameObject go = view.m_display.gameObject;
				GameObject helpers = GameObject.Find(go.name + " Helpers");
				if (helpers != null) {
					helpers.transform.position = mainCamera.transform.position;
				}
			}
		}

		switch (currentScene) {
		case Scenes.gameDungeon :
			if (numEnemiesDefeated >= maxEnemiesDefeated) {
				if(!m_mainDialogPlayed){
					m_mainDialogPlayed = true;
					m_view.m_autoStart = true;
					m_view.m_display.StartDialog ();
				}
				/*switch (m_encounterNum) {
				case 0:
					// Line1

					//m_encounterNum++;
					if(!m_mainDialougePlayed){
						m_mainDialougePlayed = true;
						m_view.m_autoStart = true;
						m_view.Start ();
					}

					break;
				case 1:
					//Line2
					//m_encounterNum++;
					//Update m_view.m_display.target_script to tutorial
					m_view.Start ();
					break;
				case 3:
					// Instruction script
					//Update m_view.m_display.target_script to tutorial
					m_view.m_autoStart = true;
					m_view.Start ();
					break;
				default:
					// Show nothing for this line, proceed to the next one.
					break;
				}*/

				numEnemiesDefeated = 0;
			}
			break;
		case Scenes.gameCourtyard:
			if (numEnemiesDefeated >= maxEnemiesDefeated) {
				if (m_encounterNum < m_views.Length) {
					if (narrationSync != null) {
						narrationSync.ForceStopDialogue();
					}
					m_views[m_encounterNum].m_display.StartDialog();
					m_encounterNum++;
					numEnemiesDefeated = 0;
				}
			}
			break;
		}
	}
}
