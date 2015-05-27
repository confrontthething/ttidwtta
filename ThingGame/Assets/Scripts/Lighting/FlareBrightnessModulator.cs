using UnityEngine;
using System.Collections;

public class FlareBrightnessModulator : MonoBehaviour {

	public float minBrightness = 0.0f;
	public float maxBrightness = 1.0f;
	
	public float fadeSeconds = 2.0f;
	
	private PlayerStats playerStats;
	
	private float targetPercentage = 1.0f;	
	private float oldPercentage = 1.0f;
	private float apparentPercentage = 1.0f;
	private float fadeTime = 1.0f;

	private LensFlare flare = null;

	void Start() {
		playerStats = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStats>();
		flare = GetComponent<LensFlare>();
	}

	// Update is called once per frame
	void Update() {
		if (playerStats != null) {
			float currentMagic = playerStats.MagicPercentage;
			if (targetPercentage != currentMagic) {
				FadeInPercentage(currentMagic);
			}
		}
		
		if (fadeTime < 1.0f) {
			fadeTime = Mathf.Clamp(fadeTime + Time.deltaTime / fadeSeconds, 0, 1);
		}
		
		apparentPercentage = Mathf.Lerp(oldPercentage, targetPercentage, fadeTime);

		// Quadratically adjust y = 1 - (1-x)^2.
		float quadraticBrightness = 1.0f - Mathf.Pow(1.0f - apparentPercentage, 2.0f);
		flare.brightness = Mathf.Lerp(minBrightness, maxBrightness, quadraticBrightness);
	}
	
	void FadeInPercentage(float p) {
		fadeTime = 0.0f;
		oldPercentage = apparentPercentage;
		targetPercentage = p;
	}

}
