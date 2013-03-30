using UnityEngine;
using System.Collections;

public class JumpCounter : MonoBehaviour {
	int counter;
	
	// Use this for initialization
	void Start () {
		counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnTriggerEnter(Collider  other){
		if (other.tag == "Obstacle"){
			counter++;
			Destroy(other.gameObject, 3f);
		}
			
	}
}
