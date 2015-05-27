using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour {

	public RespawnPoint deathRespawnPoint;

	private UIController userInterface;
	private PlayerStats playerStats;
	private RespawnPoint lastRespawnPoint = null;

	void Start () {
		userInterface = GameObject.FindGameObjectWithTag(Tags.ui).GetComponent<UIController>();
		playerStats = GetComponent<PlayerStats>();
	}

	public void RegisterRespawnPoint (RespawnPoint rp) {
		lastRespawnPoint = rp;
	}

	public void FadeAndRespawn () {
		userInterface.FadeAndRespawn();
	}

	public void ImmediatelyRespawn () {
		if (lastRespawnPoint == null) {
			const string err = "Cannot respawn because never reached a respawn point";
			throw new System.InvalidOperationException(err);
		}

		transform.position = lastRespawnPoint.transform.position;
	}

	public void GameOver () {
		GlobalVariables.telemetry.IncrementDeaths();
		userInterface.FadeAndRestartLevel();
	}

	public void RestartLevel () {
		if (deathRespawnPoint == null) {
			Application.LoadLevel(Application.loadedLevel);
		} else {
			playerStats.RechargeHealth();
			playerStats.Recharge();
			lastRespawnPoint = deathRespawnPoint;
			ImmediatelyRespawn();
		}
	}
}
