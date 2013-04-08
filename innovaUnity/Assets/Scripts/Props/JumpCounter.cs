using UnityEngine;
using System.Collections;

public class JumpCounter : MonoBehaviour {

    private static int counter = 0;

    public static int Counter
    {
        get { return counter; }
        set { counter = value; }
    }
	
	// Use this for initialization
	void Start () {
		counter = 0;
	}
	
	void OnTriggerEnter(Collider  other){
		if (other.tag == "Obstacle"){
			counter++;
			Destroy(other.gameObject, 3f);
		}
		if (other.tag == "Segway")
			Destroy(other.gameObject);
	}
}
