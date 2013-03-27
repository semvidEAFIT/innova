using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleGenerator : MonoBehaviour{
	
	public List<GameObject> obstacles;
	private List<GameObject> current;
	
	private float timeElapsed;
	
	private float sceneryLength;
	
	void Start(){
		timeElapsed = 0f;
		current = new List<GameObject>();
		sceneryLength = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelCtrl>().sceneryLength;
	}
	
	void Update(){
		timeElapsed += Time.deltaTime;
		if(timeElapsed >= 0.7f){
			createObstacles();
			timeElapsed = 0f;
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
}
