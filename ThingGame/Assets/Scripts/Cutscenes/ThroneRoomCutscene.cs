using UnityEngine;
using System.Collections;

public class ThroneRoomCutscene : MonoBehaviour {

  public GameObject staffPosition;
  public Animator staffPositionAnimator;
  public CameraMatch mainCamera;
	public GameObject cutsceneStaff;
  public AutoPortal portal;
  public bool playerStaffVisible = true;
  public bool cutsceneStaffVisible = false;
  public DPek.Raconteur.RenPy.Display.RenPyDisplay thingNarration;
	public Transform thingAudioLocation;

  void Update() {
		GameObject helpers =
			GameObject.Find(thingNarration.gameObject.name + " Helpers");
		helpers.transform.position = thingAudioLocation.position;

    staffPosition.SetActive(playerStaffVisible);
    cutsceneStaff.SetActive(cutsceneStaffVisible);
  }

	void MakeStaffFly() {
		staffPositionAnimator.SetTrigger("fly");
		cutsceneStaff.SetActive(true);
	}

  public void BeginCutscene() {
    GetComponent<Animator>().SetBool("begin", true);
  }

  void MatchMainCam() {
    mainCamera.MatchWorld(camera, 2.0f);
  }

  void GoToBossBattle() {
    portal.TriggerManually();
  }

  void BeginDialogue() {
    thingNarration.StartDialog();
  }

}
