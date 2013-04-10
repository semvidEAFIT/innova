using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelCtrl : MonoBehaviour {
	
	private static LevelCtrl levelCtrl;
	public static LevelCtrl Instance{
		get{
			if(levelCtrl.Equals(null)){
				levelCtrl = new LevelCtrl();
			}
			return levelCtrl;
		}
	}
		
	public float sceneryLength;
	public float sceneryHeight;
	public float skyLength;
	
	public float gameSpeed = 50.0f;
	public float maxGameSpeed = 90.0f;
	public float accelerationRate = 1.0f;
	public GameObject objectGenerator;
	
	public AudioClip introLoopPrincipal;
	public AudioClip loopPrincipal;
	public AudioClip audioGanar;
	public AudioClip introLoopSegway;
	public AudioClip LoopSegway;
	public AudioClip audioFail;

    public GameObject loseScreen;
    public bool fadeLose = false;
    public bool won = false;
    public float fadeDuration = 5.0f;
    public float elapsedFadeTime = 0.0f;
	private float timeElapsed;
    public GUISkin skin;
    private bool finished = false, lost = false;
    private float elapsedTime = 0.0f;

    public Texture2D heart;

	void Awake(){
		levelCtrl = this;
	}
	
	void Start(){
        objectGenerator = Instantiate(objectGenerator, new Vector3(transform.position.x + sceneryLength / 2, objectGenerator.transform.position.y,
            objectGenerator.transform.position.z), transform.rotation) as GameObject;
        Destroy(GameObject.Find("CharacterSelection"));
		PlayLoopPrincipal();
	}
	
	void Update(){
		
		if(gameSpeed < maxGameSpeed){
			gameSpeed += accelerationRate * Time.deltaTime;
		}
		
		if (!audio.isPlaying){
			if (audio.clip==introLoopPrincipal){
				audio.clip=loopPrincipal;
				audio.Play();
				audio.loop=true;
			}
			if (audio.clip==introLoopSegway){
				audio.clip=LoopSegway;
				audio.Play();
				audio.loop=true;
			}
		}
        if(fadeLose){
            loseScreen.renderer.material.color = new Color(loseScreen.renderer.material.color.r, loseScreen.renderer.material.color.g, loseScreen.renderer.material.color.b, Mathf.Clamp(Mathf.Lerp(256, 0, elapsedFadeTime /  fadeDuration),0,256));
            elapsedFadeTime+=Time.deltaTime;
        }
        if(finished){
            elapsedTime += Time.deltaTime;
            if(elapsedTime > audio.clip.length){
                //DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Player"));
                Application.LoadLevel("Register");
            }
        }
	}

    public void WinGame()
    {
        Player.Finished = true;
		setSpeedToZero();
        Player.GotTicket = true;
        //Destroy(this.objectGenerator);
        PlayWin();
//        float length = audio.clip.length;
        finished = true;
    }
	
    public void LoseGame() {
        lost = true;
        Player.Finished = true;
        Player.GotTicket = false;
        PlayFail();
		setSpeedToZero();
        if(loseScreen != null){
            loseScreen.SetActive(true);
        }       
    }
	
	void OnGUI () {
        if (skin != null)
        {
            GUI.skin = skin;
        }

        //GUI.color = Color.black;
        GUI.Label(new Rect(0, 0, Screen.width / 8, Screen.height / 16), "SCORE");

        GUI.TextField(new Rect(Screen.width / 8, 0, Screen.width / 7, Screen.height / 16), ((int)Player.Score).ToString());
        //TODO FIX STREAK LABEL
        GUI.Label(new Rect(3 * Screen.width / 4 - Screen.width / 72, 0, Screen.width / 6, Screen.height / 16), "STREAK");
        GUI.TextField(new Rect(7 * Screen.width / 8, 0, Screen.width / 10, Screen.height / 16), "x" + (Player.Streak));

        if (lost)
        {
            if (GUI.Button(new Rect(Screen.width / 3, 4 * Screen.height / 6, Screen.width / 3, Screen.height / 5), "Next")) {
                Application.LoadLevel("Register");
            }
        }

        for (int i = 0; i < Player.Lifes; i++)
        {
            GUI.Label(new Rect( Screen.width/2 - 165/2 + (55 * i), 10, 50, 50), heart);
        }
	}
	
	public void PlayLoopPrincipal (){
		audio.loop=false;
		audio.Stop();
		audio.clip = introLoopPrincipal;
		audio.Play();
	}
	
	public void PlaySegway(){
		audio.loop=false;
		audio.Stop();
		audio.clip = introLoopSegway;
		audio.Play();
	}


    //public IEnumerator PlayWin()
    //{
    //    if (audio.clip != audioGanar)
    //    {
    //        audio.loop = false;
    //        audio.Stop();
    //        audio.clip = audioGanar;
    //        audio.Play();
    //    }
    //    yield return new WaitForSeconds(audio.clip.length);
    //}
    //	
    //	public IEnumerator PlayFail() {
    //		audio.loop=false;
    //		audio.clip = audioFail;
    //		audio.Play();
    //        yield return new WaitForSeconds(audio.clip.length);
    //	}

    public void PlayWin()
    {
        if (audio.clip != audioGanar)
        {
            audio.loop = false;
            audio.Stop();
            audio.clip = audioGanar;
            audio.Play();
        }
    }
	public void PlayFail() {
		audio.loop=false;
		audio.clip = audioFail;
		audio.Play();
	}
	
	public void setSpeedToZero(){
		GameObject.FindGameObjectWithTag("Auditorium").GetComponent<Scenery>().speed = 0f;
		
		objectGenerator.GetComponent<ObjectGenerator>().StopObstacles();
		
		GameObject[] skies = GameObject.FindGameObjectsWithTag("Sky");
		for(int i = 0; i < skies.Length; i++){
			skies[i].GetComponent<SkyMovement>().speed = 0;
		}
	}
}