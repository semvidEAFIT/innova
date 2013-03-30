using UnityEngine;
using System.Collections;

public class Crowd : MonoBehaviour {
	
	public float speed;

	// Use this for initialization
	void Start () {
		speed = 0f;

	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(speed,0,0);
	}
	
	public void accelerateCrowd(){
		speed=-1f;
		audio.Play();
	}
}
