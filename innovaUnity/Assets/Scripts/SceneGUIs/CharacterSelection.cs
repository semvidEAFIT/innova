using UnityEngine;
using System.Collections;

public class CharacterSelection : MonoBehaviour {
	
	public AudioClip loopMenu;
	public AudioClip characterSelected;
	
	public GameObject boy;
	public GameObject girl;
	
	private static bool isBoy;
	private bool hasSelectedCharacter = false;
	
	private float elapsedTime = 0.0f;
	
	public static bool IsBoy {
		get {
			return CharacterSelection.isBoy;
		}
	}	
	
	// Use this for initialization
	void Start () {
		if(!hasSelectedCharacter){
			audio.clip=loopMenu;
			audio.loop=true;
			audio.Play();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (hasSelectedCharacter) {
			elapsedTime += Time.deltaTime;
			if (elapsedTime > audio.clip.length) {
				DontDestroyOnLoad(this);
				Application.LoadLevel("TheGame");
			}
		}
		
        if(girl.GetComponent<StillCharacter>().CharacterSelected){
            boy.GetComponent<StillCharacter>().selectCharater(false);
        }

        if(boy.GetComponent<StillCharacter>().CharacterSelected){
            girl.GetComponent<StillCharacter>().selectCharater(false);
        }

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
				
				hasSelectedCharacter = true;
			}
		}

        if(!hasSelectedCharacter){
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                isBoy = false;
                girl.GetComponent<StillCharacter>().selectCharater(true);
                boy.GetComponent<StillCharacter>().selectCharater(false);
            }
            else 
            {
                if(Input.GetKeyDown(KeyCode.LeftArrow)){
                    isBoy = true;
                    girl.GetComponent<StillCharacter>().selectCharater(false);
                    boy.GetComponent<StillCharacter>().selectCharater(true);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            hasSelectedCharacter = true;
            audio.Stop();
            audio.clip = characterSelected;
            audio.loop = false;
            audio.Play();
        }
	}
}
