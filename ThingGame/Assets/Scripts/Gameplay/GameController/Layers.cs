using UnityEngine;
using System.Collections;

public class Layers : MonoBehaviour {

	public const int default_layer = 0;
	public const int transparentFX = 1;
	public const int ignoreRaycast = 2;
	public const int water = 4;
	public const int ui = 5;
	public const int gameDungeon_realtimeOnly = 8;
	public const int gameDungeon_bakedOnly = 9;
	public const int interactableObject = 10;
	public const int projectile = 11;
	public const int player = 12;
	public const int enemy = 13;
	public const int fpsWeapons = 14;
	public const int playerWallOnly = 15;
	public const int ignoreProjectile = 16;
	public const int glow = 17;
	public const int particleFX = 18;

	public const string default_layer_string = "Default";
	public const string transparentFX_string = "TransparentFX";
	public const string ignoreRaycast_string = "Ignore Raycast";
	public const string water_string = "Water";
	public const string ui_string = "UI";
	public const string gameDungeon_realtimeOnly_string = "GameDungeon-RealtimeOnly";
	public const string gameDungeon_bakedOnly_string = "GameDungeon-BakedOnly";
	public const string interactableObject_string = "InteractableObject";
	public const string projectile_string = "Projectile";
	public const string player_string = "Player";
	public const string enemy_string = "Enemy";
	public const string fpsWeapons_string = "FPS Weapons";
	public const string playerWallOnly_string = "PlayerWallOnly";
	public const string ignoreProjectile_string = "IgnoreProjectile";
	public const string glow_string = "Glow";
	public const string particleFX_string = "Particle FX";

	public static int GetLayerMaskWith (params int[] layers) {
		int mask = 1;
		foreach (int layer in layers) {
			mask = mask << layer;
		}
		return mask;
	}

	public static int GetLayerMaskWithout (params int[] layers) {
		int mask = 1;
		foreach (int layer in layers) {
			mask = mask << layer;
		}
		return ~mask;
	}
}
