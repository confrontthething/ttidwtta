using UnityEngine;
using System.Collections;

public class BurnableObject : MonoBehaviour {

	Animator anim;

	void Start () {
		anim = GetComponent<Animator>();
	}

	public void Burn () {
		anim.SetTrigger("hit");
	}
	
}
