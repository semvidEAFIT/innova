using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scenery : MonoBehaviour {
	
	public float speed;
	
	public float accelerationRate;
	
	private float maxSpeed;
	
//	private float sceneryLength;
	
	private int timesSwapped;
	
//	private int buildingCount;
	
	private bool stopped;
	
//	private GameObject gameControl;
//	public int maxSwapsBeforeObstacles;
//	
//	public List<GameObject> obstacles;
//	private List<GameObject> current;
	
//	private float timeElapsed;
	
	// Use this for initialization
	void Start () {
		accelerationRate = LevelCtrl.Instance.accelerationRate;
//		gameControl = GameObject.FindGameObjectWithTag("GameController");
		speed = LevelCtrl.Instance.gameSpeed * 0.9f;
		maxSpeed = LevelCtrl.Instance.maxGameSpeed * 0.9f;
		stopped = false;
//		sceneryLength = LevelCtrl.Instance.sceneryLength;
//		buildingCount = gameControl.GetComponent<LevelCtrl>().objectGenerator.GetComponent<ObjectGenerator>().backgrounds.Count;
//		timesSwapped = 0;
//		current = new List<GameObject>();
//		timeElapsed = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(!stopped){
			transform.Translate(new Vector3(speed, 0, 0));
			if(speed < maxSpeed){
				//speed += accelerationRate * Time.deltaTime * 0.9f;
				speed = LevelCtrl.Instance.gameSpeed  * Time.deltaTime;
			}
		}
		
        //if(transform.position.x < -sceneryLength - 10){
        //    transform.Translate(new Vector3(-buildingCount * sceneryLength, 0, 0));
        //}
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
	
	public void setStopped(bool stop){
		stopped = stop;
		
	}
}