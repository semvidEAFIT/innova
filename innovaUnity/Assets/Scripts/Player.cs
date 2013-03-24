using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	private int coinsCollected;
	private int timesTripped;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space") && transform.position.y < 6.2){
			rigidbody.AddForce(Vector3.up * 10000);
		}
	}
	
	void OnTriggerEnter(Collider c){
		if (c.tag=="Obstacle"){
			//destroy obstacle, reset jumpcounter, get closer to crowd
			Debug.Log("collided with obstacle");
		}
		if (c.tag=="Segway"){
			//get on segway
		}
		if (c.tag=="Crowd"){
			//die?
		}
	}
}
