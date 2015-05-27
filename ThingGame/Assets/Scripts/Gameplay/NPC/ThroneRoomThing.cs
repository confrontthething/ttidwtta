using UnityEngine;
using System.Collections;

public class ThroneRoomThing : MonoBehaviour {

	public ThroneRoomNarration narration;
	public bool attackable = false;
	bool hit = false;

	public void Hit() {
		if (!hit && attackable) {
			narration.ThingHit();
			hit = true;
		}
	}

}
