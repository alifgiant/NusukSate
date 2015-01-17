using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class gameScript : MonoBehaviour {

	//object sate
	public GameObject[] source;
	public GameObject latarEs;

	//status is playing or not
	public bool played = false;

	//time passed conter
	float timer = 0.01f;

	//time pointer when to spawn object
	float scale = 0.0f;


	public float slowingSpeed = 1f;

	public float slowingTime = 2f;

	float slowingTimer = 0;

	public bool slowed = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (played) {
			if (timer>scale) {
				createGuy(source[Random.Range(0,8)],Random.Range(-4,4),11,0);
				if (slowed) {
					scale+=Random.Range(0.7f, 1.6f)*slowingTime+2f;
				}else{
					scale+=Random.Range(0.7f, 1.6f);
				}			
			}	
			timer+=Time.deltaTime;
		}
		else {
			timer = 0.01f;
			scale = 0.0f;
		}

		if (slowingTimer>0) {
			slowingTimer -= Time.deltaTime;	
		}else if ((slowingTimer == 0 || slowingTimer < 0) && slowed){
			slowingTimer = 0;
			slowed = false;
		}

		//over ride andorid back button
		if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
	}

	
	public void createGuy(GameObject guy, float x, float y, float z){
		//Instantiate (guy, new Vector3 (x, y, z), Quaternion.identity);
		Instantiate (guy, new Vector3 (x, y, z), Quaternion.identity);
	}
	
	int getHighScore(){
		return PlayerPrefs.GetInt("highscore", 0);   
	}
	
	void StoreHighscore(int newHighscore)
	{
		int oldHighscore = getHighScore();    
		if(newHighscore > oldHighscore)
			PlayerPrefs.SetInt("highscore", newHighscore);
	}

	//only once called, only when first time to play
	public void playgame(){
		GameObject.Find ("scores").GetComponent<Text> ().enabled = true;
		GameObject.Find ("heart").GetComponent<Image> ().enabled = true;
		resetGame ();
	}

	public void setIceBackground(){
		if (!slowed) {
			createGuy (latarEs, 0, 0, 0);
		}

		slowed = true;
		slowingTimer = slowingTime;
	}

	//called every time reset pressed
	public void resetGame(){
		//audio.clip = play;
		//audio.Play ();
		GameObject scoreBoard = GameObject.Find ("scores");
		scoreBoard.GetComponent<Text> ().text = "0";
		scoreBoard.SendMessage ("resetScore");
		Transform locTusuk = GameObject.Find ("tusukSate").transform;
		locTusuk.position = new Vector3 (0,locTusuk.position.y,locTusuk.position.z);

		destroyAllGuy ("baik");
		destroyAllGuy ("gosong");
		destroyAllGuy ("bom");
		destroyAllGuy ("es");

		played = true;
		slowed = false;
		showEndMenu (false);
	}

	//called when losing condition is reached	
	void gameover(){
		played = false;
		int high = GameObject.Find ("scores").GetComponent<score> ().nowscore;
		StoreHighscore (high);
		//audio.clip = over;
		//audio.Play ();
		showEndMenu (true);
	}

	//destroy every object with tag	
	void destroyAllGuy(string tag){
		GameObject [] player = GameObject.FindGameObjectsWithTag (tag);
		foreach (var item in player) {
			//item.SendMessage("destroy")	;
			GameObject.Destroy((GameObject)item);
		}
	}

	void showEndMenu(bool stat){
		GameObject reset =  GameObject.Find("reset");
		reset.GetComponent<Image> ().enabled = stat;
		reset.GetComponent<Button> ().enabled = stat;
		
		GameObject share = GameObject.Find ("share");
		share.GetComponent<Image> ().enabled = stat;
		share.GetComponent<Button> ().enabled = stat;
		
		GameObject rate = GameObject.Find("rate");
		rate.GetComponent<Image> ().enabled = stat;
		rate.GetComponent<Button> ().enabled = stat;
		
		GameObject highScore = GameObject.Find ("highScores");
		highScore.GetComponent<Text> ().enabled = stat;
		if (stat) {
			highScore.GetComponent<Text> ().text = getHighScore ().ToString ();	
		}
	}	

	const string AppId = "1513862295562201";
	const string ShareUrl = "https://www.facebook.com/dialog/feed";
	
	public void shareBut(){
		//AndroidJNI.
		Share ("http://bit.ly/1tGb1TR", "http://www.mediafire.com/convkey/ad70/4737bbnajaccb7yzg.jpg", "Hey let's play Nusuk Sate", "A new game by Rantau tak pulang", "My high score is " + getHighScore().ToString(), "https://www.facebook.com");
		
	}
	
	void Share(string link, string pictureLink, string name,string caption, string description, string redirectUri)
	{
		Application.OpenURL (ShareUrl+"?app_id="+ AppId +"&picture="+WWW.EscapeURL(pictureLink)+"&description="+ WWW.EscapeURL(description) +"&link=" + WWW.EscapeURL( link )+"&name=" + WWW.EscapeURL(name) +"&redirect_uri="+ WWW.EscapeURL(redirectUri));
	}
}
