using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectGenerator : MonoBehaviour{
	
	public List<GameObject> obstacles;
	public GameObject segwayGO;
	private List<GameObject> current;
	
	private float iniTime;
	
	private float distanceRun;
	private float speed;
	
	private float sceneryLength;
	
	private bool segway, segwayUsed;
	
	void Start(){
		distanceRun = 0f;
		speed = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelCtrl>().maxGameSpeed;
		
		iniTime = 0f;
		current = new List<GameObject>();
		sceneryLength = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelCtrl>().sceneryLength;
		
		segwayUsed = false;
		
	}
	
	void Update(){
		distanceRun = Time.time * speed;
		
		if(Time.time - iniTime >= 1f){
			createObstacles();
			iniTime = Time.time;
		}
		
		//CAMBIAR "20" A UNA VARIABLE
		if(distanceRun >= 20 && !segwayUsed){
			Debug.Log("Se metió.");
			createSegway();
		}
	}
	
	void createObstacles(){
		int r = Mathf.RoundToInt(Random.Range(0, obstacles.Count * 2));
		for(int i = 0; i < obstacles.Count; i++){
			if(i == r){
				current.Add(Instantiate(obstacles[i], new Vector3(Mathf.RoundToInt(Random.Range(
					transform.position.x - (sceneryLength / 2), sceneryLength)) + sceneryLength, 
					transform.position.y, obstacles[1].transform.position.z), 
					obstacles[i].transform.rotation) as GameObject);
				current[current.Count - 1].gameObject.transform.parent = this.gameObject.transform;
			}
		}
	}
	
	void createSegway(){
		if(Mathf.RoundToInt(Random.Range(0, 5)) <= 2){
			Instantiate(segwayGO, new Vector3(transform.position.x + sceneryLength, transform.position.y, transform.position.z), 
				segwayGO.transform.rotation);
		}
		segwayUsed = true;
	}
}