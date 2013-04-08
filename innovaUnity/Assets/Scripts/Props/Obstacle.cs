using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	
	public float speedMultiplier;
	private float speed;
	public AudioClip[] audioFile;
	private bool blink;
	private bool stopped;
	private float timer;
	private bool hasChild;
	
	// Use this for initialization
	void Start () {
		speed = LevelCtrl.Instance.gameSpeed * Time.deltaTime;
		blink = false;
		stopped = false;
		timer=0;
		if (transform.childCount>0) hasChild=true;
		else hasChild=false;
		if (audioFile.Length!=0){
			AudioClip fileToLoad = audioFile[Random.Range(0, audioFile.Length)];
			audio.clip = fileToLoad;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!stopped){
			speed = LevelCtrl.Instance.gameSpeed * Time.deltaTime * speedMultiplier;
			transform.Translate(new Vector3(speed, 0, 0));
		}
		if (blink){
			timer+=Time.deltaTime;
			if (timer>0.05f)	{
				if (hasChild) {
					foreach (Transform child in transform){
						child.renderer.enabled = !child.renderer.enabled;
					}
				}
				else renderer.enabled = !renderer.enabled;
				timer = 0;
			}
		}
	}
	
	void OnTriggerEnter (Collider c) {
		if (c.tag == "Player" ){
			if(this.gameObject.tag != "Segway"){
				blink = true;
				Destroy(collider);
				Destroy(this.gameObject, 0.2F);
			}
		}
		if (audioFile.Length!=0) audio.Play();
	}

    void OnCollisionEnter(Collision c) { 
        if(c.collider.tag == "Crowd"){
            Destroy(collider);
            DestroyImmediate(this.gameObject);
            if (audioFile.Length != 0) audio.Play();
        }
    }
	public void setStopped(bool stop){
		stopped = stop;
	}
}
