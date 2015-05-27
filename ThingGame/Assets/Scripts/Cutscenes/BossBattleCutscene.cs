using UnityEngine;
using System.Collections;

public class BossBattleCutscene : MonoBehaviour {

	public Animator thingAnimator;
	public Animator doorAnimator;
	public Animator frinchfryAnimator;
	public Animator bossUIAnimator;

	public Animator bossCutsceneUIAnimator;

	public Animator backgroundMusic;

	public GameObject smallFrinchfry;
	Animator smallFrinchfryAnim;
	public MeshRenderer[] bigFrinchfry;
	public GameObject[] targets;

	public GameObject skipText;

	public BossAI boss;
	public DPek.Raconteur.RenPy.Display.RenPyDisplay renPyDisplay;
	public DPek.Raconteur.RenPy.RenPyViewBasic renPyView;
	public Transform player;

	public BossGrid grid;

	Animator cutsceneAnimator;
	CutsceneHelper helper;
	bool didFinish = false;

	bool skipEnabled = false;

	void Start () {
		cutsceneAnimator = GetComponent<Animator>();
		helper = GetComponent<CutsceneHelper>();
		GameObject dialogueHelpers =
			GameObject.Find(renPyDisplay.gameObject.name + " Helpers");
		dialogueHelpers.transform.localPosition = Vector3.zero;

		smallFrinchfry.SetActive(true);
		foreach (MeshRenderer mr in bigFrinchfry) {
			mr.enabled = false;
		}
		foreach (GameObject obj in targets) {
			obj.SetActive(false);
		}

		smallFrinchfryAnim = smallFrinchfry.GetComponent<Animator>();
		smallFrinchfryAnim.speed = 0.6f;
	}

	void Update () {
		if (!didFinish) {
			if (skipEnabled) {
				if (Input.GetKeyDown(KeyCode.Tab)) {
					frinchfryAnimator.SetTrigger("skip");
					bossUIAnimator.SetBool("visible", true);
					doorAnimator.SetTrigger("skip");
					thingAnimator.SetTrigger("skip");
					cutsceneAnimator.SetTrigger("skip");
					renPyDisplay.StopDialog();
					renPyView.ClearDialogArea();
					SwitchFrinchfries();
					DisableSkip();
					EnableBoss();
					helper.ExitDialogueCutscene();
				}
			}
		}

		if (renPyDisplay.Running) {
			var helpers = GameObject.Find(renPyDisplay.gameObject.name + " Helpers");
			if (helpers != null) {
				helpers.transform.position = player.position;
			}
		}
	}

	void EnableSkip () {
		skipEnabled = true;
		skipText.SetActive(true);
	}

	void DisableSkip () {
		skipEnabled = false;
		skipText.SetActive(false);
	}

	void MakeFrinchfryBig() {
		frinchfryAnimator.SetTrigger("activate");
		Invoke ("DisplayBossHealthBar", 2f);
	}

	void DisplayBossHealthBar() {
		bossUIAnimator.SetBool("visible", true);
	}

	void OpenDoor() {
		doorAnimator.SetTrigger("open");
	}

	void MakeThingFly() {
		thingAnimator.SetTrigger("fly");
	}

	void ToggleFrinchfryTalkingAnimation() {
		smallFrinchfryAnim.SetBool("talking", !smallFrinchfryAnim.GetBool("talking"));
	}

	void BossDipWhite() {
		bossCutsceneUIAnimator.SetTrigger("dipWhite");
	}

	void SwitchFrinchfries() {
		smallFrinchfry.SetActive(false);
		foreach (MeshRenderer mr in bigFrinchfry) {
			mr.enabled = true;
		}
		foreach (GameObject obj in targets) {
			obj.SetActive(true);
		}
	}

	void StartDialogue() {
		renPyDisplay.StartDialog();
	}

	void EnableBoss() {
		backgroundMusic.SetTrigger("crossfade");
		boss.isOn = true;
		didFinish = true;
		grid.EnableLines();
	}

}
