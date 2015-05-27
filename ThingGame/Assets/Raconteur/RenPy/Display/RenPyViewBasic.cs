using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using DPek.Raconteur.RenPy.Display;
using DPek.Raconteur.RenPy.Parser;
using DPek.Raconteur.RenPy.Script;
using DPek.Raconteur.RenPy.State;
using System.Collections;

namespace DPek.Raconteur.RenPy
{
	public class RenPyViewBasic : MonoBehaviour
	{
		public bool m_autoStart;
		public RenPyDisplay m_display;
		public int m_lineNum;

		public InteractableObject m_interact;
		public string m_label;

		public Text speakerText;
		public Text dialogueText;
		public GameObject skipArea;

		private GameObject skipTemp;

		private bool skipDialougue;
		private bool withAudio;

		public void Start()
		{
			withAudio = false;
			m_label = "";
			if (m_autoStart) {
				m_display.StartDialog();
			}
		}

		void Update()
		{
			if (!m_display.Running) {
				m_display.StopDialog();
				return;
			}

			if (m_display.GetCurrentStatement() == null) {
				m_display.NextStatement();
			}

			RenPyStatementType mode = m_display.GetCurrentStatement().Type;

			switch (mode) {
			case RenPyStatementType.SAY:
				// Check for input to go to next line
				float wait_time = 0;
				float elapsed_time = 0;

				if(m_display.m_voice.m_source.isPlaying){
					withAudio = true;
					wait_time = m_display.m_voice.m_source.clip.length;
					elapsed_time = m_display.m_voice.m_source.time;
				}
				if(withAudio){
					if (wait_time <= elapsed_time || (skipArea != null
					                              && ( Input.GetKeyDown (KeyCode.Tab)))){
						if(m_display.m_voice.m_source.isPlaying){
							m_display.m_voice.m_source.Stop ();
						}
						m_lineNum++;
						m_display.NextStatement();
						withAudio = false;
					}
				}
				//Audio free linej
				/*if (skipArea != null && Input.GetKeyDown (KeyCode.LeftShift)) {
						m_lineNum++;
						//Sync Text with audio
						if(m_display.m_voice.m_source.isPlaying){
							m_display.m_voice.m_source.Stop ();
						}
						m_display.NextStatement();
						withAudio = false;
				}*/
				break;

			case RenPyStatementType.PAUSE:
				var pause = m_display.GetCurrentStatement() as RenPyPause;
				if (pause.WaitForInput) {
						m_display.NextStatement();
				}
				// Or wait until we can go to the next line
				else {
					StartCoroutine(WaitNextStatement(pause.WaitTime));
				}
				break;
			case RenPyStatementType.MENU:
				// Do nothing
				break;
			case RenPyStatementType.LABEL:
				var label = m_display.GetCurrentStatement() as RenPyLabel;
				m_label = label.Name;

				if(m_label == "end"){
					if(m_interact != null && GlobalVariables.gameLevel != Scenes.hubWorld){
						m_interact.enabled = true;
					}
				}
				m_display.NextStatement();
				break;
			default:
				// Show nothing for this line, proceed to the next one.
				m_display.NextStatement();
				Update(); // Update immediately to prevent delay
				break;
			}
			var currentStatement = m_display.GetCurrentStatement();
			if (currentStatement == null || currentStatement.Type != RenPyStatementType.SAY) {
				speakerText.text = "";
				dialogueText.text = "";
				if(skipArea != null){
					skipArea.SetActive(false);
				}
			} else{
				var speech = m_display.GetCurrentStatement() as RenPySay;

				RenPyCharacterData speaker = m_display.GetSpeaker(speech);
				speakerText.color = speaker.Color;
				speakerText.text = speaker.Name;
				dialogueText.text = speech.Text;
				if(skipArea != null){
					skipArea.SetActive(true);
				}
			}
		}

		public void ClearDialogArea() {
			speakerText.text = "";
			dialogueText.text = "";
			if (skipArea != null) {
				skipArea.SetActive(false);
			}
			if (m_display.m_voice.m_source.isPlaying) {
				m_display.m_voice.m_source.Stop();
			}
		}

