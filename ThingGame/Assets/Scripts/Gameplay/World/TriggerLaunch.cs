using UnityEngine;
using System.Collections;

public class TriggerLaunch : MonoBehaviour {

	public Transform target;
	public float force;
	Vector3 direction;

	public bool isOn = false;

	ImpactReceiver player;

	void Start () {
		player = GameObject.FindWithTag(Tags.player).GetComponent<ImpactReceiver>();
		Debug.Log(target);
	}
	
	void OnTriggerEnter (Collider other) {
		Debug.Log("did detect");
		if (other.tag == Tags.player) {
			Debug.Log("is player");
			Debug.Log("target != null && isOn : " + (target != null) + " && " + isOn);
			if (target != null && isOn) {
				Debug.Log("launch!");
				Vector3 heading = target.position - transform.position;
				float distance = heading.magnitude;
				direction = heading / distance;
				AddKnockback();
			}
		}
	}

	void AddKnockback () {
		if (player != null) {
			player.AddImpact(direction, force);
		}
	}
}
