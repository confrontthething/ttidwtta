using UnityEngine;
using System.Collections;

public class TutorialDialogScript : MonoBehaviour {

	private DPek.Raconteur.RenPy.RenPyViewBasic m_view; 
	private bool m_mainDialogPlayed = false;
//	public bool m_tutorialgiven;

	// Use this for initialization
	void Start () {
		//Initialize vars
		m_view = GetComponent<DPek.Raconteur.RenPy.RenPyViewBasic> ();
		
		
	}//End Start()
	
	// Update is called once per frame
	void Update () {
		//Position the helpers
		GameObject helpers = GameObject.Find(gameObject.name + " Helpers");
		if (helpers != null) {
			helpers.transform.position = GameObject.FindGameObjectWithTag (Tags.player).transform.position;
		}
	}

	void OnTriggerStay (Collider other) {
		//If PLAYER enters range
		//Debug.Log ("TRIGGER ENTERED");
		if (other.CompareTag(Tags.player) && !m_mainDialogPlayed) {
			//Debug.Log ("TRIGGER LAUNCHED");
				m_mainDialogPlayed = true;
				m_view.m_autoStart = true;
				m_view.m_display.StartDialog();
		}
	}
}
