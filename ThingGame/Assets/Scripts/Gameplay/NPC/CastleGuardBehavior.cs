using UnityEngine;
using System.Collections;

public class CastleGuardBehavior : MonoBehaviour {

	private DPek.Raconteur.RenPy.RenPyViewBasic m_view;

	public GameObject m_cellDoor;
	public GameObject m_cellRoomDoor;
	public Animator modelAnimator;

//	private bool m_gateopened;
//	private bool m_dungeonopened;
	private bool m_mainDialogPlayed;

	public bool debugMode;

	private int m_encounterNum;

	// Use this for initialization
	void Start () {
		//Initialize vars
		m_view = GetComponent<DPek.Raconteur.RenPy.RenPyViewBasic> ();
		m_cellDoor = GameObject.Find ("bars 1");
		m_cellRoomDoor = GameObject.Find ("CellRoomDoor");
		m_mainDialogPlayed = false;

		if (!debugMode) {
			m_view = GetComponent<DPek.Raconteur.RenPy.RenPyViewBasic> ();
//			m_gateopened = false;
//			m_dungeonopened = false;
		} else {
//			m_gateopened = true;
			m_cellDoor.GetComponent<Animator> ().SetBool ("barsOpen", true);
			m_cellRoomDoor.GetComponent<Animator> ().SetBool ("open", true);
		}
	}//End Start()


	// Update is called once per frame
	void Update () {

		//Position the helpers
		GameObject cHelpers = GameObject.Find (gameObject.name + " Helpers");
		cHelpers.transform.position = gameObject.transform.position;

		if (m_view.m_display.Running && modelAnimator != null) {
			modelAnimator.SetBool("talking", true);
			modelAnimator.SetBool("walking", false);
		} else {
			modelAnimator.SetBool("talking", false);
		}

		//CUE OPEN GATE
		if(m_view.m_label == "raiseBars" && !debugMode){
			if(m_cellDoor != null){
//				m_gateopened = true;
				m_cellDoor.GetComponent<Animator>().SetBool("barsOpen", true);
			}
		}
		//CUE OPEN DUNGEON DOOR
		if(m_view.m_label == "openCellRoomDoor" && !debugMode){
			if(m_cellRoomDoor != null){
				//m_gateopened = true;
				m_cellRoomDoor.GetComponent<Animator>().SetBool("open", true);
			}
		}

	}

	void OnTriggerEnter(Collider other){
		//If PLAYER enters range
		if(other.CompareTag("Player") && !debugMode){
				// Welcome (default)Script
				//m_encounterNum++;
				if(!m_mainDialogPlayed){
					m_mainDialogPlayed = true;
					m_view.m_autoStart = true;
					m_view.m_display.StartDialog ();
				}

		}
	}
}
