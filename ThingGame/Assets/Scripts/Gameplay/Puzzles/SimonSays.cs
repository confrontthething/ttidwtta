using UnityEngine;
using System.Collections;

public class SimonSays : AbstractPuzzle {

	public Animator puzzleGlobalAnimator;
	public Animator puzzleCamera;
	public GameObject platformCamera;
	public CameraMatch mainCamera;
	public GameObject puzzleUI;
	public Animator staffAnimator;
	public SecretDoor exitDoor;

	public Animator wallRed;
	public Animator wallBlue;
	public Animator wallYellow;

	public Animator userRed;
	public Animator userBlue;
	public Animator userYellow;

	public Animator platformA;
	public Animator platformB;
	public Animator platformC;

	public enum Gemstone {
		Red = 0, Blue = 1, Yellow = 2
	}

	[Range(1, 10)]
	public int difficultyPuzzleA = 4;
	[Range(1, 10)]
	public int difficultyPuzzleB = 5;
	[Range(1, 10)]
	public int difficultyPuzzleC = 7;

	private Gemstone[] puzzleA;
	private Gemstone[] puzzleB;
	private Gemstone[] puzzleC;

	public enum Mode {
		WallGems, UserGems
	}

	private int currentPromptIndex = 0;
	private int currentPromptGemIndex = 0;
	private float lastCameraSwitchTime = 0f;

	private bool acceptPlayerInput = false;

	/**
	 * Delay between camera motion and input.
	 * There is zero delay between two inputs however.
	 */
	private const float INPUT_DELAY = 1.0f;

	public static void GeneratePuzzle(ref Gemstone[] puzzle,
		int difficulty,
		System.Random rand) {
		puzzle = new Gemstone[difficulty];
		for (int i = 0; i < difficulty; i++) {
			bool nice;
			int tries = 0;
			do {
				nice = true;
				tries++;

				puzzle[i] = (Gemstone)rand.Next(3);

				if (i >= 2) {
					// We shall enforce niceness. Everyone be nice to each other :)
					if (puzzle[i] == puzzle[i - 1] && puzzle[i] == puzzle[i - 2]) {
						// BAD! NOT NICE! UGLY! THREE IN A ROW! :( :( :(
						nice = false;
					}
				}
			} while (!nice && tries < 100);
			// Theoretically if we don't bound our retries, it might go on forever.
			// The chance of getting three in a row is ~ (1/3) * (1/3)^(100).
		}
	}

	void GeneratePuzzles() {
		System.Random rand = new System.Random();
		GeneratePuzzle(ref puzzleA, difficultyPuzzleA, rand);
		GeneratePuzzle(ref puzzleB, difficultyPuzzleB, rand);
		GeneratePuzzle(ref puzzleC, difficultyPuzzleC, rand);
	}

	protected override void OnEnterPuzzle() {
		GeneratePuzzles();

		SetMode(Mode.WallGems);
		mainCamera.SaveLocal();
		puzzleGlobalAnimator.SetTrigger("enter");
		staffAnimator.SetBool("hidden", true);
	}

	void Exit() {
		mainCamera.ForceResetLocal(); // JUST IN CASE TROLOLOLOLOL.
		staffAnimator.SetBool("hidden", false);
		exitDoor.isActivated = true;
		ExitPuzzle();
	}

	Gemstone[] GetPuzzle(int index) {
		switch (index) {
		case 0:
			return puzzleA;
		case 1:
			return puzzleB;
		case 2:
			return puzzleC;
		default:
			throw new System.IndexOutOfRangeException("Puzzle index must be 0, 1, or 2");
		}
	}

	Animator GetPlatform(int index) {
		switch (index) {
		case 0:
			return platformA;
		case 1:
			return platformB;
		case 2:
			return platformC;
		default:
			throw new System.IndexOutOfRangeException("Platform index must be 0, 1, or 2");
    	}
	}

	Animator GetWallGem(Gemstone g) {
		switch (g) {
		case Gemstone.Red:
			return wallRed;
		case Gemstone.Blue:
			return wallBlue;
		case Gemstone.Yellow:
			return wallYellow;
		default:
			throw new System.IndexOutOfRangeException("Gem index must be 0, 1, or 2");
    	}
	}

