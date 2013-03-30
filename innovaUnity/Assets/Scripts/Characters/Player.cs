using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float jumpSpeed = 60.0f;
	public float gravity = 1.0f;
	
	public GameObject segwayGO;
	private GameObject levelController;
	
	private Vector3 moveDirection = Vector3.zero;
	private bool jumped;
	private bool segway;
	private float move;
	private int countCrashed;
	private CharacterController controller;
	private float height;
	private float segwayHeight;
	private float currentHeight;
	
	// Use this for initialization
	void Start () {
		height = transform.position.y;
		segwayHeight = height + 4;
		move = 0.0001f;
		jumped = false;
		countCrashed = 0;
		controller = GetComponent<CharacterController>();
		segway = false;
		levelController = GameObject.Find("GameCtrl");
		
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
			if(segway){
				currentHeight = segwayHeight;
			} else {
				currentHeight = height;
			}
			if (moveDirection.y >= 0 || transform.position.y+moveDirection.y * Time.deltaTime > currentHeight){
				moveDirection.y -= gravity;
			}else{
				moveDirection.y = 0;
				controller.Move(new Vector3(0, currentHeight - transform.position.y, 0));
				jumped = false;
			}
		}
		
		
		
		if (transform.position.x < -30 && countCrashed == 1) {
			moveDirection.x = 0;
		}
	
		
		
        if (Input.GetKeyDown(KeyCode.DownArrow)){
			controller.radius=controller.radius/2;
			controller.center = new Vector3( controller.center.x, controller.center.y, controller.center.z + 2.5f);
		}
		
		if(Input.GetKeyUp(KeyCode.DownArrow)){
			controller.radius = controller.radius * 2;
			controller.center = new Vector3( controller.center.x, controller.center.y, controller.center.z - 2.5f);
		}
		controller.Move(moveDirection * Time.deltaTime);
		
	}
	
	void OnTriggerEnter(Collider c){
		
		if (c.tag == "Obstacle"){
			//destroy obstacle, reset jumpcounter, get closer to crowd
			if(!segway){
				countCrashed++;
				moveDirection.x -= 3000 * Time.deltaTime;
			}
		}
		if (c.tag == "Segway"){
			getOnSegway();
		}
		if (c.tag == "Crowd"){
			levelController.GetComponent<LevelCtrl>().PlayFail();
			Destroy(this.gameObject);
			c.gameObject.GetComponent<Crowd>().accelerateCrowd();
		}
	}
	
	void OnTriggerStay(Collider c){
		if(c.tag == "Segway"){
			getOnSegway();
		}
	}
	
	void OnTriggerExit(Collider c){
		if (c.tag == "Segway"){
			getOnSegway();
		}
	}
	
	void getOnSegway(){
		if((Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl)) && !segway){
			segwayGO = Instantiate(segwayGO, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation) as GameObject;
			transform.Translate(0f, 0f, -4f);
			segwayGO.transform.parent = this.transform;
			segway = true;
			levelController.GetComponent<LevelCtrl>().PlaySegway();
		}
	}
}
