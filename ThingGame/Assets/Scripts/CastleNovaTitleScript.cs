using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CastleNovaTitleScript : MonoBehaviour {

	public Text insertCoin;
	public Text levels;
	private Animator anim;

	public AudioSource insertCoinAudio;
	public AudioSource startAudio;
	public AudioSource itemSelectAudio;
	int coinCnt = 0;
	bool selectSoundPlayed = false;

	void Start () {
		anim = GetComponent<Animator>();

		switch (GlobalVariables.gameLevel) {
		case Scenes.gameDungeon : 
			levels.text = "Level 1";
			anim.SetBool("level1", true);
			break;
		case Scenes.gameCourtyard : 
			insertCoin.text = "[ 2 credit(s) left ]";
			levels.text = "Level 1\nLevel 2";
			anim.SetBool("hasCredits", true);
			anim.SetBool("level2", true);
			break;
		case Scenes.gameThroneRoom : 
			insertCoin.text = "[ 1 credit(s) left ]";
			levels.text = "Level 1\nLevel 2\nLevel 10";
			anim.SetBool("hasCredits", true);
			anim.SetBool("level3", true);
			break;
		}
	}

	void InsertCoin () {
		coinCnt++;
		insertCoin.text = "[ " + coinCnt + " credit(s) left ]";
	}

	void PlayInsertCoinSound () {
		if (insertCoinAudio != null) {
			insertCoinAudio.Play();
		}
	}

	void PlayStartSound () {
		if (startAudio != null) {
			startAudio.Play();
		}
	}

	void PlayItemSelectSound () {
		if (itemSelectAudio != null && !selectSoundPlayed) {
			itemSelectAudio.Play();
			selectSoundPlayed = true;
		}
	}

	public void LoadGame () {
		UIFade fader = GameObject.FindGameObjectWithTag(Tags.ui).GetComponent<UIFade>();
		fader.LoadLevelWithLoadingScreen(GlobalVariables.gameLevel);
	}

}
