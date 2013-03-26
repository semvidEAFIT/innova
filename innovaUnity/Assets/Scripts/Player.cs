using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	private int coinsCollected;
	private int timesTripped;
	private float move;
	private int countCrashed;
	private  Crowd crowd; 
	// Use this for initialization
	void Start () {
		move = 0.0001f;
		countCrashed = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space") && transform.position.y > -10){
			rigidbody.AddForce(Vector3.up * 10000);
		}
		transform.Translate(move, 0, 0);
		move *= -1;
	}
	
	void OnTriggerEnter(Collider c){
		if (c.tag == "Obstacle"){
			//destroy obstacle, reset jumpcounter, get closer to crowd
			countCrashed++;
			Debug.Log("collided with obstacle");
			
			if(countCrashed == 2){
				
				transform.Translate(5f, 0, 0);
				countCrashed = 0;
				
			}
			
		}
		if (c.tag == "Segway"){
			//get on segway
		}
		if (c.tag == "Crowd"){
			//die?
		}
	}
}
