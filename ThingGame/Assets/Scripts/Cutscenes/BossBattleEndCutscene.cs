using UnityEngine;
using System.Collections;

public class BossBattleEndCutscene : MonoBehaviour {

	public DPek.Raconteur.RenPy.Display.RenPyDisplay cutsceneNarration;
	public GameObject frinchfry;
	public GameObject thing;
	public Transform lookAtTarget;
	public CameraMatch firstPersonController;
	public Camera cutsceneCamera;
	public Transform fpcTransform;
	public Animator staffPositionAnimator;
	public SmoothMouseLook look1;
	public SmoothMouseLook look2;

	private UIController userInterface;
	private PlayerStats playerStats;

	void Start() {
		playerStats =
			GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStats>();
	}

	void Update() {
		if (cutsceneNarration.Running) {
			var helpers = GameObject.Find(cutsceneNarration.gameObject.name + " Helpers");
			if (helpers != null) {
				helpers.transform.position = fpcTransform.position;
			}
		}
	}

	public void FrinchfryDied() {
		GetComponent<Animator>().SetTrigger("cutscene");
	}

	void ShowCryingFrinchfryAndThing() {
		frinchfry.SetActive(true);
		thing.transform.parent.gameObject.SetActive(true);
	}

	void Begin() {
		cutsceneNarration.StartDialog();
		staffPositionAnimator.SetBool("hidden", true);
	}

	void End() {
		staffPositionAnimator.SetBool("hidden", false);
	}

	void MatchMainCamera() {
		// Move the main camera into position.
		Vector3 xzPosition = cutsceneCamera.transform.position;
		xzPosition.y = fpcTransform.position.y;
		fpcTransform.position = xzPosition;
		fpcTransform.LookAt(lookAtTarget);

		// Then do an instantaneous match.
		firstPersonController.SaveLocal();
		firstPersonController.MatchWorld(cutsceneCamera, 0.0f);
	}

	void ReturnMainCamera() {
		firstPersonController.ResetLocal();
	}

	void ResetCameraLook() {
		look1.Reset();
		look2.Reset();
	}

	void LockCamera() {
		playerStats.EnterModal();
	}

}