		/*void OnGUI()
		{
			if (!m_display.Running || m_display.GetCurrentStatement() == null) {
				return;
			}

			Rect rect;
			GUIStyle style = new GUIStyle();
			style.alignment = TextAnchor.MiddleCenter;
			style.normal.textColor = Color.white;
			style.fontSize = 15;
			style.wordWrap = true;

			// Draw background
			var bg = m_display.GetBackgroundImage();
			if (bg != null) {
				var pos = new Rect(0, 0, Screen.width, Screen.height);
				GUI.DrawTexture(pos, bg.Texture, ScaleMode.ScaleAndCrop);
			}

			// Draw images
			var images = m_display.GetImages();
			foreach (RenPyImageData image in images) {
				float screenWidth = Screen.width;
				float screenHeight = Screen.height;
				float texWidth = image.Texture.width;
				float texHeight = image.Texture.height;
				var pos = new Rect(0, 0, texWidth, texHeight);
				switch(image.Alignment) {
					case Util.RenPyAlignment.BottomCenter:
						pos.x = screenWidth / 2 - texWidth / 2;
						pos.y = screenHeight - texHeight;
						break;
					case Util.RenPyAlignment.BottomLeft:
						pos.x = 0;
						pos.y = screenHeight - texHeight;
						break;
					case Util.RenPyAlignment.BottomRight:
						pos.x = screenWidth - texWidth;
						pos.y = screenHeight - texHeight;
						break;
					case Util.RenPyAlignment.Center:
						pos.x = screenWidth / 2 - texWidth / 2;
						pos.y = screenHeight / 2 - texHeight / 2;
						break;
					case Util.RenPyAlignment.LeftCenter:
						pos.x = 0;
						pos.y = screenHeight / 2 - texHeight / 2;
						break;
					case Util.RenPyAlignment.RightCenter:
						pos.x = screenHeight - texWidth;
						pos.y = screenHeight / 2 - texHeight / 2;
						break;
					case Util.RenPyAlignment.TopCenter:
						pos.x = screenWidth / 2 - texWidth / 2;
						pos.y = 0;
						break;
					case Util.RenPyAlignment.TopLeft:
						pos.x = 0;
						pos.y = 0;
						break;
					case Util.RenPyAlignment.TopRight:
						pos.x = screenHeight - texWidth;
						pos.y = 0;
						break;
				}
				GUI.DrawTexture(pos, image.Texture, ScaleMode.ScaleToFit);
			}

			// Draw the window if needed
			if (m_display.ShouldDrawWindow()) {
				DrawBox(50, Screen.height - 200, Screen.width - 100, 200);
			}

			// Draw text
			switch (m_display.GetCurrentStatement().Type) {
				case RenPyStatementType.SAY:
					var speech = m_display.GetCurrentStatement() as RenPySay;

					// Render the speaker
					int y = Screen.height - 200;
					int width = Screen.width - 100;
					rect = new Rect(50, y, width, 200);
					style.alignment = TextAnchor.UpperLeft;
					RenPyCharacterData speaker = m_display.GetSpeaker(speech);
					var oldColor = style.normal.textColor;
					style.normal.textColor = speaker.Color;
					GUI.Label(rect, speaker.Name, style);
					style.normal.textColor = oldColor;

					// Render the speech
					style.alignment = TextAnchor.MiddleCenter;
					rect = new Rect(50, y + 50, width, 100);
					GUI.Label(rect, speech.Text, style);
					break;

				case RenPyStatementType.MENU:
					var menu = m_display.GetCurrentStatement() as RenPyMenu;

					// Display the choices
					int height = 30;
					int numChoices = menu.GetChoices().Count;
					int yPos = Mathf.Max(0, Screen.height/2 - numChoices*height);
					rect = new Rect(100, yPos, Screen.width-200, height);
					foreach (var choice in menu.GetChoices()) {

						// Check if a choice was selected
						DrawBox(100, rect.y + 5, rect.width, rect.height - 10);
						if (GUI.Button(rect, choice, style)) {
							m_display.PickChoice(menu, choice);
							m_display.NextStatement();
						}

						rect.y += height;
					}
					break;
			}
		}
		*/

		bool waiting = false;
		private IEnumerator WaitNextStatement(float time)
		{
			if (!waiting) {
				waiting = true;
				yield return new WaitForSeconds(time);
				m_display.NextStatement();
				waiting = false;
			}
		}

		private void DrawBox(float x, float y, float width, float height)
		{
			Rect rect = new Rect(x, y, width, height);
			Texture2D texture = new Texture2D(1, 1);
			texture.SetPixel(0, 0, new Color(0, 0, 0, 0.6f));
			texture.Apply();
			GUI.skin.box.normal.background = texture;
			GUI.Box(rect, GUIContent.none);
		}
	}
}
