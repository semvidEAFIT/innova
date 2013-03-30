using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scenery : MonoBehaviour {
	
	private float speed;
	private float maxSpeed;
	
	private float sceneryLength;
	
	private int timesSwapped;
	
	private int buildingCount;
	
	private GameObject gameControl;
//	public int maxSwapsBeforeObstacles;
//	
//	public List<GameObject> obstacles;
//	private List<GameObject> current;
	
//	private float timeElapsed;
	
	// Use this for initialization
	void Start () {
		gameControl = GameObject.FindGameObjectWithTag("GameController");
		speed = gameControl.GetComponent<LevelCtrl>().gameSpeed * 0.8f;
		maxSpeed = gameControl.GetComponent<LevelCtrl>().maxGameSpeed * 0.9f;
		sceneryLength = gameControl.GetComponent<LevelCtrl>().sceneryLength;
		buildingCount = gameControl.GetComponent<LevelCtrl>().objectGenerator.GetComponent<ObjectGenerator>().backgrounds.Count;
//		timesSwapped = 0;
//		current = new List<GameObject>();
//		timeElapsed = 0;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(speed, 0, 0));
		if(speed < maxSpeed){
			speed += speed / 100;
		}
		if(transform.position.x < -sceneryLength - 10){
			transform.Translate(new Vector3(-buildingCount * sceneryLength, 0, 0));
		}
//		if(!current.Equals(null)){
//			current.transform.Translate(speed, 0, 0);
//		}
	}
	
//	void createObstacles(){
//		int r = Mathf.RoundToInt(Random.Range(0, obstacles.Count));
//		for(int i = 0; i < obstacles.Count; i++){
//			if(i == r){
//				current.Add(Instantiate(obstacles[i], new Vector3(transform.position.x, 
//					obstacles[i].transform.position.y, transform.position.z - 1), obstacles[i].transform.rotation) as GameObject);
//				current[current.Count - 1].gameObject.transform.parent = this.gameObject.transform;
//			}
//		}
//	}
}