using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class CheatCodePuzzle : AbstractPuzzle {

	public DPek.Raconteur.RenPy.Display.RenPyDisplay[] renPyDisplays;
	public DPek.Raconteur.RenPy.Display.RenPyDisplay[] cheatCodeRenPyDisplays;
	public Transform narrationRoot;
	public Transform firstPersonController;
	public NarrationSync narrationSync;

	[Serializable]
	public class CheatKey {
		public enum Key {
			Up, Down, Left, Right
		}
		public Key key;
		public GameObject overlay;
	}

	public CheatKey[] cheatCode;

	public GlitchEffect glitchCamera;
	public CameraMatch mainCamera;
	public Camera cutsceneCamera;
	public GameObject bridge;
	public Collider bridgePathObstacle;
	public Animator staffPositionAnimator;

	public bool noiseEnabled = false;
	public float noiseLevel = 0.0f;
	public bool bridgeEnabled = false;

	private int cheatCodesFound = 0;
	private bool interactive = false;
	private int interactiveIndex = -1;

	public bool debugHaveAllCodes = false;

	private Animator animator;
	private bool available = true;

	void Start() {
		animator = GetComponent<Animator>();

		if (debugHaveAllCodes) {
			cheatCodesFound = 3;
		}
	}

	void Update() {
		narrationRoot.position = firstPersonController.position;

		animator.SetBool("dialogueRunningFirst", renPyDisplays[1].Running);
		animator.SetBool("dialogueRunningSecond", renPyDisplays[2].Running);
		animator.SetBool("dialogueRunningGetFirst",
			cheatCodeRenPyDisplays[0].Running);
		animator.SetBool("dialogueRunningGetSecond",
			cheatCodeRenPyDisplays[1].Running);
		animator.SetBool("dialogueRunningGetThird",
			cheatCodeRenPyDisplays[2].Running);

		glitchCamera.enabled = noiseEnabled;
		if (noiseEnabled) {
			glitchCamera.intensity = 1.0f * noiseLevel;
		}

		bridge.SetActive(bridgeEnabled);
		bridgePathObstacle.enabled = !bridgeEnabled;

		if (interactive) {
			if (interactiveIndex == cheatCode.Length) {
				// Player got everything right! End the puzzle.
				animator.SetBool("interactive", false);
				// Don't call ExitPuzzle() here; the animation will call it.
			} else if (!renPyDisplays[3].Running) {
				// Player still needs to continue.
				animator.SetBool("interactiveDialogueComplete", true);

				CheatKey curKey = cheatCode[interactiveIndex];

				bool down = Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S);
				bool up = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
				bool left = Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A);
				bool right = Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);

				bool wrong = (down && curKey.key != CheatKey.Key.Down)
					|| (up && curKey.key != CheatKey.Key.Up)
					|| (left && curKey.key != CheatKey.Key.Left)
					|| (right && curKey.key != CheatKey.Key.Right);

				bool correct = (down && curKey.key == CheatKey.Key.Down)
					|| (up && curKey.key == CheatKey.Key.Up)
					|| (left && curKey.key == CheatKey.Key.Left)
					|| (right && curKey.key == CheatKey.Key.Right);

				if (wrong) {
					interactiveIndex = 0;
					foreach (CheatKey key in cheatCode) {
						key.overlay.SetActive(false);
					}
				} else if (correct) {
					curKey.overlay.SetActive(true);
					interactiveIndex++;
				}
			}
		}
	}

	public void UncoverCheatCode() {
		if (narrationSync != null) {
			narrationSync.ForceStopDialogue(); // Emergency stop to prevent conflicts.
		}
		
		cheatCodesFound++;
		if (cheatCodesFound == 1) {
			animator.SetTrigger("revealFirstCutscene");
			UncoverCheatCodeDialogue (1);
		} else if (cheatCodesFound == 2) {
			UncoverCheatCodeDialogue (2);
			animator.SetTrigger("revealSecondCutscene");
		} else if (cheatCodesFound == 3) {
			UncoverCheatCodeDialogue (3);
			animator.SetTrigger("revealLastCutscene");
		}
	}

	protected override bool BeforeEnterPuzzle() {
		if (!available) {
			return false;
		}

		if (narrationSync != null) {
			narrationSync.ForceStopDialogue(); // Emergency stop to prevent conflicts.
		}

		if (cheatCodesFound == 0) {
			Dialogue(0);
		} else if (cheatCodesFound == 1) {
			available = false;
			animator.SetTrigger("redisplayFirstCutscene");
		} else if (cheatCodesFound == 2) {
			available = false;
			animator.SetTrigger("redisplaySecondCutscene");
		} else if (cheatCodesFound == 3) {
			available = false;
			return true;
		}

		return false;
	}

	protected override void OnEnterPuzzle() {
		animator.SetBool("interactive", true);
		staffPositionAnimator.SetBool("hidden", true);
	}

	void EnableUserInput() {
		interactive = true;
		interactiveIndex = 0;
	}

	void DisableUserInput() {
		interactive = false;
		interactiveIndex = -1;

		foreach (CheatKey key in cheatCode) {
			key.overlay.SetActive(false);
		}
	}

	void Dialogue(int codesFound) {
		if (codesFound == 0) {
			if (renPyDisplays[0].Running) {
				Debug.Log("dialogue already running; do not restart");
			} else {
				Debug.Log("dialogue stopped; going to begin now");
				renPyDisplays[0].StartDialog();
			}
		} else if (codesFound == 1) {
			renPyDisplays[1].StartDialog();
		} else if (codesFound == 2) {
			renPyDisplays[2].StartDialog();
		} else if (codesFound == 3) {
			renPyDisplays[3].StartDialog();
		}
	}

	void UncoverCheatCodeDialogue(int codesFound) {
		if (codesFound == 1) {
			cheatCodeRenPyDisplays[0].StartDialog();
		} else if (codesFound == 2) {
			cheatCodeRenPyDisplays[1].StartDialog();
		} else if (codesFound == 3) {
			cheatCodeRenPyDisplays[2].StartDialog();
		}
	}

	void ForceStopDialogue(int codesFound) {
		if (codesFound == 0) {
			renPyDisplays[0].StopDialog();
		} else if (codesFound == 1) {
			renPyDisplays[1].StopDialog();
		} else if (codesFound == 2) {
			renPyDisplays[2].StopDialog();
		} else if (codesFound == 3) {
			renPyDisplays[3].StopDialog();
		}
	}

	void StartBridgeCutscene(float time) {
		mainCamera.SaveLocal();
		mainCamera.MatchWorld(cutsceneCamera, time);
	}

	void EndBridgeCutscene(float time) {
		mainCamera.ResetLocal(time);
	}

	void ExitCheatCodePuzzle() {
		ExitPuzzle();
		staffPositionAnimator.SetBool("hidden", false);
	}

	void ReactivateAfterDelay() {
		Invoke("Reactivate", 0.5f);
	}

	void Reactivate() {
		available = true;
	}

}
