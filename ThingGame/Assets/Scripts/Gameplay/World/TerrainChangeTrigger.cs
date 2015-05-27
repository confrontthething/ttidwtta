using UnityEngine;
using System.Collections;

public class TerrainChangeTrigger : MonoBehaviour {

	public MovementSoundController.TerrainType terrain;

	MovementSoundController soundController;

	void Start () {
		soundController = GameObject.FindWithTag(Tags.player).GetComponentInChildren<MovementSoundController>();
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == Tags.player) {
			if (soundController != null) {
				soundController.Terrain = terrain;
			}
		}
	}
}
