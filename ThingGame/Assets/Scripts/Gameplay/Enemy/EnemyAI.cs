using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public bool roam = true;
	public float roamingSpeed = 2f;
	public float roamingRadius = 10f;
	private float[] roamingRange;
	private Vector3 roamingCenter;
	private Vector3 roamingTarget;
	public float roamingPauseTime = 8f;
	private float roamingPauseTimer = 0f;

	public float chaseSpeed = 5f;

	private float attackRange = 7f;

	private EnemySight enemySight;
	private NavMeshAgent navAgent;
	private Transform player;
	private PlayerStats playerStats;
//	private LastPlayerSighting lastPlayerSighting;

	private Animator animator;

	void Start () {
		enemySight = GetComponent<EnemySight>();
		navAgent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag(Tags.player).transform;
		playerStats = player.GetComponent<PlayerStats>();
//		lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
		animator = GetComponentInChildren<Animator>();

		roamingRange = new float[2]{roamingRadius * -1, roamingRadius};
		ResetRoam();
	}
	
	void Update () {
		if (playerStats.InModalSequence) {
			navAgent.enabled = false; // Enemies should freeze while player is in cutscene/puzzle.
			return;
		} else {
			navAgent.enabled = true;
		}

		navAgent.Resume();

		if (playerStats.Health > 0f) {
			if (enemySight.PlayerInSight) {
				if ((PlayerInAttackRange() && IsInFrontOfPlayer()) || animator.GetBool("attack")) {
					Attack();
				} else {
					Chase ();
				}
			}
			else {
				if (roam) {
					if (navAgent.speed == chaseSpeed) {
						ResetRoam();
					}
					Roam();
				}
			}
		} else {
			if (roam) {
				ResetRoam();
			}
		}
	}

	void Attack () {
		if (animator != null && !animator.GetBool("attack")) {
			animator.SetTrigger("attack");
		}

		navAgent.destination = transform.position;
	}
	
	bool PlayerInAttackRange () {
		return Vector3.Distance(transform.position, player.position) <= attackRange;
	}

	bool IsInFrontOfPlayer () {
		Vector3 relativePoint = transform.InverseTransformPoint(player.position);
		return Mathf.Abs(relativePoint.x) <= 0.1f;
	}

	void Chase () {
		Vector3 sightingDeltaPos = enemySight.PersonalLastSighting - transform.position;
		if (sightingDeltaPos.sqrMagnitude > 4f) {
			navAgent.destination = enemySight.PersonalLastSighting;
		}

		navAgent.speed = chaseSpeed;
	}

	void Roam () {
		if (NavReachedDestination() && roamingPauseTimer == 0f) {
			Vector3 randDir = new Vector3(Random.Range(roamingRange[0], roamingRange[1]), 
			                              0, 
			                              Random.Range(roamingRange[0], roamingRange[1]));
			roamingTarget = roamingCenter + randDir;
			navAgent.destination = roamingTarget;
			roamingPauseTimer = Random.Range(roamingPauseTime * 0.5f, roamingPauseTime);
		}
		roamingPauseTimer = Mathf.Max(roamingPauseTimer - Time.deltaTime, 0f);
	}

	bool NavReachedDestination () {
		if (!navAgent.pathPending)
		{
			if (navAgent.remainingDistance <= navAgent.stoppingDistance)
			{
				if (!navAgent.hasPath || navAgent.velocity.sqrMagnitude == 0f)
				{
					return true;
				}
			}
		}
		return false;
	}
	
	void ResetRoam () {
		roamingCenter = transform.position;
		roamingTarget = roamingCenter;
		navAgent.speed = roamingSpeed;
	}

	public void Stop () {
		navAgent.Stop();
	}

	public void Resume () {
		navAgent.Resume();
	}

}
