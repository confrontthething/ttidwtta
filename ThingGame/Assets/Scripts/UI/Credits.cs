using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	public Blur blurEffect;
	public float blurAmount;

	public event System.EventHandler FinishedCredits;

	public enum CreditsState {
		Normal, StandaloneCredits, EndingCredits
	}

	private CreditsState state = CreditsState.Normal;
	private Animator animator;

	// Use this for initialization
	void Start() {
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update() {
		if (blurEffect != null) {
			if (blurAmount > 0.0f) {
				blurEffect.blurSize = blurAmount;
				blurEffect.enabled = true;
			} else {
				blurEffect.enabled = false;
			}
		}

		if (Input.anyKey) {
			if (state == CreditsState.StandaloneCredits) {
				EndCreditsStandalone();
			} else if (state == CreditsState.EndingCredits) {
				DidHideEndingCredits();
			}
		}
	}

	public void ShowHospitalEnding() {
		GlobalVariables.telemetry.PlayerChoseFrinchfry();
		GlobalVariables.telemetry.SendTelemetry();
		animator.SetTrigger("hospitalQuote");
	}

	public void ShowHouseEnding() {
		GlobalVariables.telemetry.PlayerChoseTheThing();
		GlobalVariables.telemetry.SendTelemetry();
		animator.SetTrigger("houseQuote");
	}

	public void ShowCreditsStandalone() {
		animator.SetBool("credits", true);
		state = CreditsState.StandaloneCredits;
	}

	public void EndCreditsStandalone() {
		animator.SetBool("credits", false);
		state = CreditsState.Normal;
	}

	void DidEndCreditsStandalone() {
		System.EventHandler handler = FinishedCredits;
		if (handler != null) {
			handler(this, System.EventArgs.Empty);
		}
	}

	public void ShowIntroQuotation() {
		animator.SetTrigger("introQuote");
	}

	void DidEndIntroQuotation() {
		// Load the intro scene!
		GlobalVariables.didStartGame = true;
		UIFade fader =
			GameObject.FindGameObjectWithTag(Tags.ui).GetComponent<UIFade>();
		fader.LoadLevel(Scenes.intro);
	}

	void DidShowEndingCredits() {
		state = CreditsState.EndingCredits;
	}

	void DidHideEndingCredits() {
		state = CreditsState.Normal;

		// Load the main menu again!
		UIFade fader =
			GameObject.FindGameObjectWithTag(Tags.ui).GetComponent<UIFade>();
		fader.LoadLevel(Scenes.mainMenu);
	}

}
