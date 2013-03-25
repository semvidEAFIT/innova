using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Landscape : MonoBehaviour {
	
	public List<GameObject> backgrounds;
//	private float speed;
//	public float maxSpeed;
	public float sceneryLength;
	
	// Use this for initialization
	void Start () {
		for(int i = 0; i < backgrounds.Count; i++){
				Instantiate(backgrounds[i], new Vector3(transform.position.x + (i *sceneryLength), 
				(transform.position.y + sceneryLength / 2), transform.position.z), backgrounds[i].transform.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
//		if(speed < maxSpeed){
//			foreach(GameObject go in backgrounds){
//				go.transform.Translate(new Vector3(speed, 0, 0));
//				if(go.transform.position.x < -sceneryLength - 10){
//					go.transform.Translate(new Vector3(-(backgrounds.Count) * sceneryLength, 0, 0));
//				}
//			}
//			speed += speed / 100;
//		}
	}
}
