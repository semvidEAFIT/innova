using UnityEngine;
using System.Collections;

public class JumpCounter : MonoBehaviour {
	
	private int counter;
	
	// Use this for initialization
	void Start () {
		counter = 0;
	}
	
	// Update is called once per frame
	void Update (){
		
	}
	
	void OnTriggerEnter(Collider  other){
		if (other.tag == "Obstacle"){
			counter++;
			Destroy(other.gameObject, 3f);
			Debug.Log(counter);
		}
	}
	
	public void setStreak(){
		this.transform.parent.GetComponent<Player>().setStreak(counter);
	}
	
	public void resetStreak(){
		this.counter = 0;
	}
}
