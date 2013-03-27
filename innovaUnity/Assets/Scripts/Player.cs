using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float jumpSpeed;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	private bool jumped, falling;
	private float move;
	private int countCrashed;
	private float speed; 
	private bool translate;
	
	// Use this for initialization
	void Start () {
		move = 0.0001f;
		jumpSpeed=30F;
		jumped = false;
		falling = false;
		countCrashed = 0;
		speed = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelCtrl>().maxGameSpeed;
		translate=false;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0, move, 0);
		move *= -1;

		if(countCrashed >= 2){
			
			Crowd c=(Crowd)GameObject.Find("Girl(Clone)").GetComponent("Crowd");
			c.accelerateCrowd();
		}
			float posX = transform.position.x;
			while(transform.position.x > posX-28f && translate){
			
				transform.Translate(speed, 0, 0);
				Debug.Log("velocidad"+speed);
				Debug.Log("posX"+posX);
				Debug.Log("position now"+transform.position);
			}
		translate=false;
		
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
			countCrashed++;
			translate=true;
			
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
