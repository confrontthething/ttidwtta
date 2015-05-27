using UnityEngine;
using System.Collections;

public class BossEruption : MonoBehaviour {

	public Animator[] anims;

	public BossAI bossAI;

	private int numEruptions = 3;

	private int[] points;

	void Start () {
		points = new int[anims.Length];
		for (int i = 0; i < points.Length; i++) {
			points[i] = i;
		}
	}

	public void ActivateEruption () {
		// get randomized list
		for (int i = 0; i < points.Length; i++) {
			int tmp = points[i];
			int r = Random.Range(i, points.Length);
			points[i] = points[r];
			points[r] = tmp;
		}

		// activate eruptions up to numEruptions
		for (int i = 0; i < numEruptions && i < anims.Length; i++) {
			anims[points[i]].SetTrigger("activate");
		}
	}
	
	public void SpeedUp (float increment) {
		foreach (Animator anim in anims) {
			anim.speed += increment;
		}
		numEruptions++;
	}

	public void CancelAnimation () {
		foreach (Animator anim in anims) {
			anim.SetTrigger("cancel");
		}
	}

	public void Dead () {
		foreach (Animator anim in anims) {
			anim.SetBool("dead", true);
		}
	}
}
