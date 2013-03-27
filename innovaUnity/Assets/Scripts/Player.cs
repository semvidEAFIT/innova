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
	private CharacterController controller;
	
	// Use this for initialization
	void Start () {
		move = 0.0001f;
		jumpSpeed=30F;
		jumped = false;
		falling = false;
		countCrashed = 0;
		speed = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelCtrl>().maxGameSpeed;
		
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0, move, 0);
		move *= -1;

		
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
		
		if (transform.position.x < -30 && countCrashed==1) {
			moveDirection.x=0;
		}
	

        if (Input.GetKeyDown(KeyCode.DownArrow)){
            
			controller.radius=controller.radius/2;
			controller.center = new Vector3( controller.center.x, controller.center.y, controller.center.z+ 2.5f);
			
		}
		
		if(Input.GetKeyUp(KeyCode.DownArrow)){
				
			controller.radius = controller.radius*2;
			controller.center = new Vector3( controller.center.x, controller.center.y, controller.center.z - 2.5f);
			
		}
		
		controller.Move(moveDirection * Time.deltaTime);
		
	}
	
	void OnTriggerEnter(Collider c){
		
		if (c.tag == "Obstacle"){
			//destroy obstacle, reset jumpcounter, get closer to crowd
			Destroy(c.gameObject);
			countCrashed++;
			moveDirection.x -= 3000 * Time.deltaTime;
			
			
			Destroy(c.gameObject, 0.1F);
		}
		if (c.tag == "Segway"){
			//get on segway
		}
		if (c.tag == "Crowd"){
			//die?
			Destroy(this.gameObject);
			c.gameObject.GetComponent<Crowd>().accelerateCrowd();
		}
	}
}
