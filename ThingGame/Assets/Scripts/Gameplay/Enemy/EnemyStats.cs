using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {

	public static readonly int ENEMY_DAMAGE = 1;

	private EnemyGroupSight groupSight;
	
	private Animator anim;
	public AnimationClip deathAnimation;

	AudioSource audioSrc;
	public AudioClip deathAudio;

	private EnemyAudioHintController hints;

	private int health = 2;

	void Start () {
		groupSight = GetComponentInParent<EnemyGroupSight>();
		anim = GetComponentInChildren<Animator>();
		audioSrc = GetComponentInChildren<AudioSource>();
		hints = GameObject.Find("GameController").GetComponent<EnemyAudioHintController>();
	}

	public void DealDamage (int damage) {
		health -= damage;
		if (health <= 0) {
			Die();
		}
	}

	void Die () {
		groupSight.OverridePlayerInSight();
		++hints.numEnemiesDefeated;
		anim.SetBool("dead", true);
		audioSrc.clip = deathAudio;
		audioSrc.Play();
		GetComponentInChildren<CapsuleCollider>().enabled = false;
		Destroy(gameObject, deathAnimation.length);
	}
}
