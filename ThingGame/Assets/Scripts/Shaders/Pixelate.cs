using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/,door/Pixelate")]
public class Pixelate : MonoBehaviour {

	public int rows = 180;

	public FilterMode preFilter = FilterMode.Trilinear;
	public FilterMode postFilter = FilterMode.Point;

	public Camera[] cameras;

	private RenderTexture renderTex;

	void Awake () {
		/*
		 * rows   height
		 * ---- = ------
		 * cols   width
		 * 
		 * cols = (rows * width) / height
		 */

		int curHeight = Screen.height;
		int curWidth = Screen.width;
		int cols = Mathf.CeilToInt((float)(rows * curWidth) / (float)curHeight);
		renderTex = new RenderTexture(cols * 2, rows * 2, 24);
		renderTex.filterMode = preFilter;

		foreach (Camera cam in cameras) {
			if (cam != null) {
				cam.targetTexture = renderTex;
			}
		}
	}

	void OnRenderImage (RenderTexture src, RenderTexture dst) {
		RenderTexture scaled = RenderTexture.GetTemporary (renderTex.width / 2, renderTex.height / 2);
		scaled.filterMode = postFilter;

		Graphics.Blit (renderTex, scaled);
		Graphics.Blit (scaled, dst);

		RenderTexture.ReleaseTemporary (scaled);
	}

}
