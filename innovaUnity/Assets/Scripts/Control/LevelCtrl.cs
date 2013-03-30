using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelCtrl : MonoBehaviour {
	
	private static LevelCtrl levelCtrl;
	public static LevelCtrl Instance{
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
	
	public GameObject objectGenerator;
	
//	public List<GameObject> backgrounds;
//	public List<GameObject> sky;
//	
//	public GameObject player;
//	public GameObject crowd;
	
	private float timeElapsed;
	
	void Awake(){
		levelCtrl = this;
	}
	
	void Start(){
//		for(int i = 0; i < sky.Count; i++){
//			Instantiate(sky[i], new Vector3(sky[i].transform.position.x + (i * skyLength), sky[i].transform.position.y + 20, sky[i].transform.position.z), 
//				sky[i].transform.rotation);
//		}
//		
//		for(int i = 0; i < backgrounds.Count; i++){
//				Instantiate(backgrounds[i], new Vector3(transform.position.x + (i * sceneryLength), 
//				(backgrounds[i].transform.position.y + sceneryHeight / 2.1f), backgrounds[i].transform.position.z), backgrounds[i].transform.rotation);
//		}
//		
//		Instantiate(player, player.transform.position, player.transform.rotation);
//
//		Instantiate(crowd, crowd.transform.position, crowd.transform.rotation);
		Instantiate(objectGenerator, new Vector3(transform.position.x + sceneryLength, objectGenerator.transform.position.y, 
			objectGenerator.transform.position.z), transform.rotation);
	}
	
	void Update(){
		if(Time.time >= 40f){
			Debug.Log("Se acabo'sta monda");
		}
	}
}