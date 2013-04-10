using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {
	
	public float scrollingSpeed=0.2f;
	
	public string[] lineasCreditos;
	public bool[] esTitulo;
	
	private string text;
	
	// Use this for initialization
	void Start () {
		for (int i=0; i<lineasCreditos.Length; i++){
			if (esTitulo[i]) text+= "<size=35>" + lineasCreditos[i] + "</size>" + "\n";
			else text+= lineasCreditos[i] + "\n";
		}
		guiText.text= text;
	}
	
	// Update is called once per frame
	void Update () {
		transform.transform.Translate(0, scrollingSpeed*Time.deltaTime, 0);

        if (lineasCreditos.Length / 10 + 1 < gameObject.transform.position.y)
        {
            StartCoroutine(endGame());
		}

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)){
            Application.LoadLevel("MainMenu");
        }
	}

    IEnumerator endGame() {
        yield return new WaitForSeconds(0.5f);
        Application.LoadLevel("MainMenu");
    }
}
