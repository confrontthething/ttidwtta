using UnityEngine;
using System.Collections;

public class EnemyAnimatorController : MonoBehaviour {

	public bool variedStartTime = true;

	public AudioSource enemyAttackSource;
	public float attackPitchVariance = 0.2f;
	float[] attackPitchRange;

	private Animator anim;
	private EnemyAI enemyAI;

	void Start () {
		anim = GetComponent<Animator>();
		enemyAI = GetComponentInParent<EnemyAI>();

		if (variedStartTime && anim != null) {
			anim.SetTrigger("freeze");
			Invoke ("StartAnimator", Random.Range(0, 2f));
		}

		attackPitchRange = new float[]{enemyAttackSource.pitch - attackPitchVariance,
									   enemyAttackSource.pitch + attackPitchVariance};
	}

	void StartAnimator () {
		anim.SetTrigger("start");
	}
	
	void Stop () {
		if (enemyAI != null) {
			enemyAI.Stop();
		}
	}

	void Resume () {
		if (enemyAI != null) {
			enemyAI.Resume();
		}
	}

	void PlayAttackSound () {
		enemyAttackSource.pitch = Random.Range(attackPitchRange[0], attackPitchRange[1]);
		enemyAttackSource.Play();
	}

}
