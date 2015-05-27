using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuListing : MonoBehaviour {

	public Text text;
	public string label;
	public System.Action callback;
	public MainMenu mainMenu;

	bool selected = false;

	// Update is called once per frame
	void Update() {
		text.text = label;
	}

	public void Attach(GameObject parent) {
		transform.SetParent(parent.transform);
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		transform.localScale = Vector3.one;
	}

	public void Select() {
		mainMenu.DeselectAll();
		GetComponent<Animator>().SetBool("selected", true);
		selected = true;
	}

	public void Deselect() {
		GetComponent<Animator>().SetBool("selected", false);
		selected = false;
	}

	public void Activate() {
		GetComponent<Animator>().SetTrigger("activate");
		callback();
	}

	public bool Selected {
		get {
			return selected;
		}
	}

}
