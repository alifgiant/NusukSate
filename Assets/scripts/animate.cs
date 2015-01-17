using UnityEngine;
using System.Collections;

public class animate : MonoBehaviour {

	//variable to hold speed par
	public float speed;
	
	private GameObject tusuk;

	public bool pinned = false;

	float slowingFac = 1;

	public GameObject sate;

	gameScript cameraScript;


	// Use this for initialization
	void Start () {
		int level = (GameObject.Find ("scores").GetComponent<score> ().nowscore/7)+1;
		speed = Random.Range (-5, -2)+level;
		if (speed == 0)
						speed = -2;
		cameraScript = GameObject.Find ("Main Camera").GetComponent<gameScript> ();
		tusuk = GameObject.Find("tusukSate");
	}

	// Update is called once per frame
	void Update () {

		bool played = cameraScript.played;

		if (played) {
			if (cameraScript.slowed) {
				slowingFac = cameraScript.slowingSpeed;
			}else{
				slowingFac = 1;
			}

			transform.Translate (0, speed * Time.deltaTime / slowingFac, 0);

			if (pinned) {
				transform.position = new Vector3( tusuk.transform.position.x - jarakTusuk, transform.position.y, transform.position.z);
			}
		}
	}

	float jarakTusuk = 0;
	//called every collision on trigger
	void OnTriggerEnter2D(Collider2D other){
		Debug.Log("collide");
		if (other.gameObject.tag == "tusuk") {
			audio.Play();
			//offset = this.transform.position - other.gameObject.GetComponent<dragSate>().v3;
			pinned = true;
			jarakTusuk = other.transform.position.x - this.transform.position.x;
			if (this.gameObject.tag == "baik") {
				//normal = false;
				//audio.Play ();
				GameObject.Find ("scores").SendMessage ("addscore", 1);
			} else if (this.gameObject.tag == "gosong"){
				GameObject.Find ("scores").SendMessage ("addscore", -1);
			} else if (this.gameObject.tag == "es"){
				//GameObject.Find ("Main Camera").GetComponent<gameScript> ().slowing = 3f;
				cameraScript.createGuy(sate, this.transform.position.x, this.transform.position.y, this.transform.position.z);
				cameraScript.setIceBackground();
				Destroy(this.gameObject);
			} else if (this.gameObject.tag == "bom"){
				GameObject.Find ("Main Camera").SendMessage ("gameover");
			}
		} else if (other.gameObject.tag == "end") {
			if (this.gameObject.tag == "baik" && !pinned) {
				GameObject.Find ("Main Camera").SendMessage ("gameover");
			} else {
				GameObject.Destroy (this.gameObject);
			}
		} else if (other.gameObject.tag == "coution" && this.gameObject.tag == "bad") {
			//transform.FindChild("attention").GetComponent<SpriteRenderer>().enabled = true;
		}
	}

	void destroyThisObject(){
		GameObject.Destroy (gameObject);
	}

}
