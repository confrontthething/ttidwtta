using UnityEngine;
using System.Collections;

public class BridgeEncounter : MonoBehaviour {

	public Camera bridgeCamera;
	Animator cameraAnim;
	public AnimationClip doorRevealClip;
	public LayerMask doorCullingMask;

	public SecretDoor secretDoor;

	public GameObject firePitGroup;
	public GameObject entranceGroup;

	public GameObject[] lights;

	public GameObject narration;

	// initial initializer boolean
	bool init = false;

	// is bridge encounter being initialized
	bool isInitActive = false;
	// is bridge encounter active
	bool isActive = false;

	// total number of enemies
	int numEnemies;

	// spawn delay
	float spawnDelayTimer;
	float spawnDelay = 0.5f;

	void Start () {
		cameraAnim = bridgeCamera.gameObject.GetComponent<Animator>();
		narration.SetActive(false);
	}
	
	void FixedUpdate () {
		if (!init) {
			firePitGroup.SetActive(false);
			entranceGroup.SetActive(false);
			
			foreach (GameObject light in lights) {
				light.SetActive(false);
			}

			secretDoor.isActivated = false;
			
			numEnemies = firePitGroup.transform.childCount + entranceGroup.transform.childCount;

			init = true;
		}

		if (isInitActive) {
			if (numEnemies != 0) {
				if (spawnDelayTimer <= 0) {
					foreach (Transform child in firePitGroup.transform) {
						if (!child.gameObject.activeSelf) {
							// spawn in enemy
							child.gameObject.SetActive(true);
							break;
						}
					}

					foreach (Transform child in entranceGroup.transform) {
						if (!child.gameObject.activeSelf) {
							// spawn in enemy
							child.gameObject.SetActive(true);
							break;
						}
					}

					// decrement numEnemies and reset timer
					numEnemies--;
					spawnDelayTimer = spawnDelay;
				}
				// decrement timer
				spawnDelayTimer -= Time.fixedDeltaTime;

			} else {
				// all enemies spawned in. begin encounter.
				BeginEncounter();
			}
		}
		else if (isActive) {
			// all enemies dead. end encounter.
			if (firePitGroup.transform.childCount == 0 && entranceGroup.transform.childCount == 0) {
				EndEncounter();
			}
		}
	}

	public void InitEncounter () {
		// activate fire pit group
		firePitGroup.SetActive(true);
		foreach (Transform child in firePitGroup.transform) {
			// set up enemy
			EnemySight sight = child.GetComponent<EnemySight>();
			sight.enabled = false;
			EnemyAI ai = child.GetComponent<EnemyAI>();
			ai.roam = false;
			// hide enemy to spawn in later
			child.gameObject.SetActive(false);
		}

		// activate entrance group
		entranceGroup.SetActive(true);
		foreach (Transform child in entranceGroup.transform) {
			// set up enemy
			EnemySight sight = child.GetComponent<EnemySight>();
			sight.enabled = false;
			EnemyAI ai = child.GetComponent<EnemyAI>();
			ai.roam = false;
			// hide enemy to spawn in later
			child.gameObject.SetActive(false);
		}

		// turn on lights
		foreach (GameObject light in lights) {
			light.SetActive(true);
		}

		// set spawn delay
		spawnDelayTimer = spawnDelay;

		// enable camera? TODO: figure out how to do this
		cameraAnim.SetTrigger("enabled");

		// start spawning in enemies
		isInitActive = true;
	}

	void BeginEncounter () {
		// enable fire pit enemy combat scripts
		foreach (Transform child in firePitGroup.transform) {
			// set up enemy
			EnemySight sight = child.GetComponent<EnemySight>();
			sight.enabled = true;
			sight.ResetGroupSight();
			EnemyAI ai = child.GetComponent<EnemyAI>();
			ai.roam = true;
		}
		EnemyGroupSight groupSight = firePitGroup.GetComponent<EnemyGroupSight>();
		groupSight.ResetEnemyArray();

		// enable entrance enemy combat scripts
		foreach (Transform child in entranceGroup.transform) {
			// set up enemy
			EnemySight sight = child.GetComponent<EnemySight>();
			sight.enabled = true;
			sight.ResetGroupSight();
			EnemyAI ai = child.GetComponent<EnemyAI>();
			ai.roam = true;
		}
		groupSight = entranceGroup.GetComponent<EnemyGroupSight>();
		groupSight.ResetEnemyArray();

		isInitActive = false;
		isActive = true;

		narration.SetActive(true);
	}

	void EndEncounter () {
		isActive = false;

		bridgeCamera.cullingMask = doorCullingMask;
		cameraAnim.SetTrigger("door");
		Invoke("ActivateDoor", 2f);
		Invoke("DisableEncounter", doorRevealClip.length);
	}

	void ActivateDoor () {
		secretDoor.isActivated = true;
	}

	void DisableEncounter () {
		enabled = false;
	}

}
