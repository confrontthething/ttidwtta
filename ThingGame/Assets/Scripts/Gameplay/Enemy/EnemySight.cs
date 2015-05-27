using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

	public bool isOn = true;

	private SphereCollider detectionRange;
	private float hearingRange;
	public LayerMask detectionMask;
	public float fieldOfViewAngle = 110f;

	private Vector3 personalLastSighting;
	private bool playerInSight;

	private EnemyGroupSight enemyGroupSight;
	private NavMeshAgent navAgent;

	private LastPlayerSighting lastPlayerSighting;

	private GameObject player;
	private PlayerStats playerStats;
	private Vector3 previousSighting;

	void Start () {
		foreach (SphereCollider col in GetComponents<SphereCollider>()) {
			if (col.isTrigger) {
				detectionRange = col;
				hearingRange = detectionRange.radius * 4f;
			}
		}
		
		navAgent = GetComponent<NavMeshAgent>();

		enemyGroupSight = GetComponentInParent<EnemyGroupSight>();

		lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
		player = GameObject.FindGameObjectWithTag(Tags.player);
		playerStats = player.GetComponent<PlayerStats>();

		personalLastSighting = lastPlayerSighting.resetPosition;
		previousSighting = lastPlayerSighting.resetPosition;
	}
	
	void Update () {
		if (!isOn) {
			return;
		}

		if (lastPlayerSighting.position != previousSighting) {
			personalLastSighting = lastPlayerSighting.position;
		}
		previousSighting = lastPlayerSighting.position;

		if (enemyGroupSight != null && enemyGroupSight.PlayerInSight) {
			PlayerSighted();
//			personalLastSighting = enemyGroupSight.LastSighting;
		}

		if (playerStats.Health <= 0f) {
			Debug.Log("Player died. Lose sight");
			PlayerLost();
		}
	}

	void PlayerSighted () {
		playerInSight = true;
		lastPlayerSighting.position = player.transform.position;
	}

	void PlayerLost () {
		playerInSight = false;
		lastPlayerSighting.position = lastPlayerSighting.resetPosition;
	}
	
	void OnTriggerStay (Collider other) {
		if (!isOn) {
			return;
		}

		// Check if player is in sight
		if (other.gameObject == player) {
			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);

			if (angle < fieldOfViewAngle * 0.5f) {
				RaycastHit hit;
				if (Physics.Raycast(transform.position, direction.normalized, out hit, detectionRange.radius, detectionMask)) {
					if (hit.collider.gameObject == player) {
//						Debug.Log("Player sighted (direct)");
						PlayerSighted();
					}
					else if (playerInSight) {
//						Debug.Log("Lost sight while player detected");
						PlayerLost();
					}
				}
			}
		}
		// Check if projectile is in hearing range
		else if (other.gameObject.layer == Layers.projectile && playerStats.Health > 0) {
			NavMeshPath path = new NavMeshPath();
			navAgent.CalculatePath(player.transform.position, path);
			if (path.status == NavMeshPathStatus.PathComplete 
			    && CalculatePathLength(player.transform.position) <= hearingRange)
			{
//				Debug.Log("Player sighted (fire ball)");
				PlayerSighted();
			}
		}
	}

	void OnTriggerExit (Collider other) {
		if (!isOn) {
			return;
		}

		if (other.gameObject == player) {
			playerInSight = false;
		}
	}
	
	float CalculatePathLength (Vector3 targetPosition)
	{
		// create a path and set it based on a target position
		NavMeshPath path = new NavMeshPath();
		navAgent.CalculatePath(targetPosition, path);

		// calculate length of path (enemy_pos, corners, target_pos)
		float pathLength = 0f;
		Vector3 prevPoint = transform.position;
		foreach (Vector3 point in path.corners) {
			pathLength += Vector3.Distance(targetPosition, point);
			prevPoint = point;
		}
		pathLength += Vector3.Distance(prevPoint, targetPosition);
		
		return pathLength;
	}

	public bool PlayerInSight {
		get { return playerInSight; }
	}

	public Vector3 PersonalLastSighting {
		get { return personalLastSighting; }
	}

	public void ResetGroupSight () {
		enemyGroupSight = GetComponentInParent<EnemyGroupSight>();
		Debug.Log("reset group sight: " + enemyGroupSight + " - " + gameObject + "(" + gameObject.name + ")");
	}
}
