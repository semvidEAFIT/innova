using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float jumpSpeed = 60.0f;
	public float gravity = 1.0F;
	
	private Vector3 moveDirection = Vector3.zero;
	private bool jumped;
	private float move;
	private int countCrashed;
	private CharacterController controller;
	private float height;
	
	// Use this for initialization
	void Start () {
		height = transform.position.y;
		move = 0.0001f;
		jumped = false;
		countCrashed = 0;
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0, move, 0);
		move *= -1;
		
		if(Input.GetKeyDown(KeyCode.Space) && !jumped){
			moveDirection.y = jumpSpeed;
			jumped = true;
		}
		
		if(jumped){
			if (moveDirection.y >= 0 || transform.position.y > height){
				moveDirection.y -= gravity;
			}else{
				moveDirection.y = 0;
				transform.position.Set (transform.position.x,height, transform.position.z);
				jumped = false;
			}
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
			countCrashed++;
			moveDirection.x -= 3000 * Time.deltaTime;
		}
		if (c.tag == "Segway"){
			if(Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl)){
			}
		}
		if (c.tag == "Crowd"){
			//die?
			Destroy(this.gameObject);
			c.gameObject.GetComponent<Crowd>().accelerateCrowd();
		}
	}
	
	void OnTriggerStay(Collider c){
		if (c.tag == "Segway"){
			if(Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl)){
			}
		}
	}
	
	void OnTriggerExit(Collider c){
		if (c.tag == "Segway"){
			if(Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl)){

			}
		}
	}
}
