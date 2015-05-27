using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

	private PlayerStats playerStats;

//	public LayerMask groundMask;
//	private bool onGround = false;

	void Start () {
		playerStats = gameObject.transform.parent.gameObject.GetComponent<PlayerStats>();
	}

//	public bool OnGround {
//		get {
//			return onGround;
//		}
//	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.tag == Tags.enemy) {
			playerStats.DealDamage(EnemyStats.ENEMY_DAMAGE);
		}
//		if (groundMask == (groundMask | (1 << collision.gameObject.layer))) {
//			Debug.Log("OnCollisionEnter(" + collision.gameObject.name + ")");
//			onGround = true;
//		}
	}

	void OnCollisionStay (Collision collision) {
		if (collision.gameObject.tag == Tags.enemy) {
			playerStats.DealDamage(EnemyStats.ENEMY_DAMAGE);
		}
	}

//	void OnCollisionExit (Collision collision) {
//		if (groundMask == (groundMask | (1 << collision.gameObject.layer))) {
//			Debug.Log("OnCollisionExit(" + collision.gameObject.name + ")");
//			onGround = false;
//		}
//	}
}
