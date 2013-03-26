using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	
	public float speed;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(speed, 0, 0));
	}
}
