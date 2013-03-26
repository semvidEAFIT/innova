using UnityEngine;
using System.Collections;

public class Crowd : MonoBehaviour {
	
	public float speed;
	
	// Use this for initialization
	void Start () {
		speed = 5f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void accelerarMultitud(){
		transform.Translate(-speed, 0, 0);
	}
}
