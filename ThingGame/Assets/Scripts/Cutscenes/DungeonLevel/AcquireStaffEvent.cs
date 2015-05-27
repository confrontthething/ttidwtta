using UnityEngine;
using System.Collections;

public class AcquireStaffEvent : MonoBehaviour {

	public bool disableStaff = true;

	public GameObject playerStaff;
	public Light playerLight;
	public GameObject pedestalStaff;
	public SecretDoor secretDoor;

	public Animator cameraAnim;
	public AnimationClip cameraEnabledClip;
	public Transform playerCutscenePosition;

	public GameObject health;
	public GameObject magicBar;
	public GameObject magicText;

	public ParticleSystem pedestalParticles;
	public Light spotlight;
	public CapsuleCollider pedestalCollider;

	public DPek.Raconteur.RenPy.RenPyViewBasic m_view; 

	GameObject player;
	PlayerStats playerStats;
	float playerLightIntensity;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag(Tags.player);
		playerStats = player.GetComponent<PlayerStats>();
		playerLightIntensity = playerLight.intensity;

		m_view = GetComponent<DPek.Raconteur.RenPy.RenPyViewBasic> ();

		if (disableStaff) {
			DisableStaff();
		}
	}

	void DisableStaff () {
		playerStaff.SetActive(false);
		health.SetActive(false);
		magicBar.SetActive(false);
		magicText.SetActive(false);
		playerLight.intensity = 0.1f;

		playerStats.combatEnabled = false;
		secretDoor.isActivated = false;
	}

	public void EnableStaff () {
		pedestalStaff.SetActive(false);
		playerStaff.SetActive(true);
		health.SetActive(true);
		magicBar.SetActive(true);
		magicText.SetActive(true);
		playerLight.intensity = playerLightIntensity;

		pedestalCollider.enabled = false;

		playerStats.combatEnabled = true;
		pedestalParticles.Stop();

		BeginCutscene();
	}

	void BeginCutscene () {
		cameraAnim.SetTrigger("enabled");
		Invoke("ActivateDoor", 2f);
		Invoke("EndCutscene", cameraEnabledClip.length);
	}

	void ActivateDoor () {
		secretDoor.isActivated = true;
	}

	void EndCutscene () {
		m_view.m_autoStart = true;
		m_view.Start ();
	}
}
