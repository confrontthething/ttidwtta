using UnityEngine;
using System.Collections;

public class UIFade : MonoBehaviour {

	public bool fadeIn = true;

	public enum FadeColor {
		Black = 0, White = 1
	}

	public FadeColor fadeOutColor = FadeColor.Black;

	public bool controlVolume = false;
	public float volume = 1.0f;
	public AudioSource[] audioToFade;
	private float[] originalVolume;

	public delegate void FadeOutAction();
	public event FadeOutAction OnFadeOut;

	private Animator animator;

	// Use this for initialization
	void Awake() {
		animator = GetComponent<Animator>();

		if (fadeIn) {
			animator.SetBool("fadeWhite", GlobalVariables.previousFadeColor == FadeColor.White);
			animator.SetTrigger("fadeIn");
		}
	}

	void Update() {
		if (controlVolume) {
			// Save the original volumes if we haven't already.
			if (originalVolume == null) {
				originalVolume = new float[audioToFade.Length];
				for (int i = 0; i < audioToFade.Length; i++) {
					originalVolume[i] = audioToFade[i].volume;
				}
			}

			// Scale the volumes by the volume parameter.
			for (int i = 0; i < audioToFade.Length; i++) {
				audioToFade[i].volume = originalVolume[i] * volume;
			}
		}
	}

	public void LoadLevel(string levelName) {
		OnFadeOut += () => {
			if (!string.IsNullOrEmpty(levelName)) {
				Application.LoadLevel(levelName);
     		}
		};

		GlobalVariables.previousFadeColor = fadeOutColor;
		animator.SetBool("fadeWhite", fadeOutColor == FadeColor.White);
		animator.SetTrigger("fadeOut");
	}

	IEnumerator LoadLevelAsync(string levelName) {
		AsyncOperation async = Application.LoadLevelAsync(levelName);
		async.allowSceneActivation = false;

		OnFadeOut += () => {
			async.allowSceneActivation = true;
		};

		// Wait for the scene to load before fading out.
		while (async.progress < 0.9f) {
			// Whoo, let's spin wait!
			yield return new WaitForSeconds(0.1f);
		}

		// Scene loaded in background! Let's fade.
		GlobalVariables.previousFadeColor = fadeOutColor;
		animator.SetBool("fadeWhite", fadeOutColor == FadeColor.White);
		animator.SetTrigger("fadeOut");
	}

	public void LoadNextLevelAsync() {
		StartCoroutine(LoadLevelAsync(GlobalVariables.nextLevel));
	}

	public void LoadLevelWithLoadingScreen(string levelName) {
		GlobalVariables.nextLevel = levelName;
		LoadLevel(Scenes.loadingScreen);
  	}

	void FadeOutCallback() {
		if (OnFadeOut != null) {
			OnFadeOut();
		}
	}

}
