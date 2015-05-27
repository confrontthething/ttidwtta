using UnityEngine;
using System.Collections;

public class CourtYardNarration : MonoBehaviour {

	private DPek.Raconteur.RenPy.RenPyViewBasic m_view;
	private bool m_mainDialogPlayed;
	public bool m_tutorialgiven;

	// Use this for initialization
	void Start () {
		//Initialize vars
		m_view = GetComponent<DPek.Raconteur.RenPy.RenPyViewBasic> ();
		m_mainDialogPlayed = false;


	}//End Start()

	// Update is called once per frame
	void Update () {
		//Position the helpers
		GameObject tutHelpers = GameObject.Find ("TutNarration Helpers");
		GameObject noCodeHelpers = GameObject.Find ("No codes Helpers");
		GameObject oneCodeHelpers = GameObject.Find ("One code Helpers");
		GameObject twoCodeHelpers = GameObject.Find ("Two code Helpers");
		GameObject allCodeHelpers = GameObject.Find ("All codes Helpers");

		if (tutHelpers != null) {
			tutHelpers.transform.position = GameObject.FindGameObjectWithTag ("Player").transform.position;
		}
		if (noCodeHelpers != null) {
			noCodeHelpers.transform.position = GameObject.FindGameObjectWithTag ("Player").transform.position;
		}
		if (oneCodeHelpers != null) {
			oneCodeHelpers.transform.position = GameObject.FindGameObjectWithTag ("Player").transform.position;
		}
		if (twoCodeHelpers != null) {
			twoCodeHelpers.transform.position = GameObject.FindGameObjectWithTag ("Player").transform.position;
		}
		if (allCodeHelpers != null) {
			allCodeHelpers.transform.position = GameObject.FindGameObjectWithTag ("Player").transform.position;
		}
	}

	void OnTriggerEnter(Collider other){
		//If PLAYER enters range
		Debug.Log ("TRIGGER ENTERED");
		if(other.CompareTag("Player") && !m_mainDialogPlayed){
			//Debug.Log ("TRIGGER LAUNCHED");
			m_mainDialogPlayed = true;
			m_view.m_autoStart = true;
			m_view.m_display.StartDialog();
		}
	}

}
