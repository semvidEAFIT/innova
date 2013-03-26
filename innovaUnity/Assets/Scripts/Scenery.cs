using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scenery : MonoBehaviour {
	
	private float speed;
	public float maxSpeed;
	
	public float sceneryLength;
	
	private int timesSwapped;
	public int maxSwapsBeforeObstacles;
	
	public List<GameObject> obstacles;
	private GameObject current;
	
	// Use this for initialization
	void Start () {
		speed = 1f;
		timesSwapped = 0;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(speed, 0, 0));
		if(speed < maxSpeed){
			speed += speed / 100;
		}
		if(transform.position.x < -sceneryLength - 10){
			transform.Translate(new Vector3(-3 * sceneryLength, 0, 0));
			timesSwapped += 1;
			if((obstacles.Count > 0) && (timesSwapped > maxSwapsBeforeObstacles)){
				createObstacle();
			}
		}
		if(!current.Equals(null)){
			current.transform.Translate(speed, 0, 0);
		}
	}
	
	void createObstacle(){
		int r = Mathf.RoundToInt(Random.Range(0, obstacles.Count));
		for(int i = 0; i < obstacles.Count; i++){
			if(i == r){
				current = Instantiate(obstacles[i], new Vector3(transform.position.x, 
					obstacles[i].transform.position.y, transform.position.z - 1), obstacles[i].transform.rotation) as GameObject;
			}
		}
	}
}
