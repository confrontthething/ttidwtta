using UnityEngine;
using System.Collections;

public class NarrationObjectZeroController : MonoBehaviour {

	public Animator castleGuardAnimator;
	public Animator castleGuardModelAnimator;
	public Transform playerTransform;
	private GameObject dialogueHelpers = null;

	private DPek.Raconteur.RenPy.RenPyViewBasic m_view;

	// Use this for initialization
	void Start () {
		m_view = GetComponent<DPek.Raconteur.RenPy.RenPyViewBasic> ();
	}

	// Update is called once per frame
	void Update () {
		dialogueHelpers =
			dialogueHelpers ?? GameObject.Find(gameObject.name + " Helpers");

		if (dialogueHelpers) {
			dialogueHelpers.transform.position = playerTransform.position;
		}

		if (m_view.m_label == "retControl") {
			castleGuardAnimator.SetTrigger("approach");
			castleGuardModelAnimator.SetBool("walking", true);
			this.enabled = false;
		}
	}
}
