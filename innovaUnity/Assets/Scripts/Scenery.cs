using UnityEngine;
using System.Collections;

public class Scenery : MonoBehaviour {
	
	private float speed;
	public float maxSpeed;
	public float sceneryLength;
	
	// Use this for initialization
	void Start () {
		speed = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(speed, 0, 0));
		if(speed < maxSpeed){
			speed += speed / 100;
		}
		if(transform.position.x < -sceneryLength - 10){
			transform.Translate(new Vector3(-3 * sceneryLength, 0, 0));
		}
	}
}
