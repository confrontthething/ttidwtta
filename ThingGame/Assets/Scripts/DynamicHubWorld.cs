using UnityEngine;
using System.Collections;

public class DynamicHubWorld : MonoBehaviour {

	public PlayerStats playerStats;
	public Transform newPlayerPos;
	public GameObject frinchfry;

	public GameObject[] thingCutsceneObjects;
	public GameObject redFlash;

	public InteractableObject intObj;

	public Animator doorAnim;
	public GameObject doorSounds;

	public GameObject leakingSmoke1;
	public GameObject leakingSmoke2;

	void Awake () {
		if (GlobalVariables.gameLevel != Scenes.gameDungeon) {
			playerStats.modalByDefault = false;
			DisableIntro();
			Vector3 lookAtTarget = new Vector3(frinchfry.transform.position.x,
				playerStats.gameObject.transform.position.y,
				frinchfry.transform.position.z);
			playerStats.gameObject.transform.LookAt(lookAtTarget);
		}
	}

	void Start () {
		if (GlobalVariables.gameLevel != Scenes.gameDungeon) {
			switch (GlobalVariables.gameLevel) {
			case Scenes.gameCourtyard :
				EnableAfterDungeon();
				break;
			case Scenes.gameThroneRoom :
				EnableAfterCourtyard();
				break;
			}
		}
	}

	void DisableIntro () {
		foreach (GameObject obj in thingCutsceneObjects) {
			obj.SetActive(false);
		}
		redFlash.SetActive(false);
	}

	void EnableAfterDungeon () {
		// TODO: player frinchfry's after dungeon dialogue
		if (newPlayerPos != null) {
			Transform player = playerStats.gameObject.transform;
			player.position = newPlayerPos.position;
		}
		if (leakingSmoke1 != null) {
			leakingSmoke1.SetActive(true);
		}
	}

	void EnableAfterCourtyard () {
		// TODO: player frinchfry's after courtyard dialogue
		if (newPlayerPos != null) {
			Transform player = playerStats.gameObject.transform;
			player.position = newPlayerPos.position;
		}
		if (leakingSmoke2 != null) {
			leakingSmoke2.SetActive(true);
		}
	}
}
