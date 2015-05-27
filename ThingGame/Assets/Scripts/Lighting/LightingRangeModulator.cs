using UnityEngine;
using System.Collections;

public class LightingRangeModulator : MonoBehaviour {

	public float minRange = 0.0f;
	public float maxRange = 20.0f;

	public float fadeSeconds = 2.0f;

	private PlayerStats playerStats;
	
	private float targetPercentage = 1.0f;	
	private float oldPercentage = 1.0f;
	private float apparentPercentage = 1.0f;
	private float fadeTime = 1.0f;

	void Start () {
		playerStats = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStats>();
	}

	void Update () {
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
		light.range = Mathf.Lerp(minRange, maxRange, apparentPercentage);
	}

	void FadeInPercentage (float p) {
		fadeTime = 0.0f;
		oldPercentage = apparentPercentage;
		targetPercentage = p;
	}

}
