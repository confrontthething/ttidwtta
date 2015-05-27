using UnityEngine;
using System.Collections;

public class HeadBob : MonoBehaviour {

	public AnimationCurve bobCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
	public float bobDistance = 0.1f;
	public float cycleLength = 1.0f;
	public float lagTime = 0.2f;

	private float defaultTranslateY = 0.0f;
	private CharacterMotor charMotor;
	private bool charMotorEnabled = true;
	private float timer;
	private float speed = 0.0f;
	private float lagVelocity = 0.0f;

	// Use this for initialization
	void Start() {
		defaultTranslateY = transform.localPosition.y;
		charMotor = GetComponentInParent<CharacterMotor>();
	}

	// Update is called once per frame
	void Update() {
		if (charMotor.enabled == false && charMotorEnabled == true) {
			charMotorEnabled = false;

			Vector3 localPosition = transform.localPosition;
			localPosition.y = defaultTranslateY;
			transform.localPosition = localPosition;

			timer = 0.0f;
		} else if (charMotor.enabled == true) {
			charMotorEnabled = true;

			Vector3 localPosition = transform.localPosition;
			localPosition.y = defaultTranslateY + EvaluateBob();
			transform.localPosition = localPosition;

			timer = Mathf.Repeat(timer + Time.deltaTime / cycleLength, 1.0f);
		}
	}

	float EvaluateBob() {
		float maxSpeed = charMotor.movement.maxForwardSpeed;
		Vector3 xzVelocity = charMotor.movement.velocity;
		xzVelocity.y = 0.0f;
		float xzSpeed = xzVelocity.magnitude;
		speed = Mathf.SmoothDamp(speed, xzSpeed, ref lagVelocity, lagTime);
		float ratio = bobDistance * speed / maxSpeed;

		// Timer 0 = Curve 0.5
		// Timer 0.25 = Curve 1
		// Timer 0.5 = Curve 0.5
		// Timer 0.75 = Curve 0
		// Timer 1 = Curve 0.5
		if (timer < 0.25f) {
			return ratio * (bobCurve.Evaluate(0.5f + 2.0f * timer) - 0.5f);
		} else if (timer < 0.75f) {
			return ratio * (bobCurve.Evaluate(1.5f - 2.0f * timer) - 0.5f);
		} else {
			return ratio * (bobCurve.Evaluate(-1.5f + 2.0f * timer) - 0.5f);
		}
	}

}
