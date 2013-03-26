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
	
	public GameObject player;
	private GameObject crowd;
	
	public List<GameObject> backgrounds;
	public float sceneryLength;
	
	private float timeElapsed;
	
	void Start(){
		for(int i = 0; i < backgrounds.Count; i++){
				Instantiate(backgrounds[i], new Vector3(transform.position.x + (i *sceneryLength), 
				(transform.position.y + sceneryLength / 2), transform.position.z), backgrounds[i].transform.rotation);
		}
		Instantiate(player, player.transform.position, player.transform.rotation);
//		Instantiate(crowd, crowd.transform.position, crowd.transform.rotation);
	}
	
	void Update(){
		
	}
}
