using UnityEngine;
using System.Collections;

public class RespawnPoint : MonoBehaviour {
	
	private PlayerRespawn playerRespawn;

	void Start () {
		playerRespawn = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerRespawn>();
	}

	void OnTriggerEnter(Collider other) {
		if (playerRespawn && other.gameObject == playerRespawn.gameObject) {
			playerRespawn.RegisterRespawnPoint(this);
    	}
	}

}
