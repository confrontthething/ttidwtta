using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour {

	private static readonly KeyCode ACTIVATION_KEY = KeyCode.E;

	public enum InteractionType {
		LevelNavigation = 0, UnlitMagicSource = 1, LitMagicSource = 2,
		HealthPickup = 3, PuzzleEntrance = 4, PuzzleEntranceComplete = 5,
		AcquireStaff = 6, CheatCode = 7, BossMagicSource = 8, BossHealthPickup = 9,
		GameMachine = 10
	};

	public InteractionType type;
	public GameObject interactArea;
	public GameObject keycap;
	public Text instruction;

	public bool enablePreciseLookAt = true;
	public LayerMask lookAtMask;

	private GameObject mainCamera;
	private PlayerStats player;
	private UIFade fader;

	// UnlitMagicSource, LitMagicSource
	public GameObject lightSource;

	// LevelNavigation
	public string nextLevelText = "Go to next level";
	public Scenes.SceneName levelName;
	public bool skipLoadingScreen = false;

	// PuzzleEntrance, PuzzleEntranceComplete
	public AbstractPuzzle puzzle;

	// AcquireStaff
	public AcquireStaffEvent staffEvent;

	// CheatCode
	public CheatCodePuzzle cheatPuzzle;

	// BossMagicSource, BossHealthPickup
	public Animator parentAnim;

	bool isActive = false;
	bool isHidden = false;
	bool isRevealed = false;

	bool didTrigger = false;

	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag(Tags.mainCamera);
		player = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStats>();
		fader = GameObject.FindGameObjectWithTag(Tags.ui).GetComponent<UIFade>();

		interactArea.SetActive(false);
		RefreshText();

		if (type == InteractionType.UnlitMagicSource) {
			lightSource.SetActive(false);
		}
	}

	void Update () {
		if (isActive && !isHidden) {
			RefreshText();

			// if user pressed left click
			if (Input.GetKeyDown(ACTIVATION_KEY)) {
				switch (type) {
				case InteractionType.LevelNavigation :
					Debug.Log("LevelNavigation activation");
					if (skipLoadingScreen) {
						fader.LoadLevel(Scenes.GetLevelName(levelName));
					} else {
						fader.LoadLevelWithLoadingScreen(Scenes.GetLevelName(levelName));
					}
					break;

				case InteractionType.LitMagicSource :
					Debug.Log("LitMagicSource activation");
					player.Recharge();
					Hide();
					break;

				case InteractionType.UnlitMagicSource :
					Debug.Log("UnlitMagicSource activation");
					// TODO: light magic source animation
					// TODO: player light magic source animation
					lightSource.SetActive(true);
					type = InteractionType.LitMagicSource;
					break;

				case InteractionType.HealthPickup :
					Animator anim = GetComponent<Animator>();
					if (isRevealed) {
						if (player.Health < player.MaxHealth) {
							anim.SetTrigger("heal");
							player.RechargeHealth();
							enabled = false;
							Deactivate();
							var audioHelper = GetComponent<AudioHelper>();
							if (audioHelper != null) {
								audioHelper.PlayUISound();
							}
						}
					} else {
						if (player.Health < player.MaxHealth) {
							anim.SetTrigger("heal");
							player.RechargeHealth();
							enabled = false;
							Deactivate();
							var audioHelper = GetComponent<AudioHelper>();
							if (audioHelper != null) {
								audioHelper.PlayUISound();
							}
						} else {
							anim.SetTrigger("reveal");
							isRevealed = true;
						}
					}
					break;
				case InteractionType.PuzzleEntrance :
					puzzle.EnterPuzzle();
					break;
				case InteractionType.AcquireStaff :
					staffEvent.EnableStaff();
					Deactivate();
					enabled = false;
					break;
				case InteractionType.CheatCode :
					cheatPuzzle.UncoverCheatCode();
					Deactivate();
					gameObject.SetActive(false);
					break;
				case InteractionType.BossMagicSource :
					player.Recharge();
					interactArea.SetActive(false);
					parentAnim.SetBool("on", false);
					break;
				case InteractionType.BossHealthPickup :
					player.RechargeHealth();
					interactArea.SetActive(false);
					parentAnim.SetBool("on", false);
					break;
				case InteractionType.GameMachine:
					GetComponent<GameMachine>().Activate();
					break;
				}
			}
		}
	}

	void OnTriggerEnter (Collider other) {
		if (enabled && other.gameObject.tag == Tags.player) {
			Activate ();

			RaycastHit hit;
			if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, Mathf.Infinity, lookAtMask)) {
				if (hit.collider.gameObject == gameObject) {
					Reshow();
				} else {
					Hide();
				}
			}
		}
	}

	void OnTriggerStay (Collider other) {
		if (enabled && other.gameObject.tag == Tags.player && enablePreciseLookAt) {
			if (!didTrigger) {
				Activate();
			}
			RaycastHit hit;
			if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, Mathf.Infinity, lookAtMask)) {
				if (hit.collider.gameObject == gameObject) {
					Reshow();
				} else {
					Hide();
				}
			}
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.gameObject.tag == Tags.player) {
			Reshow();
			Deactivate ();

			if (type == InteractionType.PuzzleEntranceComplete) {
				// Automatically disable the "puzzle complete" message
				// after the player has seen it.
				enabled = false;
			}
		}
	}

	public void Activate () {
		if (type == InteractionType.LitMagicSource && player.Magic == player.MaxMagic) {
			return;
		}

		RefreshText();
		interactArea.SetActive(true);
		isActive = true;
		isHidden = false;
		didTrigger = true;
	}

	public void Deactivate () {
		interactArea.SetActive(false);
		isActive = false;
		isHidden = false;
		didTrigger = false;
  }

	public void Hide () {
		if (isActive) {
			isHidden = true;
			interactArea.SetActive(false);
		}
	}

	public void Reshow () {
		if (isActive && isHidden) {
			if (type == InteractionType.LitMagicSource && player.Magic == player.MaxMagic) {
				return;
			}

			RefreshText();
			isHidden = false;
			interactArea.SetActive(true);

			// call Update() to prevent delay
			Update();
		}
	}

	void RefreshText () {
		string text;
		bool actionable;

		switch (type) {
		case InteractionType.LevelNavigation:
			text = nextLevelText;
			actionable = true;
			break;
		case InteractionType.LitMagicSource:
			text = "Recharge";
			actionable = true;
			break;
		case InteractionType.UnlitMagicSource:
			text = "Activate";
			actionable = true;
			break;
		case InteractionType.HealthPickup:
			if (isRevealed) {
				if (player.Health < player.MaxHealth) {
					text = "Recharge all hearts";
					actionable = true;
				} else {
					text = "Cannot recharge (max hearts)";
					actionable = false;
				}
			} else {
				text = "Open chest";
				actionable = true;
			}
			break;
		case InteractionType.PuzzleEntrance:
			text = "Enter puzzle";
			actionable = true;
			break;
		case InteractionType.PuzzleEntranceComplete:
			text = "Puzzle complete!";
			actionable = false;
			break;
		case InteractionType.AcquireStaff :
			text = "Pick up staff";
			actionable = true;
			break;
		case InteractionType.CheatCode:
			text = "Pick up paper";
			actionable = true;
			break;
		case InteractionType.BossMagicSource :
			text = "Activate";
			actionable = true;
			break;
		case InteractionType.BossHealthPickup :
			text = "Recharge all hearts";
			actionable = true;
			break;
		case InteractionType.GameMachine:
			text = "Examine game machine";
			actionable = true;
			break;
		default:
			throw new System.InvalidOperationException("No such object type!");
		}

		instruction.text = text;
		keycap.SetActive(actionable);
	}

}
