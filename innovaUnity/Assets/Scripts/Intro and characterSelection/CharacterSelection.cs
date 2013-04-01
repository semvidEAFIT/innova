using UnityEngine;
using System.Collections;

public class CharacterSelection : MonoBehaviour {
	
	public AudioClip loopMenu;
	public AudioClip characterSelected;
	
	public GameObject boy;
	public GameObject girl;
	
	private bool isBoy;
	
	// Use this for initialization
	void Start () {
		audio.clip=loopMenu;
		audio.loop=true;
		audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)){
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit)){
				audio.Stop();
				audio.clip=characterSelected;
				audio.loop=false;
				audio.Play();
				
				if (hit.collider.gameObject==boy)
					isBoy=true;
				else isBoy=false;
			}
			
			Debug.Log(isBoy);
		}
	}
}
