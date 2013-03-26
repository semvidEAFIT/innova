using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	private int coinsCollected;
	private int timesTripped;
	private float move;
	private int countCrashed;
	private float gameSpeed; 
	// Use this for initialization
	void Start () {
		move = 0.0001f;
		countCrashed = 0;
		gameSpeed = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelCtrl>().gameSpeed;
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
				countCrashed = 0;
				float posX = transform.position.x;
				while(transform.position.x<posX-25){
					transform.Translate(gameSpeed,0,0);
				}
			}
			
		}
		if (c.tag == "Segway"){
			//get on segway
		}
		if (c.tag == "Crowd"){
			//die?
			//c.gameObject.GetComponent<Crowd>().accelerarMultitud();
		}
	}
}
