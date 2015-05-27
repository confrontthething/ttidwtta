using UnityEngine;
using System.Collections;

public class EndCastleGuardBehavior : MonoBehaviour {

	public DPek.Raconteur.RenPy.RenPyViewBasic m_view;
	public Animator modelAnimator;
	Animator movementAnimator;

	CastleGuardStats stats;

	bool narrationStarted = false;
	bool narrationEnded = false;

	void Start () {
		//Initialize vars
		movementAnimator = GetComponent<Animator>();
		stats = GetComponent<CastleGuardStats>();
		stats.enabled = false;
	}

	void Update () {

		//Position the helpers
		GameObject cHelpers = GameObject.Find (gameObject.name + " Helpers");
		if (cHelpers != null) {
						cHelpers.transform.position = gameObject.transform.position;
		}

		if (!narrationEnded) {
			if (!narrationStarted && m_view.m_display.Running) {
				narrationStarted = true;
			}
			if (narrationStarted && !m_view.m_display.Running) {
				narrationEnded = true;
				if (movementAnimator != null) {
					movementAnimator.SetTrigger("move");
				}
			}
		}

		if (modelAnimator != null && modelAnimator.isActiveAndEnabled) {
			modelAnimator.SetBool("talking", narrationStarted && !narrationEnded);
		}
	}

	public void WalkingOn () {
		Debug.Log("WalkingOn()");
		if (modelAnimator != null) {
			modelAnimator.SetBool("walking", true);
		}
	}

	public void WalkingOff () {
		Debug.Log("WalkingOff()");
		if (modelAnimator != null) {
			modelAnimator.SetBool("walking", false);
		}
		if (stats != null) {
			stats.enabled = true;
		}
	}
}
