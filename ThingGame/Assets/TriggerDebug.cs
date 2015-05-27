using UnityEngine;
using System.Collections;

public class TriggerDebug : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	void onTriggerEnter (Collider Other){
		Debug.Log ("Object Entered the trigger");
	}
	void onTriggerStay (Collider Other){
		Debug.Log ("Object is within the trigger");
	}

	void onTriggerExit (Collider Other){
		Debug.Log ("Object Exited the trigger");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
