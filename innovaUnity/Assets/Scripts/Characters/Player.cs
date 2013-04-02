using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float jumpSpeed = 60.0f;
	public float gravity = 1.0f;
	
	public GameObject segwayGO;
	private GameObject levelController;
	
	private Vector3 moveDirection = Vector3.zero;
	
	private bool jumped;
	private static bool segway;
	private bool segwayUsed;
	private bool blink;
	
	private int streak;
	private int countCrashed;
	private int countCrashedSegway;
	private CharacterController controller;
	
	private float move;
	private float height;
	private float segwayHeight;
	private float currentHeight;
	private float timer;
	private static float score;
	public static int segwayBonus = 0;
    private static bool stopScore = false;

    public static bool StopScore
    {
        get { return Player.stopScore; }
        set { Player.stopScore = value; }
    }

	private Sprite animation;
	
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
		segwayUsed = false;
		
		levelController = GameObject.Find("GameCtrl");
		animation= GetComponent<Sprite>();
		
		
		animation.reverse=false;
		animation.index=0;
		animation.currentRow=3;
		animation.loop=true;
	}
	
	// Update is called once per frame
	void Update () {
		
		//Debug.Log(Input.GetKeyDown(KeyCode.RightControl));
		if(!Player.stopScore){
		    score += (100 + streak * 10) * Time.deltaTime;
		}
		transform.Translate(0, move, 0);
		move *= -1;
		
		if(segwayGO == null){
			segway = false;
		}
		
		if(Input.GetKeyDown(KeyCode.Space) && !jumped && !sliding){
			if (!segway){
				animation.loop=false;
				animation.index=1;
				animation.currentRow=2;
				moveDirection.y = jumpSpeed;
			} else {
				moveDirection.y = jumpSpeed*2;
			}
			
			
			jumped = true;
			
			
			
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
				if (!segway) moveDirection.y -= gravity;
				else moveDirection.y -= gravity*2;
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
		
		if (transform.position.x < -25 && countCrashed == 1) {
			moveDirection.x = 0;
		}
		
        if (Input.GetKeyDown(KeyCode.DownArrow) && !jumped && !sliding && !segway){
			controller.radius=controller.radius/2;
			controller.center = new Vector3( controller.center.x, controller.center.y, controller.center.z + 2.5f);
			
			if (!segway){
				animation.loop=false;
				animation.currentRow=1;
				animation.index = 3; //dont ask why...
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
			
			if (!segway){
				animation.loop=true;
				animation.index=0;
				animation.currentRow=3;
			}
			
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
				this.GetComponentInChildren<JumpCounter>().resetStreak();
				moveDirection.x -= 3000 * Time.deltaTime;
			} else {
				Destroy(segwayGO.gameObject);
				segway = false;
				
				animation.loop=false;
				animation.currentRow=2;
				animation.index=2;
				
				levelController.GetComponent<LevelCtrl>().PlayLoopPrincipal();
				
				moveDirection.y = jumpSpeed;
				jumped = true;
			}
		}
		if (c.tag == "Crowd"){
			levelController.GetComponent<LevelCtrl>().LoseGame();
			Destroy(this.gameObject);
			c.gameObject.GetComponent<Crowd>().accelerateCrowd();
		}
		if(c.tag == "Auditorium"){
            animation.loop = true;
            animation.currentRow = 0;
            animation.index = 0;
			levelController.GetComponent<LevelCtrl>().WinGame();
		}
	}
	
	void OnTriggerStay(Collider c){
		if(c.tag == "Segway"){
			if(Input.GetKey(KeyCode.DownArrow) && !segway) {
				getOnSegway();
			}
		}
	}
	
	void getOnSegway(){
		Debug.Log("SEGWAY");
		animation.loop=false;
		animation.currentRow=3;
		animation.index=3;
		
		segwayGO = Instantiate(segwayGO, new Vector3(transform.position.x, transform.position.y, transform.position.z), 
			transform.rotation) as GameObject;
		transform.Translate(0f, 0f, -4f);
		segwayGO.transform.parent = this.transform;
		segway = true;
		segwayUsed = true;
		levelController.GetComponent<LevelCtrl>().PlaySegway();
	}
	
	public bool getSegwayUsed(){
		return segwayUsed;
	}
	
	public void setStreak(int streak){
		this.streak = streak;
	}
	
	public static float getScore(){
		if(segway){
			segwayBonus = 0;
		}
		return score + segwayBonus;
	}
}
