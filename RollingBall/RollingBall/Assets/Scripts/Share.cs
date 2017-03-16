using UnityEngine;
using System.Collections;

public class Share : MonoBehaviour {

	string subject = "Rolling Ball";
	string body = "PLAY THIS GAME NOW";

	// Use this for initialization
	public void ShareApp() {
		AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject> ("currentActivity");
		currentActivity.Call ("shareText", subject, body);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
