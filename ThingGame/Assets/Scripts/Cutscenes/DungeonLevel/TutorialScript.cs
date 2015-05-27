using UnityEngine;
using System.Collections;

public class TutorialScript : MonoBehaviour {

	public GameObject entranceDoor;
	SecretDoor entranceDoorScript;
	public GameObject exitDoor;
	SecretDoor exitDoorScript;

	public GameObject singleEnemyGroup;
	public GameObject multiEnemyGroup;
	public GameObject enemyLight;
	bool initEnemies = true;

	public Animator tutorialCamera;

	public BoxCollider trigger;
	public GameObject entranceWall;

	public DPek.Raconteur.RenPy.Display.RenPyDisplay dialogue;

	PlayerStats playerStats;

	bool tutorialActive = false;

	void Start () {
		entranceDoorScript = entranceDoor.GetComponent<SecretDoor>();
		exitDoorScript = exitDoor.GetComponent<SecretDoor>();

		playerStats = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStats>();
	}

	void Update () {
		if (initEnemies) {
			singleEnemyGroup.SetActive(false);
			multiEnemyGroup.SetActive(false);
			initEnemies = false;
		}

		if (!tutorialActive) {
			return;
		}

		if (singleEnemyGroup.activeSelf && singleEnemyGroup.transform.childCount == 0) {
			EndSpawnSingleEnemy();
		}
		if (multiEnemyGroup.activeSelf && multiEnemyGroup.transform.childCount == 0) {
			EndSpawnMultiEnemy();
		}
	}

	// Close entrance
	void CloseEntranceDoor () {
		Debug.Log("CloseEntranceDoor()");
		entranceDoorScript.isActivated = false;
		entranceDoorScript.isOpen = false;
		entranceWall.SetActive(true);
		Invoke("StartSpawnSingleEnemy", 3f);
	}

	// Single enemy
	void StartSpawnSingleEnemy () {
		Debug.Log("StartSpawnSingleEnemy()");
		tutorialCamera.SetTrigger("enemies");
		enemyLight.SetActive(true);
		Invoke("SpawnSingleEnemy", 1f);
	}
	void SpawnSingleEnemy () {
		Debug.Log("SpawnSingleEnemy()");
		singleEnemyGroup.SetActive(true);
		Invoke("StartDialog", 2f);
	}
	void StartDialog() {
		dialogue.StartDialog();
	}
	void EndSpawnSingleEnemy () {
		Debug.Log("EndSpawnSingleEnemy()");
		singleEnemyGroup.SetActive(false);

		StartSpawnMultiEnemy();
	}

	// Multiple enemies
	void StartSpawnMultiEnemy () {
		Debug.Log("StartSpawnMultiEnemy()");
		tutorialCamera.SetTrigger("enemies");
		Invoke("SpawnMultiEnemy", 1f);
		Invoke("ReleaseEnemies", 4f);
	}
	void SpawnMultiEnemy () {
		Debug.Log("SpawnMultiEnemy()");
		foreach (EnemyAI ai in multiEnemyGroup.GetComponentsInChildren<EnemyAI>()) {
			ai.roam = false;
		}
		multiEnemyGroup.SetActive(true);
	}
	void ReleaseEnemies () {
		foreach (EnemyAI ai in multiEnemyGroup.GetComponentsInChildren<EnemyAI>()) {
			ai.roam = true;
		}
	}
	void EndSpawnMultiEnemy () {
		Debug.Log("EndSpawnMultiEnemy()");
		multiEnemyGroup.SetActive(false);

		OpenExitDoor();
	}

	// Open exit
	void OpenExitDoor () {
		Debug.Log("OpenExitDoor()");
		exitDoorScript.isActivated = true;
		exitDoorScript.isOpen = true;

		EndTutorial();
	}


	// Start tutorial
	void StartTutorial () {
		tutorialActive = true;
		trigger.enabled = false;
		playerStats.permanentInvincibility = true;

		CloseEntranceDoor();
	}
	// End tutorial
	void EndTutorial () {
		tutorialActive = false;
		playerStats.permanentInvincibility = false;

		enabled = false;
	}

	// Start trigger
	void OnTriggerEnter (Collider other) {
		if (!other.isTrigger && other.gameObject.tag == Tags.player) {
			Debug.Log("TutorialScript : did trigger!");
			StartTutorial();
		}
	}
}
