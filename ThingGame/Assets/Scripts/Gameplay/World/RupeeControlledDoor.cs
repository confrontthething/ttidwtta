using UnityEngine;
using System.Collections;

public class RupeeControlledDoor : MonoBehaviour {

	[System.Serializable]
	public class RupeeControl {
		public string name; /* Must match the boolean parameter in the Animator. */
		public Animator rupeeAnimator;
		public InteractableObject magicSource;
	}

	public RupeeControl[] rupeeControls;
	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (RupeeControl rc in rupeeControls) {
			if (rc.magicSource.type == InteractableObject.InteractionType.LitMagicSource) {
				animator.SetBool(rc.name, true);
			}
		}
	}

	/***** Begin animation event functions. *****/

	void DeactivateAllMagicSources() {
		// Used to hide the interact text in the split-second before the cutscene starts.
		foreach (RupeeControl rc in rupeeControls) {
			rc.magicSource.Hide();
	    }
	}

	void ReactivateCurrentMagicSource() {
		foreach (RupeeControl rc in rupeeControls) {
			rc.magicSource.Reshow();
		}
	}

	void LightRupee(string name) {
		// Linear-time search but not enough items for us to really care about hashtables.
		foreach (RupeeControl rc in rupeeControls) {
			if (rc.name == name) {
				rc.rupeeAnimator.SetBool("lit", true);
			}
		}
	}

	void LightAllRupees() {
		foreach (RupeeControl rc in rupeeControls) {
			rc.rupeeAnimator.SetBool("lit", true);
    	}
	}

}
