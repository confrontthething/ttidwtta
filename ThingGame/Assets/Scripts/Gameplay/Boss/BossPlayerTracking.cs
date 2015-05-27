using UnityEngine;
using System.Collections;

public class BossPlayerTracking : MonoBehaviour {

	GameObject player;

	void Start () {
		player = GameObject.FindWithTag(Tags.player);
		Debug.Log(gameObject.transform.up);
	}
	
	void Update () {
//		Debug.Log(gameObject.transform.position + ", " + player.transform.position);
		gameObject.transform.LookAt(player.transform, gameObject.transform.up);
	}
}
