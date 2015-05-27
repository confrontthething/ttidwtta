using UnityEngine;
using System.Collections;

public class BossTarget : MonoBehaviour {

	public Material[] materials;
	private int materialIndex = 0;

	public AudioSource hitSound;
	public AudioSource destroyedSound;

	private MeshRenderer meshRend;
	private Animator anim;
	private BossStats stats;

	private int health = 3;
	private int maxHealth;

	private bool isOn = true;

	void Start () {
		meshRend = GetComponent<MeshRenderer>();
		anim = GetComponent<Animator>();
		GameObject boss = GameObject.FindWithTag(Tags.boss);
		stats = boss.GetComponent<BossStats>();
		maxHealth = health;
	}
	
	public void DealDamage () {
		if (!isOn) {
			return;
		}

		health--;
		if (health <= 0) {
			Destroyed();
		} else {
			anim.SetTrigger("damaged");
			meshRend.material = materials[materialIndex++];
			hitSound.Play();
		}
	}

	void Destroyed () {
		isOn = false;
		anim.SetBool("destroyed", true);
		stats.TargetDestroyed();
		destroyedSound.Play();
	}

	public int Health {
		get {
			return health;
		}
	}

	public int MaxHealth {
		get {
			return maxHealth;
		}
	}

	public float HealthPercentage {
		get {
			return (float)health / (float)maxHealth;
		}
	}
}
