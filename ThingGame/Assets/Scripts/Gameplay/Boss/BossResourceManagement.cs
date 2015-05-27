using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossResourceManagement : MonoBehaviour {

	public BossGrid grid;
	public BossStats bossStats;
	public PlayerStats playerStats;

	public GameObject healthDrop;
	Animator healthDropAnimator;
	public GameObject rechargeDrop;
	Animator rechargeDropAnimator;

	public bool preciseDrops = true;

	public GameObject interactArea;

	bool rechargeOn = false;
	bool healthOn = false;

	bool didHideOnDeath = false;

	void Start () {
		healthDropAnimator = healthDrop.GetComponent<Animator>();
		rechargeDropAnimator = rechargeDrop.GetComponent<Animator>();
	}

	void Update () {
		if (!bossStats.IsAlive) {
			if (!didHideOnDeath) {
				Defeated();
			}
			return;
		}

		// manage mana
		if (!rechargeOn && playerStats.Magic <= playerStats.MaxMagic/2) {
			ActivateRecharge();
		}
		else if (rechargeOn && playerStats.Magic == playerStats.MaxMagic) {
			rechargeOn = false;
		}

		// manage health
		if (!healthOn && playerStats.Health <= playerStats.MaxHealth/2) {
			ActivateHealth();
		}
		else if (healthOn && playerStats.Health == playerStats.MaxHealth) {
			healthOn = false;
		}
	}

	void ActivateRecharge () {
		Debug.Log("ActivateRecharge()");

		if (preciseDrops) {
			// Get new position
			Vector3 dropPos = playerStats.gameObject.transform.position;
			Vector2 playerGridPos = grid.PlayerPos;

			if (playerGridPos.y == 0) {
				dropPos.x -= 5f;
			}
			else {
				dropPos.x += 5f;
			}

			dropPos.y = rechargeDrop.transform.position.y;

			if (playerGridPos.x == 0) {
				dropPos.z -= 5f;
			}
			else {
				dropPos.z += 5f;
			}

			// Set new position
			rechargeDrop.transform.position = dropPos;
		}

		// Spawn in
		rechargeDropAnimator.SetBool("on", true);
		rechargeOn = true;
	}

	void ActivateHealth () {
		Debug.Log("ActivateHealth()");

		if (preciseDrops) {
			// Get new position
			Vector3 dropPos = playerStats.gameObject.transform.position;
			Vector2 playerGridPos = grid.PlayerPos;

			if (playerGridPos.y == 2) {
				dropPos.x += 10f;
			}
			else {
				dropPos.x -= 10f;
			}

			dropPos.y = healthDrop.transform.position.y;

			if (playerGridPos.x == 2) {
				dropPos.z += 10f;
			}
			else {
				dropPos.z -= 10f;
			}

			// Set new position
			healthDrop.transform.position = dropPos;
		}

		// Spawn in
		healthDropAnimator.SetBool("on", true);
		healthOn = true;
	}

	void Defeated () {
		healthDrop.SetActive(false);
		rechargeDrop.SetActive(false);
		interactArea.SetActive(false);

		didHideOnDeath = true;
	}

}
