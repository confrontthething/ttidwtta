using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossUIController : MonoBehaviour {

	public BossStats bossStats;
	public BossTarget[] targets;

	public RectTransform healthBar;
	public float barSpeed = 2f;

	void Update () {
		if (healthBar != null && bossStats != null) {
			float percentage = PercentHealth();
			float newMax = Mathf.Lerp(healthBar.anchorMax.x, percentage, Time.deltaTime * barSpeed);
			healthBar.anchorMax = new Vector2(newMax, 1.0f);
		}
	}

	float PercentHealth () {
		float totalHealth = 0;
		float currentHealth = 0;
		foreach (BossTarget target in targets) {
			totalHealth += target.MaxHealth + target.MaxHealth + target.MaxHealth;
			currentHealth += target.Health;
			if (target.Health != 0) {
				currentHealth += target.MaxHealth + target.MaxHealth;
			}
		}
		return (float)currentHealth / (float)totalHealth;
	}
}
