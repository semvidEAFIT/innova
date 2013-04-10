using UnityEngine;
using System.Collections;

public class PlayerTutorial : MonoBehaviour {

    private Transform target = null;
    public float speed = 10.0f;
    private CharacterController controller;
    private Status status = Status.Walk;

    public Status Status1
    {
        get { return status; }
        set { status = value; }
    }

    void Start() {
        controller = GetComponent<CharacterController>();
    }

    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }
	
	// Update is called once per frame
	void Update () {
	    if(target != null){
            switch (status)
            {
                case Status.Walk:
                    {
                        if (Mathf.Abs(target.position.x - transform.position.x) < 0.1)
                        {
                            target = null;
                            GetComponent<Sprite>().fps = 10;
                            controller.enabled = false;
                        }
                        else
                        {
                            if (!controller.enabled)
                            {
                                controller.enabled = true;
                                GetComponent<Sprite>().fps = 20;
                            }
                            float direction = (target.position.x - transform.position.x > 0) ? 1 : -1;
                            controller.Move(direction * speed * Time.deltaTime * Vector3.right);
                        }
                    }
                    break;
                case Status.Slide:
                    { 
                    
                    }
                    break;
                case Status.Jump:
                    { 
                        
                    }
                    break;
                default:
                    break;
            }
        }
	}

    public enum Status { 
        Walk, Jump, Slide
    }
}
