using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	private int coinsCollected;
	private int timesTripped;
	private float move;
	private int countCrashed;
	private float speed; 
	// Use this for initialization
	void Start () {
		move = 0.0001f;
		countCrashed = 0;
		speed = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelCtrl>().maxGameSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space") && transform.position.y > -10){
			rigidbody.AddForce(Vector3.up * 10000);
		}
		transform.Translate(move, 0, 0);
		move *= -1;
		if(countCrashed > 2){
			
			Crowd c=(Crowd)GameObject.Find("Girl(Clone)").GetComponent("Crowd");
			c.accelerateCrowd();
		}
		while(transform.position.x > posX-15){
				transform.Translate(0.01f, 0, 0);
			}
	}
	
	void OnTriggerEnter(Collider c){
		
		if (c.tag == "Obstacle"){
			//destroy obstacle, reset jumpcounter, get closer to crowd
			countCrashed++;
			float posX = transform.position.x;
			
			
			
			
		}
		if (c.tag == "Segway"){
			//get on segway
		}
		if (c.tag == "Crowd"){
			//die?
			c.gameObject.GetComponent<Crowd>().accelerateCrowd();
		}
	}
	
	void OnTriggerExit(Collider c){
		if(c.tag=="Obstacle"){
			Destroy(c.gameObject);
		}
	}
}
