using UnityEngine;
using System.Collections;

public class JumpCounter : MonoBehaviour {

    private static int counter;

    public static int Counter
    {
        get { return JumpCounter.counter; }
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
	
	public void setStreak(){
		this.transform.parent.GetComponent<Player>().setStreak(counter);
	}
	
	public void resetStreak(){
		JumpCounter.counter = 0;
	}
}
