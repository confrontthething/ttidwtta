using UnityEngine;
using System.Collections;

public class BossPortal : MonoBehaviour {

	void Start () {
		// turn portal to boss battle off if boss has been defeated
		if (GlobalVariables.bossDefeated) {
			gameObject.SetActive(false);
		}
	}

}
