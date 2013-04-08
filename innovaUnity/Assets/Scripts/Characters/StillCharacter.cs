using UnityEngine;
using System.Collections;

public class StillCharacter : MonoBehaviour {
	
	public Material normalSprite;
	public Material specialSprite;
	public GameObject particles;

    private bool characterSelected;

    public bool CharacterSelected
    {
        get { return characterSelected; }
    }
	
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
        selectCharater(true);
	}

    public void selectCharater(bool p)
    {
        characterSelected = p;
        if (p)
        {
            particles.particleSystem.Play();
            renderer.material = specialSprite;
        }
        else 
        {
            renderer.material = normalSprite;
            particles.particleSystem.Stop();
        }
    }
	
	void OnMouseExit() {
        selectCharater(false);
	}
}
