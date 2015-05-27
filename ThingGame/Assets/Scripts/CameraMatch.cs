using UnityEngine;
using System.Collections;

public class CameraMatch : MonoBehaviour {

	Space matchMode = Space.World;

	Vector3 defaultLocalPosition;
	Quaternion defaultLocalRotation;
	float defaultFieldOfView;
	int defaultCullingMask;

	Vector3 oldPosition;
	Quaternion oldRotation;
	float oldFieldOfView;

	Vector3 newPosition;
	Quaternion newRotation;
	float newFieldOfView;

	float interpolationTime = 2.0f;
	float timer = float.MaxValue;
	bool moving = false;

	Animator animator;

	private const int playerWeaponMask = (1 << Layers.player) | (1 << Layers.fpsWeapons);

	// Use this for initialization
	void Start() {
		SaveLocal();
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update() {
		if (!moving) {
			return;
		}

		timer += Time.deltaTime;
		float t = Mathf.Min(1.0f, timer / interpolationTime);

		if (matchMode == Space.Self) {
			gameObject.transform.localPosition =
				Vector3.Lerp(oldPosition, newPosition, t);
			gameObject.transform.localRotation =
				Quaternion.Slerp(oldRotation, newRotation, t);
		} else {
			gameObject.transform.position =
				Vector3.Lerp(oldPosition, newPosition, t);
			gameObject.transform.rotation =
				Quaternion.Slerp(oldRotation, newRotation, t);
		}

		camera.fieldOfView = Mathf.Lerp(oldFieldOfView, newFieldOfView, t);

		if (timer >= interpolationTime) {
			moving = false;
			Invoke("ReEnableAnimator", 0.5f);

			if (matchMode == Space.Self) {
				// Back to default local position.
				camera.cullingMask = defaultCullingMask;
			}
		}
	}

	public void MatchWorld(Camera c, float length = 2.0f) {
		matchMode = Space.World;
		interpolationTime = length;
		moving = true;
		animator.enabled = false;

		oldPosition = gameObject.transform.position;
		oldRotation = gameObject.transform.rotation;
		oldFieldOfView = camera.fieldOfView;

		newPosition = c.transform.position;
		newRotation = c.transform.rotation;
		newFieldOfView = c.fieldOfView;

		camera.cullingMask = camera.cullingMask & ~playerWeaponMask;

		timer = 0.0f;
	}

	public void SaveLocal() {
		defaultLocalPosition = gameObject.transform.localPosition;
		defaultLocalRotation = gameObject.transform.localRotation;
		defaultFieldOfView = camera.fieldOfView;
		defaultCullingMask = camera.cullingMask;
	}

	public void ResetLocal(float length = 2.0f) {
		matchMode = Space.Self;
		interpolationTime = length;
		moving = true;
		animator.enabled = false;

		oldPosition = gameObject.transform.localPosition;
		oldRotation = gameObject.transform.localRotation;
		oldFieldOfView = camera.fieldOfView;

		newPosition = defaultLocalPosition;
		newRotation = defaultLocalRotation;
		newFieldOfView = defaultFieldOfView;

		timer = 0.0f;
	}

	public void ForceResetLocal() {
		matchMode = Space.Self;
		moving = false;
		interpolationTime = 0.0f;
		timer = 0.0f;

		gameObject.transform.localPosition = defaultLocalPosition;
		gameObject.transform.localRotation = defaultLocalRotation;
		camera.fieldOfView = defaultFieldOfView;
	}

	public void ReEnableAnimator() {
		animator.enabled = true;
	}

}
