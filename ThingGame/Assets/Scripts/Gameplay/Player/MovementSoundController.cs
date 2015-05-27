using UnityEngine;
using System.Collections;

public class MovementSoundController : MonoBehaviour {
	
	public AudioSource walkingSource;
	public AudioSource runningSource;
	public AudioSource jumpingSource;

	public PlayerCollision playerCollision;
	public PlayerStats playerStats;
	public CharacterMotor charMotor;
	public Sprint sprint;

	public enum TerrainType {
		dirt, stone, wood, none,
	};

	public TerrainType startTerrain;

	private TerrainType terrain;
	public TerrainType Terrain {
		get {
			return terrain;
		}
		set {
			terrain = value;
			switch (terrain) {
			case TerrainType.dirt :
				walkingSource.clip = dirtWalkingClip;
				runningSource.clip = dirtRunningClip;
				jumpingSource.clip = dirtJumpingClip;
				break;
			case TerrainType.stone :
				walkingSource.clip = stoneWalkingClip;
				runningSource.clip = stoneRunningClip;
				jumpingSource.clip = stoneJumpingClip;
				break;
			case TerrainType.wood :
				walkingSource.clip = woodWalkingClip;
				runningSource.clip = woodRunningClip;
				jumpingSource.clip = woodJumpingClip;
				break;
			default :
				walkingSource.clip = null;
				runningSource.clip = null;
				jumpingSource.clip = null;
				break;
			}
		}
	}

	public AudioClip dirtWalkingClip;
	public AudioClip dirtRunningClip;
	public AudioClip dirtJumpingClip;

	public AudioClip stoneWalkingClip;
	public AudioClip stoneRunningClip;
	public AudioClip stoneJumpingClip;

	public AudioClip woodWalkingClip;
	public AudioClip woodRunningClip;
	public AudioClip woodJumpingClip;

	bool didJump = false;
	
	void Start () {
		Terrain = startTerrain;
	}
	
	void Update () {
		if (playerStats.InModalSequence) {
			walkingSource.Stop();
			runningSource.Stop();
			jumpingSource.Stop();
			return;
		}

		if (charMotor.IsGrounded()) {
			if (charMotor.movement.velocity.magnitude < 0.05f) {
				FootstepsOff();
			}
			else if (charMotor.movement.velocity.magnitude > sprint.DefaultSpeed + 0.05f) {
				RunningOn();
			}
			else {
				WalkingOn();
			}
			didJump = false;
		} else if (charMotor.jumping.jumping) {
			if (!didJump) {
				PlayJump();
			}
		}
	}

	void AllOff () {
		if (walkingSource.isPlaying) {
			walkingSource.Stop();
		}
		if (runningSource.isPlaying) {
			runningSource.Stop();
		}
		if (jumpingSource.isPlaying) {
			jumpingSource.Stop();
		}
	}

	void FootstepsOff () {
		if (walkingSource.isPlaying) {
			walkingSource.Stop();
		}
		if (runningSource.isPlaying) {
			runningSource.Stop();
		}
	}

	void WalkingOn () {
		if (!walkingSource.isPlaying) {
			walkingSource.Play();
		}
		if (runningSource.isPlaying) {
			runningSource.Stop();
		}
	}

	void RunningOn () {
		if (walkingSource.isPlaying) {
			walkingSource.Stop();
		}
		if (!runningSource.isPlaying) {
			runningSource.Play();
		}
	}

	void PlayJump () {
		if (walkingSource.isPlaying) {
			walkingSource.Stop();
		}
		if (runningSource.isPlaying) {
			runningSource.Stop();
		}
		if (!jumpingSource.isPlaying) {
			jumpingSource.Play();
		}
		didJump = true;
	}

}
