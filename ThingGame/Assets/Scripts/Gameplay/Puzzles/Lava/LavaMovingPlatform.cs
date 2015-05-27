using UnityEngine;
using System.Collections;

public class LavaMovingPlatform : MonoBehaviour {

	public Transform[] points;
	Hashtable pathArgs;
	int index = 0;

	public float speed = 4f;

	void Start () {
		transform.position = points[0].position;
		pathArgs = new Hashtable();
		pathArgs.Add("position", points[0]);
		pathArgs.Add("speed", speed);
		pathArgs.Add("easeType", "linear");
		pathArgs.Add("oncomplete", "GoToNext");

		GoToNext();
	}

	void OnDrawGizmos () {
		iTween.DrawLine(points, Color.cyan);
		iTween.DrawLine(new Transform[]{points[points.Length-1], points[0]}, Color.cyan);
	}
	
	void GoToNext () {
		pathArgs.Remove("position");
		pathArgs.Add("position", points[index]);

		iTween.MoveTo(gameObject, pathArgs);

		index = (index + 1) % points.Length;
	}

}
