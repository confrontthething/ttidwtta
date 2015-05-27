using UnityEngine;
using System.Collections;

public class MagicProjectile : MonoBehaviour {

	public static readonly int PROJECTILE_DAMAGE = 2;

	public AnimationClip impactAnimation;

	public bool randomizeAudio = false;
	public AudioSource source;
	public AudioClip[] clips;

	Animator anim;

	float lifetime = 5f;
	float speed = 1500f;

	void Start () {
		anim = GetComponent<Animator>();

		if (randomizeAudio) {
			int index = Random.Range(0, clips.Length);
			source.clip = clips[index];
			source.Play();
		}

		rigidbody.AddForce(transform.TransformDirection(Vector3.forward * speed));
		Invoke("Dissipate", lifetime);
	}

	void OnCollisionEnter (Collision other) {
		if (other.gameObject.tag == Tags.enemy) {
			EnemyStats enemy = other.transform.parent.gameObject.GetComponent<EnemyStats>();
			if (enemy != null) {
				enemy.DealDamage(PROJECTILE_DAMAGE);
			}
		} else if (other.gameObject.tag == Tags.burnableObject) {
			BurnableObject burn = other.gameObject.GetComponent<BurnableObject>();
			if (burn != null) {
				burn.Burn();
			}
		} else if (other.gameObject.tag == Tags.bossTarget) {
			BossTarget target = other.gameObject.GetComponent<BossTarget>();
			if (target != null) {
				target.DealDamage();
			}
		} else if (other.gameObject.tag == Tags.castleGuard) {
			CastleGuardStats stats = other.gameObject.GetComponentInParent<CastleGuardStats>();
			if (stats != null && stats.enabled) {
				stats.Hit();
			}
		} else if (other.gameObject.tag == Tags.throneRoomThing) {
			ThroneRoomThing thing = other.gameObject.GetComponent<ThroneRoomThing>();
			if (thing != null) {
				thing.Hit();
			}
		}
		Impact();
	}

	void Impact () {
		rigidbody.velocity = Vector3.zero;
		Destroy(gameObject.collider);
		anim.SetTrigger("collided");
		Destroy(gameObject, impactAnimation.length);
	}

	void Dissipate () {
		// TODO: dissipate animation
		Destroy(gameObject);
	}
}
