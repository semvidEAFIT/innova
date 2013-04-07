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
//	public float maxGameSpeed;
	
	public float distanceUntilSegway;
	
	public float timeToSpawn;
	private float deltaTimeToSpawn;
	
	public List<GameObject> backgrounds;
	public List<GameObject> sky;
	
	public List<GameObject> mountains;
	
	public GameObject floor;
	
	public GameObject playerBoy, playerGirl;
	public GameObject crowd;
	
	private float iniTime;
	
	private float distanceRun;
	
	private float nextX;
		
	private bool segwayUsed;
	
	private bool failed=false;
		
	void Start(){
		gameSpeed = LevelCtrl.Instance.gameSpeed;
		deltaTimeToSpawn = 0;
        if(Application.isWebPlayer){
//            gameSpeed *= 1.5f;
//            maxGameSpeed *= 1.5f;
        }
		distanceRun = 0f;
		iniTime = 0f;
		current = new List<GameObject>();
		
		segwayUsed = false;
		if(!CharacterSelection.IsBoy){
			Instantiate(playerGirl, playerGirl.transform.position, playerGirl.transform.rotation);
		}else{
			Instantiate(playerBoy, playerBoy.transform.position, playerBoy.transform.rotation);
		}
		crowd = Instantiate(crowd, crowd.transform.position, crowd.transform.rotation) as GameObject;
		crowd.transform.parent = this.transform;
		
		Instantiate(floor, floor.transform.position, floor.transform.rotation);
		
		for(int i = 0; i < sky.Count; i++){
			Instantiate(sky[i], new Vector3(sky[i].transform.position.x + (i * skyLength), sky[i].transform.position.y + 20, sky[i].transform.position.z), 
				sky[i].transform.rotation);
		}
		
		for(int i = 0; i < mountains.Count; i++){
			Instantiate(mountains[i], new Vector3(mountains[i].transform.position.x + (i * skyLength), mountains[i].transform.position.y + 29, mountains[i].transform.position.z), 
				mountains[i].transform.rotation);
		}
		
		for(int i = 0; i < backgrounds.Count; i++){
			backgrounds[i] = Instantiate(backgrounds[i], new Vector3(transform.position.x + (i * sceneryLength), 
				(backgrounds[i].transform.position.y + sceneryHeight / 1.8f), backgrounds[i].transform.position.z), 
				backgrounds[i].transform.rotation) as GameObject;
			backgrounds[i].transform.parent = this.transform;
		}
		
//		Instantiate(backgrounds[backgrounds.Count - 1], new Vector3(transform.position.x + ((backgrounds.Count - 1) * sceneryLength), 
//			(backgrounds[backgrounds.Count - 1].transform.position.y + sceneryHeight / 1.8f), backgrounds[backgrounds.Count - 1].transform.position.z),
//			backgrounds[backgrounds.Count - 1].transform.rotation);
	}
	
	void Update(){
		distanceRun = Time.time * gameSpeed * Time.deltaTime;
		
		if(Time.time - iniTime >= timeToSpawn * Time.deltaTime - deltaTimeToSpawn){
			nextX = Mathf.RoundToInt(Random.Range(transform.position.x - (sceneryLength / 4), sceneryLength)) + sceneryLength;
			Debug.Log("Obstaculo: " + nextX + "   Auditorio: " + backgrounds[backgrounds.Count - 1].transform.position.x + "   Diferencia: " + Mathf.Abs(nextX - backgrounds[backgrounds.Count - 1].transform.position.x));
			if (!failed && (Mathf.Abs(nextX - backgrounds[backgrounds.Count - 1].transform.position.x) > 0)){
				createObstacles(nextX);
				if(deltaTimeToSpawn < timeToSpawn + 1.6f){
					deltaTimeToSpawn += 1.6f * Time.deltaTime;
				}
			}
			iniTime = Time.time;
		}
		
		//CAMBIAR "20" A UNA VARIABLE
		if(distanceRun >= distanceUntilSegway && !segwayUsed){
			createSegway();
		}
	}
	
	void createObstacles(float x){
		int r = Mathf.RoundToInt(Random.Range(0, obstacles.Count));
		for(int i = 0; i < obstacles.Count; i++){
			if(i == r){
				current.Add(Instantiate(obstacles[i], new Vector3(x, 
					transform.position.y, obstacles[1].transform.position.z), 
					obstacles[i].transform.rotation) as GameObject);
				current[current.Count - 1].gameObject.transform.parent = this.gameObject.transform;
			}
		}
	}
	
	void createSegway(){
		if(Mathf.RoundToInt(Random.Range(0, 5)) <= 2){
			Instantiate(segwayGO, new Vector3(transform.position.x + sceneryLength, transform.position.y, segwayGO.transform.position.z), 
				segwayGO.transform.rotation);
		}
		segwayUsed = true;
	}
	
	public void StopObstacles(){
		failed=true;
		foreach (Transform child in transform){
			if (child.tag!="Crowd"){
				if (child.tag == "Obstacle")
					child.GetComponent<Obstacle>().setStopped(true);
				else child.GetComponent<Scenery>().setStopped(true);
			}
		}
	}
}