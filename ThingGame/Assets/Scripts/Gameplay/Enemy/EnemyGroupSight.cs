using UnityEngine;
using System.Collections;

public class EnemyGroupSight : MonoBehaviour {

	private EnemySight[] enemies;

	private bool playerInSight = false;
	private Vector3 lastSighting;

	// Use this for initialization
	void Start () {
		enemies = gameObject.GetComponentsInChildren<EnemySight>();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.childCount == 0) {
			gameObject.SetActive(false);
		}

		if (transform.childCount != enemies.Length) {
			ResetEnemyArray();
		}

		playerInSight = false;
		foreach (EnemySight enemy in enemies) {
			if (enemy.PlayerInSight) {
				playerInSight = true;
				lastSighting = enemy.PersonalLastSighting;
			}
		}
	}

	public bool PlayerInSight {
		get { return playerInSight; }
	}

	public Vector3 LastSighting {
		get { return lastSighting; }
	}

	public void ResetEnemyArray () {
		enemies = gameObject.GetComponentsInChildren<EnemySight>();
	}

	public void OverridePlayerInSight () {
		playerInSight = true;
		foreach (EnemySight enemy in enemies) {
			lastSighting = enemy.PersonalLastSighting;
		}
	}
}
