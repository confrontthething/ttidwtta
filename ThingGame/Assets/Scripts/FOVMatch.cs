using UnityEngine;
using System.Collections;

public class FOVMatch : MonoBehaviour {

	public Camera source;
	
	// Update is called once per frame
	void Update () {
		camera.fieldOfView = source.fieldOfView;
	}

}