	Animator GetUserGem(Gemstone g) {
		switch (g) {
		case Gemstone.Red:
			return userRed;
		case Gemstone.Blue:
			return userBlue;
		case Gemstone.Yellow:
			return userYellow;
		default:
			throw new System.IndexOutOfRangeException("Gem index must be 0, 1, or 2");
	    }
	}

	void SetMode(Mode m) {
		puzzleCamera.SetBool("player", m == Mode.UserGems);
		lastCameraSwitchTime = Time.time;
	}

	void UserSelectGemstone(Gemstone g) {
		acceptPlayerInput = false;

		Gemstone[] puzzle = GetPuzzle(currentPromptIndex);
		if (puzzle[currentPromptGemIndex] == g) {
			// User chose successfully.
			GetUserGem(g).SetTrigger("flash");

			if (currentPromptGemIndex == puzzle.Length - 1) {
				// User successfully finished this puzzle.
				FinishCurrentSimonPrompt();
			} else {
				// User correct, but puzzle not finished yet.
				currentPromptGemIndex++;
				acceptPlayerInput = true;
			}
		} else {
			// User was incorrect. We'll repeat the prompt.
			GlobalVariables.telemetry.IncrementSimonSaysErrors();
			Fail();
		}
	}

	void PlayNextSimonPrompt() {
		StartCoroutine(PlaySimonPrompt(currentPromptIndex + 1));
	}

	void ReplayCurrentSimonPrompt() {
		StartCoroutine(PlaySimonPrompt(currentPromptIndex));
	}

	IEnumerator PlaySimonPrompt(int index) {
		currentPromptIndex = index;
		currentPromptGemIndex = 0;

		// Wait a bit before starting.
		yield return new WaitForSeconds(1.0f);

		Gemstone[] puzzle = GetPuzzle(currentPromptIndex);

		foreach (Gemstone gem in puzzle) {
			GetWallGem(gem).SetTrigger("flash");
			yield return new WaitForSeconds(1.5f);
		}

		// Pause a bit before going to player control.
		yield return new WaitForSeconds(1.0f);
		SetMode(Mode.UserGems);
		acceptPlayerInput = true;
	}

	void FinishCurrentSimonPrompt() {
		if (currentPromptIndex == 2) {
			puzzleGlobalAnimator.SetTrigger("succeed");
    	} else {
			puzzleGlobalAnimator.SetTrigger("proceed");
    	}
  	}

	void Fail() {
		puzzleGlobalAnimator.SetTrigger("fail");
	}

	void FlashWrongRandomGem() {
		int gemIdx = Random.Range(0, 3);
		switch (gemIdx) {
		case 0:
			GetUserGem(Gemstone.Red).SetTrigger("flashWrong");
			break;
		case 1:
			GetUserGem(Gemstone.Blue).SetTrigger("flashWrong");
			break;
		default:
			GetUserGem(Gemstone.Yellow).SetTrigger("flashWrong");
			break;
		}
	}

	void RaiseCurrentPlatform() {
		GetPlatform(currentPromptIndex).SetBool("raised", true);
	}

	void Update() {
		float timeDiff = Time.time - lastCameraSwitchTime;

		if (acceptPlayerInput && timeDiff > INPUT_DELAY) {
			// Enable the interact text.
			puzzleUI.SetActive(true);

			// Check for user selection.
			if (Input.GetKeyDown(KeyCode.Alpha1)) {
				UserSelectGemstone(Gemstone.Red);
			} else if (Input.GetKeyDown(KeyCode.Alpha2)) {
				UserSelectGemstone(Gemstone.Blue);
			} else if (Input.GetKeyDown(KeyCode.Alpha3)) {
				UserSelectGemstone(Gemstone.Yellow);
			}
		} else {
			// Disable the interact text if player can't do anything.
			puzzleUI.SetActive(false);
		}
	}

	void MatchMainCamToPuzzle(float length) {
		mainCamera.MatchWorld(puzzleCamera.camera, length);
	}

	void ForceResetMainCam() {
		mainCamera.ForceResetLocal();
	}

}
