using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	private UIController userInterface;
	private PlayerRespawn playerRespawn;

	public AudioSource lowHealthSFX;
	private bool didPlayLowHealthSFX = false;

	private int health = 5;
	private int halfHealth;
	private int maxHealth = 5;

	private int magic;
	private int maxMagic = 10;

	private float invincibilityTime = 1f;

	public DPek.Raconteur.RenPy.RenPyViewBasic m_view;

	private MonoBehaviour characterMotor;
	private MonoBehaviour sideToSideLook;
	private MonoBehaviour upDownLook;

	public bool combatEnabled = true;

	private bool inModalSequence = false; /* In modal sequences like puzzles or cutscenes. */
	public bool modalByDefault = false;

	public bool isInvincible = false;
	public bool permanentInvincibility = false;

	void Start () {
		m_view = GetComponent<DPek.Raconteur.RenPy.RenPyViewBasic> ();
		userInterface = GameObject.FindGameObjectWithTag(Tags.ui).GetComponent<UIController>();
		playerRespawn = GetComponent<PlayerRespawn>();

		characterMotor = GetComponent<CharacterMotor>();
		sideToSideLook = GetComponent<SmoothMouseLook>();
		upDownLook = GetComponentInChildren<Camera>().gameObject.GetComponent<SmoothMouseLook>();

		health = maxHealth;
		halfHealth = maxHealth / 2;
		magic = maxMagic;

		if (modalByDefault) {
			EnterModal();
		}
	}

	public bool DecreaseMagicBy (int value) {
		if (magic < value) {
			return false;
		}
		magic -= value;
		return true;
	}

	public float Magic {
		get { return magic; }
	}

	public float MaxMagic {
		get { return maxMagic; }
	}

	public float MagicPercentage {
		get {
			return (float)magic / (float)maxMagic;
		}
	}

	public void Recharge () {
		// TODO: recharge animation
		magic = maxMagic;
		// TODO: update magic bar
	}

	public void DealDamage (int damage) {
		if (inModalSequence) {
			Debug.Log("PlayerStats - DealDamage() - cutscene/puzzzle!");
			return;
		}

		if (permanentInvincibility) {
			Debug.Log("PlayerStats - DealDamage() - invincible!");
			return;
		}

		if (isInvincible) {
			Debug.Log("PlayerStats - DealDamage() - temporarily invincible!");
			return;
		}

		health -= damage;
		userInterface.RedFlash();
		Debug.Log("PlayerStats - DealDamage() - damage dealt");
		InvincibilityOn();

		if (health <= halfHealth && !didPlayLowHealthSFX) {
			lowHealthSFX.Play();
			didPlayLowHealthSFX = true;
		}

		if (health <= 0) {
			Debug.Log("PlayerStats - DealDamage() - game over!");
			lowHealthSFX.Stop();
			didPlayLowHealthSFX = false;
			playerRespawn.GameOver();
		}
	}

	void InvincibilityOn () {
		isInvincible = true;
		Invoke ("InvincibilityOff", invincibilityTime);
	}

	void InvincibilityOff () {
		isInvincible = false;
	}

	public void RechargeHealth (int bonus = -1) {
		if (bonus < 0) {
			health = maxHealth;
		} else {
			health = Mathf.Min(maxHealth, health + bonus);
		}

		if (didPlayLowHealthSFX) {
			lowHealthSFX.Stop();
			didPlayLowHealthSFX = false;
		}
	}

	public int Health {
		get { return health; }
	}

	public int MaxHealth {
		get { return maxHealth; }
	}

	public bool InModalSequence {
		get { return inModalSequence; }
	}

	public void EnterModal() {
		inModalSequence = true;

		if (characterMotor != null) {
			characterMotor.enabled = false;
		}

		if (sideToSideLook != null) {
			sideToSideLook.enabled = false;
		}

		if (upDownLook != null) {
			upDownLook.enabled = false;
		}
	}

	public void ExitModal() {
		inModalSequence = false;

		if (characterMotor != null) {
			characterMotor.enabled = true;
		}

		if (sideToSideLook != null) {
			sideToSideLook.enabled = true;
		}

		if (upDownLook != null) {
			upDownLook.enabled = true;
		}
	}

}
