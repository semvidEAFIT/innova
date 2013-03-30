using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectGenerator : MonoBehaviour{
	
	public List<GameObject> obstacles;
	public GameObject segwayGO;
	private List<GameObject> current;
	
	public float sceneryLength;
	public float sceneryHeight;
	
	public float skyLength;
	
	public float gameSpeed;
	public float maxGameSpeed;
	
	public List<GameObject> backgrounds;
	public List<GameObject> sky;
	
	public GameObject floor;
	
	public GameObject player;
	public GameObject crowd;
	
	private float iniTime;
	
	private float distanceRun;
		
	private bool segwayUsed;
	
	void Start(){
		distanceRun = 0f;
		
		iniTime = 0f;
		current = new List<GameObject>();
		
		segwayUsed = false;
		
		Instantiate(player, player.transform.position, player.transform.rotation);

		Instantiate(crowd, crowd.transform.position, crowd.transform.rotation);
		
		Instantiate(floor, floor.transform.position, floor.transform.rotation);
		
		for(int i = 0; i < sky.Count; i++){
			Instantiate(sky[i], new Vector3(sky[i].transform.position.x + (i * skyLength), sky[i].transform.position.y + 20, sky[i].transform.position.z), 
				sky[i].transform.rotation);
		}
		
		for(int i = 0; i < backgrounds.Count; i++){
				Instantiate(backgrounds[i], new Vector3(transform.position.x + (i * sceneryLength), 
				(backgrounds[i].transform.position.y + sceneryHeight / 1.8f), backgrounds[i].transform.position.z), backgrounds[i].transform.rotation);
		}
		
	}
	
	void Update(){
		distanceRun = Time.time * gameSpeed;
		
		if(Time.time - iniTime >= 1f){
			createObstacles();
			iniTime = Time.time;
		}
		
		//CAMBIAR "20" A UNA VARIABLE
		if(distanceRun >= 2 && !segwayUsed){
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
		//if(Mathf.RoundToInt(Random.Range(0, 5)) <= 2){
			Instantiate(segwayGO, new Vector3(transform.position.x + sceneryLength, transform.position.y, segwayGO.transform.position.z), 
				segwayGO.transform.rotation);
		//}
		segwayUsed = true;
	}
}