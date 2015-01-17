using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class score : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int nowscore;

	void resetScore(){
		nowscore = 0;
	}

	void addscore(int i){
		nowscore += i;
		gameObject.GetComponent<Text> ().text = nowscore.ToString ();
	}
}
