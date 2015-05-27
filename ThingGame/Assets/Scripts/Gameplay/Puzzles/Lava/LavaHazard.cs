using UnityEngine;
using System.Collections;

public class LavaHazard : MonoBehaviour {

	public int damage = 1;
	PlayerStats playerStats;

	// Use this for initialization
	void Start () {
		playerStats = GameObject.FindWithTag(Tags.player).GetComponent<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == Tags.player) {
			if (playerStats != null) {
				playerStats.DealDamage(damage);
			}
		}
	}
}
