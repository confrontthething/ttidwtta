using UnityEngine;
using System.Collections;

public class Sprint : MonoBehaviour {

	public bool isEnabled = true;

	public Animator cameraAnimator;

	PlayerStats playerStats;
	CharacterMotor charMotor;
	HeadBob headBob;

	bool isSprinting = false;

	public float movementSpeedCoef = 1.5f;
	public float headBobDistanceCoef = 1.2f;
	public float headBobCycleLengthCoef = 0.85f;

	float defaultSpeed;
	public float DefaultSpeed {
		get {
			return defaultSpeed;
		}
	}
	float defaultBobDistance;
	float defaultCycleLength;

	float sprintSpeed;
	public float SprintSpeed {
		get {
			return sprintSpeed;
		}
	}
	float sprintBobDistance;
	float sprintCycleLength;

	void Start () {
		playerStats = GetComponent<PlayerStats>();
		charMotor = GetComponent<CharacterMotor>();
		headBob = GetComponentInChildren<HeadBob>();

		defaultSpeed = charMotor.movement.maxForwardSpeed;
		defaultBobDistance = headBob.bobDistance;
		defaultCycleLength = headBob.cycleLength;

		sprintSpeed = charMotor.movement.maxForwardSpeed * movementSpeedCoef;
		sprintBobDistance = headBob.bobDistance * headBobDistanceCoef;
		sprintCycleLength = headBob.cycleLength * headBobCycleLengthCoef;
	}
	
	void Update () {
		if (!isEnabled) {
			return;
		}

		if (!isSprinting) {
			// turn on if sprint requirements are met
			if (CanSprint()) {
				SprintOn();
			}
		} else {
			// turn off if sprint requirements aren't met
			if (!CanSprint()) {
				SprintOff();
			}
		}
	}

	bool CanSprint () {
		Vector3 xzVelocity = charMotor.movement.velocity;
		xzVelocity.y = 0.0f;
		float xzSpeed = xzVelocity.magnitude;

		return 	!playerStats.InModalSequence && 
				xzSpeed > charMotor.movement.maxForwardSpeed * 1f/3f &&
				Input.GetKey(KeyCode.LeftShift) && 
				Input.GetKey(KeyCode.W);
	}

	void SprintOn () {
		charMotor.movement.maxForwardSpeed = sprintSpeed;
		headBob.bobDistance = sprintBobDistance;
		headBob.cycleLength = sprintCycleLength;
		cameraAnimator.SetBool("sprinting", true);

		isSprinting = true;
	}

	void SprintOff () {
		charMotor.movement.maxForwardSpeed = defaultSpeed;
		headBob.bobDistance = defaultBobDistance;
		headBob.cycleLength = defaultCycleLength;
		cameraAnimator.SetBool("sprinting", false);

		isSprinting = false;
	}

	public bool IsSprinting {
		get {
			return isSprinting;
		}
	}
}
