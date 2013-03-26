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
	public GameObject crowd;
<<<<<<< HEAD
=======
	public GameObject obstacleGenerator;
>>>>>>> ee11c628613dadbe2d13dc1b4024cdf7436f3d30
	
	public List<GameObject> backgrounds;
	
	public float sceneryLength;
	public float gameSpeed;
	public float maxGameSpeed;
	
	private float timeElapsed;
	
	void Awake(){
		levelCtrl = this;
	}
	
	void Start(){
		for(int i = 0; i < backgrounds.Count; i++){
				Instantiate(backgrounds[i], new Vector3(transform.position.x + (i *sceneryLength), 
				(transform.position.y + sceneryLength / 2), transform.position.z), backgrounds[i].transform.rotation);
		}
		Instantiate(player, player.transform.position, player.transform.rotation);
<<<<<<< HEAD
		Instantiate(crowd, crowd.transform.position, crowd.transform.rotation);
=======
//		Instantiate(crowd, crowd.transform.position, crowd.transform.rotation);
		Instantiate(obstacleGenerator, obstacleGenerator.transform.position, player.transform.rotation);
>>>>>>> ee11c628613dadbe2d13dc1b4024cdf7436f3d30
	}
	
	void Update(){
		
	}
}
