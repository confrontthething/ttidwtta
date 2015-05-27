using UnityEngine;
using System.Collections;

public class BossAI : MonoBehaviour {

	public bool debugMode = false;

	public bool isOn = true;

	private BossStats stats;
	public BossGrid grid;

	private PlayerStats player;

	private Animator anim;
	private bool isAttacking;
	private bool didTriggerAttack = false;

	public BossEruption eruption;
	public ParticleSystem rightSmoke;
	public ParticleSystem leftSmoke;

	public AudioSource fireBreathSound;
	public AudioSource rightSlamSound;
	public AudioSource leftSlamSound;

	// animation names
	private const string anim_idle = "Boss-Idle";
	private const string anim_attackRight = "Boss-Attack-Right-Slam";
	private const string anim_attackMiddle = "Boss-Attack-Middle-Fire";
	private const string anim_attackLeft = "Boss-Attack-Left-Slam";
	private const string anim_attackBack = "Boss-Attack-Back-Eruption";

	// attacks
	private bool[] rightAttacks  = new bool[]{true , false, true , false, true , true , false, true };
	private bool[] middleAttacks = new bool[]{false, true , false, false, false, true , false, true };
	private bool[] leftAttacks   = new bool[]{true , false, false, true , false, true , false, true };
	private int rightAttackIndex = 0;
	private int middleAttackIndex = 0;
	private int leftAttackIndex = 0;

	private float attackDelay = 3f;

	private bool damageOn = false;

	void Start () {
		stats = GetComponent<BossStats>();
		player = GameObject.FindWithTag(Tags.player).GetComponent<PlayerStats>();
		anim = GetComponent<Animator>();

		rightAttackIndex = (int)Random.Range(0, rightAttacks.Length);
		middleAttackIndex = (int)Random.Range(0, middleAttacks.Length);
		leftAttackIndex = (int)Random.Range(0, leftAttacks.Length);
	}
	
	void Update () {
#if UNITY_EDITOR
		if (debugMode) {
			return;
		}
#endif
		if (!isOn || !stats.IsAlive) {
			return;
		}

		AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
		isAttacking = !info.IsName(anim_idle);

		// execute attack
		if (!isAttacking && !didTriggerAttack) {
			Invoke("ExecuteAttack", attackDelay);
			didTriggerAttack = true;
		}
		// calculate damage
		if (damageOn) {
			CalculateDamage();
		}
	}

	void ExecuteAttack () {
		if (!isOn || !stats.IsAlive || isAttacking) {
			didTriggerAttack = false;
			return;
		}

		Vector2 playerPos = grid.PlayerPos;
		switch ((int) playerPos.x) {
		case 0 : 
			if (rightAttacks[rightAttackIndex]) {
				anim.SetTrigger("attackRight");
			} else {
				ExecuteEruptionAttack();
			}
			rightAttackIndex = (rightAttackIndex + 1) % rightAttacks.Length;
			break;
		case 1 : 
			if (middleAttacks[middleAttackIndex]) {
				anim.SetTrigger("attackMiddle");
			} else {
				ExecuteEruptionAttack();
			}
			middleAttackIndex = (middleAttackIndex + 1) % middleAttacks.Length;
			break;
		case 2 : 
			if (leftAttacks[leftAttackIndex]) {
				anim.SetTrigger("attackLeft"); 
			} else {
				ExecuteEruptionAttack();
			}
			leftAttackIndex = (leftAttackIndex + 1) % leftAttacks.Length;
			break;
		}

		didTriggerAttack = false;
	}

	void ExecuteEruptionAttack () {
		anim.SetTrigger("attackBack");
		eruption.ActivateEruption();
	}

	public void RightSlamImpact () {
		rightSmoke.Play();
		rightSlamSound.Play();
	}
	
	public void LeftSlamImpact () {
		leftSmoke.Play();
		leftSlamSound.Play();
	}

	void OverrideAttackDelay () {
		CancelInvoke("ExecuteAttack");
		ExecuteAttack();
	}

	void CalculateDamage () {
		AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
		Vector2 playerPos = grid.PlayerPos;

		switch ((int) playerPos.x) {
		case 0 : if (info.IsName(anim_attackRight)) player.DealDamage(2); break;
		case 1 : if (info.IsName(anim_attackMiddle)) player.DealDamage(1); break;
		case 2 : if (info.IsName(anim_attackLeft)) player.DealDamage(2); break;
		}
		if (playerPos.y == 2f && info.IsName(anim_attackBack)) {
			player.DealDamage(1);
		}
	}
	
	public void DamageOn () {
		damageOn = true;
	}
	public void DamageOff () {
		damageOn = false;
	}

	void PlayFireBreathSound () {
		fireBreathSound.volume = 1f;
		fireBreathSound.Play();
	}

	IEnumerator FadeOutBGMusic () {
		while (fireBreathSound.volume > 0.0f) {
			fireBreathSound.volume = Mathf.Max(fireBreathSound.volume - 0.05f, 0.0f);
			yield return new WaitForSeconds(0.1f);
		}
	}
	
}
