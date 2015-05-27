using UnityEngine;
using System.Collections;

public class BridgeEncounterTrigger : MonoBehaviour {

	private BridgeEncounter bridgeEncounter;

	void Start () {
		bridgeEncounter = GetComponentInParent<BridgeEncounter>();
	}

	void OnTriggerEnter (Collider other) {
		if (!other.isTrigger && other.gameObject.tag == Tags.player) {
			Debug.Log("player hit bridge encounter trigger");
			bridgeEncounter.InitEncounter();
			gameObject.SetActive(false);
		}
	}
}
