using UnityEngine;
using System.Collections;

public abstract class AbstractPuzzle : MonoBehaviour {
	
	public InteractableObject puzzleEntrance;
	public GameObject[] enableList;
	public GameObject[] disableList;
	public bool usePuzzleMode = true;

	protected UIController userInterface;

	public void EnterPuzzle() {
		if (!BeforeEnterPuzzle()) {
			return;
		}

		if (userInterface == null) {
			userInterface = GameObject.FindGameObjectWithTag(Tags.ui).GetComponent<UIController>();
		}

		enabled = true;
		puzzleEntrance.Hide();

		if (usePuzzleMode) {
			userInterface.EnterPuzzle();
		}
		
		foreach (GameObject go in enableList) {
			go.SetActive(true);
		}
		
		foreach (GameObject go  in disableList) {
			go.SetActive(false);
		}
		
		OnEnterPuzzle();
	}

	protected virtual bool BeforeEnterPuzzle() {
		return true;
	}

	protected abstract void OnEnterPuzzle();

	protected void ExitPuzzle() {
		enabled = false;
		puzzleEntrance.type = InteractableObject.InteractionType.PuzzleEntranceComplete;
		puzzleEntrance.Reshow();

		if (usePuzzleMode) {
			userInterface.ExitPuzzle();
		}

		foreach (GameObject go in enableList) {
			go.SetActive(false);
		}
		
		foreach (GameObject go  in disableList) {
			go.SetActive(true);
    	}
  	}

}
