using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	
	public float speed;
	private bool blink;
	private float timer;
	private bool havesChild;
	
	// Use this for initialization
	void Start () {
		blink=false;
		timer=0;
		if (transform.childCount>0) havesChild=true;
		else havesChild=false;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(speed, 0, 0));
		if (blink){
			timer+=Time.deltaTime;
			if (timer>0.05f)	{
				if (havesChild) {
					foreach (Transform child in transform){
						child.renderer.enabled = !child.renderer.enabled;
					}
				}
				else renderer.enabled = !renderer.enabled;
				timer=0;
			}
		}
	}
	
	void OnTriggerEnter (Collider c) {
		if (c.tag == "Player"){
			Destroy(this.gameObject, 0.3F);
			blink=true;
		}
	}
}
