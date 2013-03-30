using UnityEngine;
using System.Collections;

public class SkyMovement : MonoBehaviour {

	private float speed;
	private float skyLength;
	private float skyCount;
	
	private GameObject gameControl;
	
	void Start () {
		gameControl = GameObject.FindGameObjectWithTag("GameController");
		speed = gameControl.GetComponent<LevelCtrl>().gameSpeed * 0.2f;
		skyCount = gameControl.GetComponent<LevelCtrl>().objectGenerator.GetComponent<ObjectGenerator>().sky.Count;
		skyLength = gameControl.GetComponent<LevelCtrl>().skyLength;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(speed, 0, 0));
		if(transform.position.x < -skyLength - 10){
			transform.Translate(new Vector3(-skyCount * skyLength, 0, 0));
		}
	}
}