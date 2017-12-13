using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class WallHandler : MonoBehaviour, ITrackableEventHandler {

	private TrackableBehaviour mTrackableBehaviour;
	private GameController gameCtrl;
	public bool cannotstart = false;

	// Use this for initialization
	void Start () {
		

		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}

		gameCtrl = GameObject.Find("GameController").gameObject.GetComponent<GameController> ();
	}

	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		GameObject.Find ("Wall").GetComponent<MeshRenderer> ().enabled = false;
		GameObject.Find ("Ground").GetComponent<MeshRenderer> ().enabled = false;
		if (cannotstart)
			return;
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED)
		{
			gameCtrl.StartGame ();
			cannotstart = true;
		}
	}
}
