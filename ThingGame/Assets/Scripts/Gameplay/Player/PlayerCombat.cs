using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour {

	public GameObject projectile;
	public Transform projectileSpawnPoint;

	public LayerMask projectileMask;

	public Animator staffAnimator;

	private GameObject playerCamera;

	private PlayerStats playerStats;
	private CharacterMotor charMotor;
		
	private float projectileDelay = 0.3f;
	private bool canShoot = true;

	void Start () {
		playerCamera = GameObject.FindGameObjectWithTag(Tags.mainCamera);
		playerStats = GetComponent<PlayerStats>();
		charMotor = GetComponent<CharacterMotor>();
	}

	void Update () {
		if (playerStats.Health <= 0f) {
			return;
		} else if (playerStats.InModalSequence) {
			return;
		} else if (!playerStats.combatEnabled) {
			return;
		}

		// move player constantly so triggers will detect it
		transform.position += Vector3.zero;

		// Do ranged attack
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			RangedAttack();
		}

		// update speed
		if (staffAnimator != null) {
			staffAnimator.SetFloat("Speed", charMotor.movement.velocity.magnitude);
		}
	}

	void RangedAttack () {
		RaycastHit hit;
		Vector3 fwd = playerCamera.transform.TransformDirection(Vector3.forward);
		Vector3 target;
		if (Physics.Raycast(playerCamera.transform.position, fwd, out hit, Mathf.Infinity, projectileMask)) {
			// Aim at closest target under reticle.
			target = hit.point;
		} else {
			// Nothing under the reticle, so aim at a very-far-away point.
			target = playerCamera.transform.position + fwd * 10000.0f;
		}

		if (playerStats.Magic != 0 && canShoot) {
			if (staffAnimator != null) {
				staffAnimator.SetTrigger("Ranged");
			}
			
			SpawnProjectile(target);
			playerStats.DecreaseMagicBy(1);
			DisableRangedAttack();
		}
	}

	void DisableRangedAttack () {
		canShoot = false;
		Invoke("EnableRangedAttack", projectileDelay);
	}
	void EnableRangedAttack () {
		canShoot = true;
	}

	void SpawnProjectile (Vector3 towards) {
		Quaternion rotation = Quaternion.LookRotation((towards - projectileSpawnPoint.position).normalized);
		Instantiate(projectile, projectileSpawnPoint.position, rotation);
	}

}
