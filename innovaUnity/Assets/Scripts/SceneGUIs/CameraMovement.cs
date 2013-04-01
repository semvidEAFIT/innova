using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	
	public GameObject[] slides;
	public float timeToChange;
	
	private float timeElapsed;
	private int curSlide;
	
	private bool firstTime;
	
	// Use this for initialization
	void Start () {
		timeElapsed=0;
		curSlide=0;
		firstTime=true;
	}
	
	// Update is called once per frame
	void Update () {
		timeElapsed+=Time.deltaTime;
		if (Input.anyKeyDown || timeElapsed>=timeToChange){
			if (curSlide < slides.Length){
				timeElapsed=0;
				NextSlide();
			} else {
				//change scene
				Application.LoadLevel("CharacterSelection");
			}
		}
	}
	
	private void NextSlide(){
		if (firstTime) {
			camera.orthographicSize=10;
			firstTime=false;
		}
		if (curSlide==3){
			camera.orthographicSize=12;
			transform.position = new Vector3 (slides[curSlide].transform.position.x, slides[curSlide].transform.position.y+1, 0) ;
		} else {
			transform.position = new Vector3 (slides[curSlide].transform.position.x, slides[curSlide].transform.position.y + 3, 0) ;
		}
			
		curSlide++;
	}
}
