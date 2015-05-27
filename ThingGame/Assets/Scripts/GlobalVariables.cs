using UnityEngine;
using System.Collections;

public class GlobalVariables : MonoBehaviour {

	public static bool didStartGame = false;
	public static bool didResetResolution = false;

	public static string nextLevel;
	public static string gameLevel = Scenes.gameDungeon;
//	public static string gameLevel = Scenes.gameCourtyard;
//	public static string gameLevel = Scenes.gameThroneRoom;
//	public static string gameLevel = Scenes.gameBossBattle;
	public static UIFade.FadeColor previousFadeColor = UIFade.FadeColor.Black;

	public static bool bossDefeated = false;

	public static bool invertedMouseX = false;
	public static bool invertedMouseY = false;

	public static Telemetry telemetry = new Telemetry();

	void Awake () {
		GameObject.DontDestroyOnLoad(gameObject);
	}
}
