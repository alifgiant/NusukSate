using UnityEngine;
using System.Collections;

public class slowing : MonoBehaviour {

	gameScript cameraScript;

	// Use this for initialization
	void Start () {
		cameraScript = GameObject.Find ("Main Camera").GetComponent<gameScript> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (!cameraScript.slowed && cameraScript.played) {
			Destroy(this.gameObject);
		}
	}
}
