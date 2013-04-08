using UnityEngine;
using System.Collections;

public class Ticket : MonoBehaviour {
	
	private float startPosition;
	private Vector3 dir;
	private bool goingUp;
	
	public float hoverDistance = 1;
	public float speed=1;
	
	// Use this for initialization
	void Start () {
		startPosition=transform.position.y;
		dir= -Vector3.forward *speed;
		goingUp=true;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(dir*Time.deltaTime);
		if (goingUp){
			if (transform.position.y-startPosition >= startPosition+hoverDistance){
				dir*=-1;
				goingUp=false;
			}
		} else {
			if (transform.position.y <= startPosition){
				dir*=-1;
				goingUp=true;
			}
		}
	}
	
	void OnTriggerEnter (Collider c){
		if (c.tag=="Player"){
			Destroy(this.gameObject);
		}
	}
}
