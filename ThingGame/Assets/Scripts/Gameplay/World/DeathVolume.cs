using UnityEngine;
using System.Collections;

public class DeathVolume : MonoBehaviour {

	private PlayerStats playerStats;
	private PlayerRespawn playerRespawn;

	void Start () {
		GameObject player = GameObject.FindGameObjectWithTag(Tags.player);
		playerStats = player.GetComponent<PlayerStats>();
		playerRespawn = player.GetComponent<PlayerRespawn>();
	}

	void OnTriggerEnter(Collider other) {
		if (playerStats && other.gameObject == playerStats.gameObject) {
			playerStats.DealDamage(1);
			if (playerStats.Health > 0) {
				playerRespawn.FadeAndRespawn();
			}
		}
	}
}
