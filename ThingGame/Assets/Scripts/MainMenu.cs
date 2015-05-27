using UnityEngine;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {

	public Animator anim;
	public UIFade fader;
	public Credits credits;
	public GameObject menuItemsRoot;
	public GameObject menuItemPrefab;

	List<MenuListing> currentMenuListings;
	bool inputOn = true;

	const string HIGHLIGHT_COLOR = "#add8e6";

	void Awake()
	{
		// Switch to native resolution if this is the initial startup.
		if (!GlobalVariables.didResetResolution) {
			Resolution res = Screen.currentResolution;
			Screen.SetResolution(res.width, res.height, true, res.refreshRate);
			GlobalVariables.didResetResolution = true;
		}
	}

	void Start () {
		if (credits != null) {
			credits.FinishedCredits += (sender, eventArgs) => {
				EnableInput();
			};
		}

		GlobalVariables.didStartGame = false;
		GlobalVariables.gameLevel = Scenes.gameDungeon;
		GlobalVariables.bossDefeated = false;

		ShowMainMenu();
	}

	void Update () {
		if (!inputOn) {
			return;
		}

		// Determine currently selected item;
		int selectedIndex = -1;
		for (int i = 0; i < currentMenuListings.Count; i++) {
			if (currentMenuListings[i].Selected) {
				selectedIndex = i;
				break;
			}
		}

		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			if (selectedIndex == -1) {
				currentMenuListings[0].Select();
			} else {
				int nextIndex = Mod(selectedIndex - 1, currentMenuListings.Count);
				currentMenuListings[nextIndex].Select();
			}
		} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			if (selectedIndex == -1) {
				currentMenuListings[0].Select();
			} else {
				int nextIndex = Mod(selectedIndex + 1, currentMenuListings.Count);
				currentMenuListings[nextIndex].Select();
			}
		} else if (Input.GetKeyDown(KeyCode.Return) ||
							 Input.GetKeyDown(KeyCode.KeypadEnter)) {
			if (selectedIndex != -1) {
				currentMenuListings[selectedIndex].Activate();
			}
		}
	}

	void StartGame () {
		GlobalVariables.didStartGame = true;
		GlobalVariables.telemetry.BeginGameTimer();
		if (fader != null) {
			fader.LoadLevel(Scenes.controls);
		}
	}

	void ShowCredits () {
		credits.ShowCreditsStandalone();
	}

	void QuitGame () {
		AppHelper.Quit();
	}

	void DisableInput () {
		inputOn = false;
	}

	void EnableInput () {
		inputOn = true;
	}

	public void DeselectAll () {
		foreach (MenuListing listing in currentMenuListings) {
			listing.Deselect();
		}
	}

	static int Mod(int a, int b) {
    return (a % b + b) % b;
	}

	void AssembleMenuItems(string[] itemLabels, System.Action[] itemActions) {
		if (currentMenuListings == null) {
			currentMenuListings = new List<MenuListing>();
		} else {
			foreach (MenuListing ml in currentMenuListings) {
				Destroy(ml.gameObject);
			}
			currentMenuListings.Clear();
		}

		for (int i = 0; i < itemLabels.Length; i++) {
			var go = (GameObject)Instantiate(menuItemPrefab);
			var listing = go.GetComponent<MenuListing>();
			listing.Attach(menuItemsRoot);
			listing.label = itemLabels[i];
			listing.callback = itemActions[i];
			listing.mainMenu = this;
			currentMenuListings.Add(listing);
		}

		currentMenuListings[0].Select();
	}

	void ShowMainMenu() {
		string[] itemLabels = {
			"Start", "Options", "Credits", "Quit"
		};
		System.Action[] itemActions = {
			() => {
				DisableInput();
				Invoke("StartGame", 0.5f);
			},
			() => {
				DisableInput();
				Invoke("ShowOptions", 0.3f);
			},
			() => {
				DisableInput();
				Invoke("ShowCredits", 0.5f);
			},
			() => {
				DisableInput();
				Invoke("QuitGame", 0.4f);
			}
		};

		AssembleMenuItems(itemLabels, itemActions);
		EnableInput();
	}

	void ShowOptions() {
		string[] itemLabels = {
			"", "", "", "Back"
		};
		System.Action[] itemActions = {
			() => {
				DisableInput();
				Invoke("SwitchMouseInvertX", 0.3f);
			},
			() => {
				DisableInput();
				Invoke("SwitchMouseInvertY", 0.3f);
			},
			() => {
				DisableInput();
				Invoke("SwitchQuality", 0.3f);
			},
			() => {
				DisableInput();
				Invoke("ShowMainMenu", 0.3f);
			}
		};

		AssembleMenuItems(itemLabels, itemActions);
		UpdateOptionsLabels();
		EnableInput();
	}

	void SwitchMouseInvertX() {
		GlobalVariables.invertedMouseX = !GlobalVariables.invertedMouseX;
		UpdateOptionsLabels();
		EnableInput();
	}

	void SwitchMouseInvertY() {
		GlobalVariables.invertedMouseY = !GlobalVariables.invertedMouseY;
		UpdateOptionsLabels();
		EnableInput();
	}

	void SwitchQuality() {
		int availableLevels = QualitySettings.names.Length;
		int newQual = Mod(QualitySettings.GetQualityLevel() + 1, availableLevels);
		QualitySettings.SetQualityLevel(newQual);
		UpdateOptionsLabels();
		EnableInput();
	}

	void UpdateOptionsLabels() {
		if (GlobalVariables.invertedMouseX) {
			currentMenuListings[0].label =
				string.Format("Mouse Horizontal: <color={0}>Inverted</color>",
					HIGHLIGHT_COLOR);
		} else {
			currentMenuListings[0].label =
				string.Format("Mouse Horizontal: <color={0}>Not Inverted</color>",
					HIGHLIGHT_COLOR);
		}

		if (GlobalVariables.invertedMouseY) {
			currentMenuListings[1].label =
				string.Format("Mouse Vertical: <color={0}>Inverted</color>",
					HIGHLIGHT_COLOR);
		} else {
			currentMenuListings[1].label =
				string.Format("Mouse Vertical: <color={0}>Not Inverted</color>",
					HIGHLIGHT_COLOR);
		}

		currentMenuListings[2].label =
			string.Format("Quality: <color={0}>{1}</color>", HIGHLIGHT_COLOR,
				QualitySettings.names[QualitySettings.GetQualityLevel()]);
	}
}
