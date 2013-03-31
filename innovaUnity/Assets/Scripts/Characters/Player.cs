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
	private bool blink;
	private bool justUsedSegway;
	private float move;
	private int countCrashed;
	private int countCrashedSegway;
	private CharacterController controller;
	private float height;
	private float segwayHeight;
	private float currentHeight;
	
	private Sprite animation;
	
	private float timer;
	
	private bool sliding;
	
	public AudioClip jump;
	public AudioClip slide;
	public AudioClip fall;
	
	// Use this for initialization
	void Start () {
		height = transform.position.y;
		segwayHeight = height + 4;
		move = 0.0001f;
		jumped = false;
		sliding = false;
		timer = 0;
		countCrashed = 0;
		controller = GetComponent<CharacterController>();
		segway = false;
		justUsedSegway = false;
		
		levelController = GameObject.Find("GameCtrl");
		animation= GetComponent<Sprite>();
		
		
		animation.reverse=false;
		animation.index=0;
		animation.currentRow=3;
		animation.loop=true;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0, move, 0);
		move *= -1;
		
		if(segwayGO == null){
			segway = false;
		}
		
		if(Input.GetKeyDown(KeyCode.Space) && !jumped && !sliding){
			moveDirection.y = jumpSpeed;
			jumped = true;
			
			if (!segway){
				animation.loop=false;
				animation.index=2;
				animation.currentRow=2;
			}
			
			//sound
			audio.Stop();
			audio.clip = jump;
			audio.Play();
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
				
				if (!segway){
					animation.loop=true;
					animation.index=0;
					animation.currentRow=3;
				}
				
				//sound
				audio.Stop();
				audio.clip = fall;
				audio.Play();
			}
		}
		
		if (transform.position.x < -30 && countCrashed == 1) {
			moveDirection.x = 0;
		}
		
        if (Input.GetKeyDown(KeyCode.DownArrow) && !jumped && !sliding && !segway){
			controller.radius=controller.radius/2;
			controller.center = new Vector3( controller.center.x, controller.center.y, controller.center.z + 2.5f);
			
			if (!segway){
				animation.loop=false;
				animation.currentRow=1;
				animation.index=3; //dont ask why...
			}
			
			//sound
			audio.Stop();
			audio.clip = slide;
			audio.Play();
			audio.loop=true;
			
			sliding=true;
		}
		
		if(Input.GetKeyUp(KeyCode.DownArrow) && sliding){
			controller.radius = controller.radius * 2;
			controller.center = new Vector3( controller.center.x, controller.center.y, controller.center.z - 2.5f);
			
			animation.loop=true;
			animation.index=0;
			animation.currentRow=3;
			
			audio.loop=false;
			audio.Stop ();
			
			sliding=false;
		}
		controller.Move(moveDirection * Time.deltaTime);
	}
	
	void OnTriggerEnter(Collider c){
		if (c.tag == "Obstacle"){
			if(!segway){
				countCrashed++;
				moveDirection.x -= 3000 * Time.deltaTime;
			} else {
				Destroy(segwayGO.gameObject);
				segway=false;
				
				animation.loop=false;
				animation.currentRow=2;
				animation.index=2;
				
				levelController.GetComponent<LevelCtrl>().PlayLoopPrincipal();
				
				moveDirection.y = jumpSpeed;
				jumped = true;
			}
		}
		if (c.tag == "Segway"){
			getOnSegway();
		}
		if (c.tag == "Crowd"){
			levelController.GetComponent<LevelCtrl>().LoseGame();
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
			animation.loop=false;
			animation.currentRow=3;
			animation.index=1;
			
			segwayGO = Instantiate(segwayGO, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation) as GameObject;
			transform.Translate(0f, 0f, -4f);
			segwayGO.transform.parent = this.transform;
			segway = true;
			levelController.GetComponent<LevelCtrl>().PlaySegway();

		}
	}
}
