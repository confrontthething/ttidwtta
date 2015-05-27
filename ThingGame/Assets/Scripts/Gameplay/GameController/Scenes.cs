using UnityEngine;
using System.Collections;

public class Scenes : MonoBehaviour {

	public enum SceneName {
		Intro, HubWorld, CastleNovaTitleScreen, LoadingScreen, GameDungeon,
		GameCourtyard, GameThroneRoom, GameBossBattle, House, Otherworld,
		PsychWard, MainMenu, Controls,
	};

	public const string intro = "Intro";
	public const string hubWorld = "HubWorld";
	public const string castleNovaTitleScreen = "CastleNovaTitleScreen";
	public const string gameDungeon = "GameDungeon";
	public const string loadingScreen = "LoadingScreen";
	public const string gameCourtyard = "GameCourtyard";
	public const string gameThroneRoom = "GameThroneRoom";
	public const string gameBossBattle = "GameBossBattle";
	public const string house = "House";
	public const string otherworld = "Otherworld";
	public const string psychWard = "Hospital";
	public const string mainMenu = "MainMenu";
    public const string controls = "Controls";

    public static string GetLevelName (Scenes.SceneName scene) {
		switch (scene) {
		case SceneName.Intro :
			return intro;
		case SceneName.HubWorld :
			return hubWorld;
		case SceneName.CastleNovaTitleScreen :
			return castleNovaTitleScreen;
		case SceneName.GameDungeon :
			return gameDungeon;
		case SceneName.LoadingScreen :
			return loadingScreen;
		case SceneName.GameCourtyard :
			return gameCourtyard;
		case SceneName.GameThroneRoom :
			return gameThroneRoom;
		case SceneName.GameBossBattle :
			return gameBossBattle;
		case SceneName.House :
			return house;
		case SceneName.Otherworld :
			return otherworld;
		case SceneName.PsychWard:
			return psychWard;
		case SceneName.MainMenu :
			return mainMenu;
		case SceneName.Controls:
			return controls;
		default :
			return "";
		}
	}

}
