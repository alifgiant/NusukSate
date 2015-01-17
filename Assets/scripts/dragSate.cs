using UnityEngine;
using System.Collections;

public class dragSate : MonoBehaviour {
	
	private float dist;
	private Transform toDrag;
	private bool dragging = false;
	private Vector3 offset;

	//var posisi sentuhan ke layar
	public Vector3 v3;

	private bool played;
	RuntimePlatform platform = Application.platform;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		played = GameObject.Find ("Main Camera").GetComponent<gameScript> ().played;
		//Debug.Log(played);
		if (played) {
			//android platform
			if(platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer){
				if (Input.GetKeyDown(KeyCode.Escape)) {
					Application.Quit(); 
				}
				
				if(Input.touchCount > 0) {
					if(Input.GetTouch(0).phase == TouchPhase.Began){
						checkTouch(Input.GetTouch(0).position.x);
					}
					if(Input.GetTouch(0).phase == TouchPhase.Moved){
						if (dragging) {
							checkDrag(Input.GetTouch(0).position.x);
						}
					}
					if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled) {					
						dragging = false;			
					}
				}
			}else{
				//If running game in editor
				//#if UNITY_EDITOR
				//If mouse button 0 is down
				if (Input.GetMouseButton (0)) {
					//Cache mouse position
					/*Vector2 mouseCache = Input.mousePosition; 
				 * touched = true;*/
					if (Input.GetMouseButtonDown (0)) {
						checkTouch(Input.mousePosition.x);
					}
					if (Input.GetMouseButton (0)) {
						if (dragging) {
							checkDrag(Input.mousePosition.x);						
						}
					}
				}
				if (Input.GetMouseButtonUp (0)) {					
					dragging = false;			
				}
				//Debug.LogError(dragging);
			}
			//#endif

		}
	}

	void checkTouch(float posX){
		toDrag = transform;
		dist = toDrag.position.z - Camera.main.transform.position.z;
		v3 = new Vector3 (posX, 0, dist);
		v3 = Camera.main.ScreenToWorldPoint (v3);
		offset = toDrag.position - v3;
		dragging = true;
	}
	
	void checkDrag(float posX){
		v3 = new Vector3 (posX, 0, dist);
		v3 = Camera.main.ScreenToWorldPoint (v3);
		toDrag.position = v3 + offset;
	}
}
