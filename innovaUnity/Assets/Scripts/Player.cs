using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	private int coinsCollected;
	private int timesTripped;
	private float move;
	public float jumpSpeed;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	private bool jumped, falling;
	
	// Use this for initialization
	void Start () {
		move = 0.0001f;
		jumpSpeed=30F;
		jumped = false;
		falling = false;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0, move, 0);
		move *= -1;
		CharacterController controller = GetComponent<CharacterController>();
		if (Input.GetKeyDown("space") && !jumped){
			moveDirection.y = jumpSpeed;
			jumped = true;
		}
		
		if (transform.position.y < 1 && falling){
				moveDirection.y = 0;
				jumped=false;
				falling=false;
		}
		
		if (transform.position.y > 13){
			falling=true;
			moveDirection.y -= gravity;
		}
		
		controller.Move(moveDirection * Time.deltaTime);
		
	}
	
	void OnTriggerEnter(Collider c){
		if (c.tag == "Obstacle"){
			//destroy obstacle, reset jumpcounter, get closer to crowd
			Debug.Log("collided with obstacle");
		}
		if (c.tag == "Segway"){
			//get on segway
		}
		if (c.tag == "Crowd"){
			//die?
		}
	}
}
