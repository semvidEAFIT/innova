using UnityEngine;
using System.Collections;

public class StillCharacter : MonoBehaviour {
	
	public Material normalSprite;
	public Material specialSprite;
	public GameObject particles;
	
	private bool characterSelected;
	
	// Use this for initialization
	void Start () {
		characterSelected=false;
		renderer.material = normalSprite;
		
		particles.particleSystem.Stop();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseEnter() {
		particles.particleSystem.Play();
		renderer.material = specialSprite;
	}
	
	void OnMouseExit() {
		renderer.material = normalSprite;
		particles.particleSystem.Stop();
	}
}
