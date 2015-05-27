using UnityEngine;
using System.Collections;

public class TriggerHazard : MonoBehaviour {

	public int damage = 1;

	private PlayerStats stats;
	
	void Start () {
		stats = GameObject.FindWithTag(Tags.player).GetComponent<PlayerStats>();
	}

	void OnTriggerStay (Collider other) {
		if (other.tag == Tags.player) {
			stats.DealDamage(damage);
		}
	}
}
