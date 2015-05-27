using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class Telemetry {

	System.DateTime timer;
	bool running = false;
	int endChoice = -1;
	bool poofGuard = false;
	int simonSaysErrors = 0;
	int machineActivations = 0;
	int deaths = 0;
	bool voicemailSaved = false;

#if UNITY_EDITOR
	const int category = 1;
#else
	const int category = 0;
#endif

	const string POST_ENDPOINT =
		"http://thingchoice.appspot.com/telemetry";

	public void BeginGameTimer() {
		if (!running) {
			running = true;
			timer = System.DateTime.UtcNow;
		}
	}

	public void PlayerChoseFrinchfry() {
		BeginGameTimer();
		endChoice = 0;
	}

	public void PlayerChoseTheThing() {
		BeginGameTimer();
		endChoice = 1;
	}

	public void IncrementDeaths() {
		BeginGameTimer();
		deaths++;
	}

	public void IncrementSimonSaysErrors() {
		BeginGameTimer();
		simonSaysErrors++;
	}

	public void PlayerPoofedCastleGuard() {
		BeginGameTimer();
		poofGuard = true;
	}

	public void IncrementMachineActivations() {
		BeginGameTimer();
		machineActivations++;
	}

	public void PlayerSavedVoicemail() {
		BeginGameTimer();
		voicemailSaved = true;
	}

	public void PlayerDeletedVoicemail() {
		BeginGameTimer();
		voicemailSaved = false;
	}

	public void SendTelemetry() {
		if (!running) {
			return;
		}

		string json = GetJson();
		byte[] postData = System.Text.Encoding.UTF8.GetBytes(json.ToCharArray());

		new WWW(POST_ENDPOINT, postData);

		// Reset all the telemetry data.
		running = false;
		endChoice = -1;
		poofGuard = false;
		simonSaysErrors = 0;
		machineActivations = 0;
		voicemailSaved = false;
		deaths = 0;
	}

	string GetJson() {
		System.TimeSpan timeDiff = System.DateTime.UtcNow - timer;
		int timeSeconds = System.Convert.ToInt32(timeDiff.TotalSeconds);

		var dict = new Dictionary<string, object>();
		dict["endChoice"] = endChoice;
		dict["timeSeconds"] = timeSeconds;
		dict["category"] = category;
		dict["poofGuard"] = poofGuard;
		dict["deaths"] = deaths;
		dict["simonSaysErrors"] = simonSaysErrors;
		dict["machineActivations"] = machineActivations;
		dict["voicemailSaved"] = voicemailSaved;

		return Json.Serialize(dict);
	}

}
