using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

	private PlayerStats playerStats;
	private PlayerRespawn playerRespawn;

	public RectTransform magicBar;
	public Text magicText;
	public Text healthActive;
	public Text healthUsed;
	public Text healthText;
	public Animator screenRed;

	private const char HEALTH_SYMBOL = '\u2665';

	private Animator animator;

	void Start() {
		GameObject player = GameObject.FindGameObjectWithTag(Tags.player);
		playerStats = player.GetComponent<PlayerStats>();
		playerRespawn = player.GetComponent<PlayerRespawn>();

		animator = GetComponent<Animator>();
	}

	void Update() {
		if (magicBar != null) {
			magicBar.anchorMax = new Vector2(playerStats.MagicPercentage, 1.0f);
		}
		if (magicText != null) {
			magicText.text = string.Format("{0}", playerStats.Magic);
		}
		if (healthActive != null) {
			int redHearts = Mathf.Max(0, playerStats.Health);
			healthActive.text = new string(HEALTH_SYMBOL, redHearts);
		}
		if (healthUsed != null) {
			int grayHearts = playerStats.MaxHealth - Mathf.Max(0, playerStats.Health);
			healthUsed.text = new string(HEALTH_SYMBOL, grayHearts);
		}
		if (healthText != null) {
			healthText.text = string.Format("{0}", playerStats.Health);
		}
	}

	void EnterModal() {
		playerStats.EnterModal();
	}

	void ExitModal() {
		playerStats.ExitModal();
	}

	public void FadeAndRespawn() {
		animator.SetTrigger("respawn");
	}

	void RespawnCallback() {
		if (playerRespawn != null) {
			playerRespawn.ImmediatelyRespawn();
		}
	}

	public void FadeAndRestartLevel() {
		animator.SetTrigger("gameOver");
	}

	void RestartLevelCallback() {
		if (playerRespawn != null) {
			playerRespawn.RestartLevel();
		}
	}

	public void RedFlash() {
		if (screenRed != null) {
			screenRed.SetTrigger("damage");
		}
	}

	/// Cutscenes hide all UI, including dialogue, and present with letterboxes.
	public void EnterCutscene() {
		animator.SetBool("cutscene", true);
	}

	public void ExitCutscene() {
		animator.SetBool("cutscene", false);
	}

	/// Puzzles hide all UI, including dialogue, but do not present with
	/// letterboxes.
	public void EnterPuzzle() {
		animator.SetBool("puzzle", true);
	}

	public void ExitPuzzle() {
		animator.SetBool("puzzle", false);
	}

	/// Dialogue sequences hide all UI except for dialogue, which remains visible.
	/// They do not present with letterboxes.
	public void EnterDialogueSequence() {
		animator.SetBool("dialogueSequence", true);
	}

	public void ExitDialogueSequence() {
		animator.SetBool("dialogueSequence", false);
	}

	/// Dialogue cutscenes hide all UI except for dialogue, which remains visible.
	/// They present with letterboxes.
	public void EnterDialogueCutscene() {
		animator.SetBool("dialogueCutscene", true);
	}

	public void ExitDialogueCutscene() {
		animator.SetBool("dialogueCutscene", false);
	}

	public void Dip() {
		DipWhite();
	}

	public void DipWhite() {
		animator.SetBool("fadeWhite", true);
		animator.SetTrigger("dip");
	}

	public void DipBlack() {
		animator.SetBool("fadeWhite", false);
		animator.SetTrigger("dip");
	}

	public UIFade Fader {
		get {
			return GetComponent<UIFade>();
		}
	}

	public void PlayUISoundOneShot(AudioClip clip, float volume) {
		audio.PlayOneShot(clip, volume);
	}

}
