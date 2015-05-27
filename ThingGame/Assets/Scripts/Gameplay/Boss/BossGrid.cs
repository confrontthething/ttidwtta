using UnityEngine;
using System.Collections;

public class BossGrid : MonoBehaviour {

	public GameObject origin;
	public GameObject limit;

	public GameObject linePrefab;

	public int n = 3;
	public int m = 3;

	private Vector3 originPos;
	private Vector3 limitPos;
	private Vector2 unit;

	private Vector2 playerPos;

	private GameObject[] lines;

	/* 
	 * Layout: (x,y)
	 * 
	 * 		[BOSS]
	 * (0,0) . . . (n,0)
	 *	 .			 .
	 *	 .			 .
	 *	 .			 .
	 * (0,m) . . . (n,m)
	 * 
	 */

	private GameObject player;

	void Start () {
		limitPos = limit.transform.position;
		originPos = origin.transform.position;
		unit = new Vector2((Mathf.Abs(limitPos.z) + Mathf.Abs(originPos.z)) / n,
		                   (Mathf.Abs(limitPos.x) + Mathf.Abs(originPos.x)) / m);

		player = GameObject.FindWithTag(Tags.player);

		DrawLines();
	}
	
	void Update () {
		CalculatePlayerPosition();
	}

	void DrawLines () {
		lines = new GameObject[n + m + 2];
		int lineIndex = 0;

		GameObject line;
		LineRenderer lRenderer;

		Vector3 startPos = originPos;
		Vector3 endPos = originPos;

		Vector3 nUnit = new Vector3(0f, 0f, (float)unit.x);
		Vector3 mUnit = new Vector3((float)unit.y, 0f, 0f);

		endPos -= nUnit * n;
		for (int i = 0; i < n+1; i++) {

			// (0,0)---(n,0)
			//      ...
			// (0,m)---(n,m)

			line = Instantiate(linePrefab, originPos, Quaternion.identity) as GameObject;
			lRenderer = line.GetComponent<LineRenderer>();
			lRenderer.SetPosition(0, startPos);
			lRenderer.SetPosition(1, endPos);

			lines[lineIndex++] = line;

			startPos -= mUnit;
			endPos -= mUnit;
		}

		startPos = originPos;
		endPos = originPos;
		endPos -= mUnit * m;
		for (int i = 0; i < m+1; i++) {

			// (0,0)   (n,0)
			//   |  ...  |
			// (0,m)   (n,m)

			line = Instantiate(linePrefab, originPos, Quaternion.identity) as GameObject;
			lRenderer = line.GetComponent<LineRenderer>();
			lRenderer.SetPosition(0, startPos);
			lRenderer.SetPosition(1, endPos);

			lines[lineIndex++] = line;

			startPos -= nUnit;
			endPos -= nUnit;
		}
	}

	public void EnableLines () {
		Animator anim;
		foreach (GameObject line in lines) {
			anim = line.GetComponent<Animator>();
			anim.SetTrigger("enable");
		}
	}

	public void DisableLines () {
		Animator anim;
		foreach (GameObject line in lines) {
			anim = line.GetComponent<Animator>();
			anim.SetTrigger("disable");
		}
	}

	void CalculatePlayerPosition () {
		Vector3 gridPos = player.transform.position - originPos;
		playerPos.x = (int) Mathf.Abs(gridPos.z / unit.x);
		playerPos.y = (int) Mathf.Abs(gridPos.x / unit.y);
	}

	public Vector2 PlayerPos {
		get {
			return playerPos;
		}
	}
}
