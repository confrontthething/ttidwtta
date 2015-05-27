using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/,door/Scanline")]
public class Scanline : ImageEffectBase {

	public int cols = 360;
	public int rows = 180;
	public float intensity = 0.3f;
	
	public void OnRenderImage (RenderTexture src, RenderTexture dst) {
		material.SetFloat ("_Cols", cols);
		material.SetFloat ("_Lines", rows);
		material.SetFloat ("_Intensity", intensity);
		Graphics.Blit (src, dst, material);
	}
	
}
