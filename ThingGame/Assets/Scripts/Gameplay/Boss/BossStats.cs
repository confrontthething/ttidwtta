using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;

public class BossStats : MonoBehaviour {

	public int numTargets = 4;
	private int maxTargets;

	public GameObject bossBarriers;
	public GameObject afterBossBarriers;
	public GameObject afterBossNarration;
	public BossBattleEndCutscene endCutscene;

	public Animator musicAnim;

	private Animator anim;
	private bool isAlive = true;
	public Animator bossUIAnim;

	private AudioSource deathSound;

	private PlayerStats playerStats;

	public BossEruption eruption;
	public BossGrid grid;

	void Start () {
		anim = GetComponent<Animator>();
		maxTargets = numTargets;
		deathSound = GetComponent<AudioSource>();
		playerStats = GameObject.FindWithTag(Tags.player).GetComponent<PlayerStats>();
	}

#if UNITY_EDITOR
	 void Update () {
	 	if (isAlive && Input.GetKeyDown(KeyCode.P)) {
	 		Defeated();
	 	}
	 }
#endif

	public void TargetDestroyed () {
		numTargets--;
		SpeedUp();
		if (numTargets <= 0) {
			Defeated();
//		} else {
//			anim.SetTrigger("damaged");
//			eruption.CancelAnimation();
		}
	}

	void SpeedUp () {
		anim.speed += 0.2f;
		eruption.SpeedUp(0.2f);
	}

	void Defeated () {
		anim.speed = 1f;
		eruption.Dead();

		isAlive = false;

		bossBarriers.SetActive(false);
		afterBossBarriers.SetActive(true);
		afterBossNarration.SetActive(true);
		GlobalVariables.bossDefeated = true;
		anim.SetBool("dead", true);

		deathSound.Play();

		grid.DisableLines();

		playerStats.RechargeHealth();
		playerStats.Recharge();

		musicAnim.SetBool("off", true);
	}

	void HideBossHealthBar () {
		bossUIAnim.SetBool("visible", false);
	}

	void FinishedDyingCallback() {
		endCutscene.FrinchfryDied();
	}

	public bool IsAlive {
		get {
			return isAlive;
		}
	}

	public int MaxTargets {
		get {
			return maxTargets;
		}
	}

	public float HealthPercentage {
		get {
			return (float)numTargets / (float)maxTargets;
		}
	}

}
