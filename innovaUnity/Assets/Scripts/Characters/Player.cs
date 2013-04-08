using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float jumpSpeed;
	public float gravity;
	
	public GameObject segwayGO;
	private GameObject levelController;
	
	private Vector3 velocity = Vector3.zero;
	
	private bool jumped;
	private static bool segway;
	private bool segwayUsed;
	private bool blink;
	private int countCrashed;
	private int countCrashedSegway;
	private CharacterController controller;
    private float crowdDistance;
    public int lifes= 2;
	private float move;
	private float height;
	private float segwayHeight;
	private float currentHeight;
	private float timer;
    private static float score = 0;

    public static float Score
    {
        get { return Player.score; }
    }

    private static int streak = 0;

    public static int Streak
    {
        get {
            int segwayBonus = (segway) ? 2 : 1;
            return segwayBonus * JumpCounter.Counter;
        }
    }

	public static int segwayBonus = 0;
    private static bool finished = false;
    private Vector3 dx = Vector3.zero;

    public static bool Finished
    {
        get { return Player.finished; }
        set { Player.finished = value; }
    }

	private Sprite animation;
	
	private bool sliding;
	
	public AudioClip jump;
	public AudioClip slide;
	public AudioClip fall;
	
	// Use this for initialization
	void Start () {
        score = 0;
        finished = false;
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

        crowdDistance = this.transform.position.x - GameObject.Find("Crowd(Clone)").transform.position.x;
		animation.reverse=false;
		animation.index=0;
		animation.currentRow=3;
		animation.loop=true;
	}

	// Update is called once per frame
	void Update () {

        if (!finished)
        {
            //Debug.Log(Input.GetKeyDown(KeyCode.RightControl));
            if (!Player.finished)
            {
                int segWaybonus = (segway) ? 2 : 1;
                score += (100 + JumpCounter.Counter * segWaybonus * 100) * Time.deltaTime;
            }
            transform.Translate(0, move, 0);
            move *= -1;

            if (segwayGO == null)
            {
                segway = false;
            }

            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && !jumped && !sliding)
            {
                if (!segway)
                {
                    animation.loop = false;
                    animation.index = 1;
                    animation.currentRow = 2;
                    velocity.y = jumpSpeed;
                }
                else
                {
                    velocity.y = jumpSpeed * 2;
                }


                jumped = true;

                //sound
                audio.Stop();
                audio.clip = jump;
                audio.Play();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && !jumped && !sliding && !segway)
            {
                controller.height = controller.height / 2;
                controller.center = new Vector3(controller.center.x, controller.center.y, controller.center.z + 2.5f);

                if (!segway)
                {
                    animation.loop = false;
                    animation.currentRow = 1;
                    animation.index = 1; //dont ask why...
                }

                //sound
                audio.Stop();
                audio.clip = slide;
                audio.Play();
                audio.loop = true;

                sliding = true;
            }

            if (Input.GetKeyUp(KeyCode.DownArrow) && sliding)
            {
                controller.height = controller.height * 2;
                controller.center = new Vector3(controller.center.x, controller.center.y, controller.center.z - 2.5f);

                if (!segway)
                {
                    animation.loop = true;
                    animation.index = 0;
                    animation.currentRow = 3;
                }

                audio.loop = false;
                audio.Stop();

                sliding = false;
            }
        }

        Vector3 acceleration = Vector3.zero;
        if (jumped)
        {
            if (segway)
            {
                currentHeight = segwayHeight;
            }
            else
            {
                currentHeight = height;
            }
            if (velocity.y >= 0 || transform.position.y + (velocity.y + acceleration.y * Time.deltaTime) * Time.deltaTime > currentHeight)
            {
                if (!segway)
                {
                    acceleration.y = -gravity;
                }
                else
                {
                    acceleration.y = -gravity * 2;
                }
            }
            else
            {
                velocity.y = 0;
                acceleration.y = 0;
                controller.Move(new Vector3(0, currentHeight - transform.position.y, 0));
                jumped = false;

                if (!finished)
                {
                    if (!segway)
                    {
                        animation.loop = true;
                        animation.index = 0;
                        animation.currentRow = 3;
                    }
                }
                else 
                {
                    animation.loop = true;
                    animation.currentRow = 0;
                    animation.index = 0;
                }

                //sound
                audio.Stop();
                audio.clip = fall;
                audio.Play();
            }
        }

        velocity += acceleration * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime + dx);
        dx = Vector3.zero;
	}
	
	void OnTriggerEnter(Collider c){
		if (c.tag == "Obstacle"){
			if(!segway){
				countCrashed++;
                JumpCounter.Counter = 0;
                dx.x -= crowdDistance / lifes - 4;
			} else {
				Destroy(segwayGO.gameObject);
				segway = false;
                //this.GetComponentInChildren<JumpCounter>().resetStreak();
				animation.loop=false;
				animation.currentRow=2;
				animation.index=1;
                JumpCounter.Counter = 0;
				levelController.GetComponent<LevelCtrl>().PlayLoopPrincipal();
				
				velocity.y = jumpSpeed;
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
		animation.loop=false;
		animation.currentRow=3;
		animation.index=0;
		
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
}
