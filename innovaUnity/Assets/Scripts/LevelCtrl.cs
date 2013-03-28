using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelCtrl : MonoBehaviour {
	
	private LevelCtrl levelCtrl;
	public LevelCtrl Instance{
		get{
			if(levelCtrl.Equals(null)){
				levelCtrl = new LevelCtrl();
			}
			return levelCtrl;
		}
	}
		
	public float sceneryLength;
	public float sceneryHeight;
	
	public float skyLength;
	
	public float gameSpeed;
	public float maxGameSpeed;
	
	public GameObject obstacleGenerator;
	
	public List<GameObject> backgrounds;
	public List<GameObject> sky;
	
	public GameObject player;
	public GameObject crowd;
	
	private float timeElapsed;
	
	void Awake(){
		levelCtrl = this;
	}
	
	void Start(){
		for(int i = 0; i < sky.Count; i++){
			Instantiate(sky[i], new Vector3(sky[i].transform.position.x + (i * skyLength), sky[i].transform.position.y + 20, sky[i].transform.position.z), 
				sky[i].transform.rotation);
		}
		
		for(int i = 0; i < backgrounds.Count; i++){
				Instantiate(backgrounds[i], new Vector3(transform.position.x + (i * sceneryLength), 
				(transform.position.y + sceneryHeight / 2.1f), transform.position.z), backgrounds[i].transform.rotation);
		}
		Instantiate(player, player.transform.position, player.transform.rotation);

		Instantiate(crowd, crowd.transform.position, crowd.transform.rotation);
		Instantiate(obstacleGenerator, new Vector3(transform.position.x + sceneryLength, obstacleGenerator.transform.position.y, 
			obstacleGenerator.transform.position.z), player.transform.rotation);

	}
	
	void Update(){
		if(Time.time >= 40f){
			Debug.Log("Se acabó'sta mondá");
		}
	}
}
