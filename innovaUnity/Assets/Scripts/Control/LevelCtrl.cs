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
	
	public float gameSpeed;
	public float maxGameSpeed;
	public GameObject objectGenerator;
	
//	public List<GameObject> backgrounds;
//	public List<GameObject> sky;
//	
//	public GameObject player;
//	public GameObject crowd;
	
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
	
	void Awake(){
		levelCtrl = this;
	}
	
	void Start(){
//		for(int i = 0; i < sky.Count; i++){
//			Instantiate(sky[i], new Vector3(sky[i].transform.position.x + (i * skyLength), sky[i].transform.position.y + 20, sky[i].transform.position.z), 
//				sky[i].transform.rotation);
//		}
//		
//		for(int i = 0; i < backgrounds.Count; i++){
//				Instantiate(backgrounds[i], new Vector3(transform.position.x + (i * sceneryLength), 
//				(backgrounds[i].transform.position.y + sceneryHeight / 2.1f), backgrounds[i].transform.position.z), backgrounds[i].transform.rotation);
//		}
//		
//		Instantiate(player, player.transform.position, player.transform.rotation);
//
//		Instantiate(crowd, crowd.transform.position, crowd.transform.rotation);
		Instantiate(objectGenerator, new Vector3(transform.position.x + sceneryLength, objectGenerator.transform.position.y, 
			objectGenerator.transform.position.z), transform.rotation);
		
		PlayLoopPrincipal();
	}
	
	void Update(){
		if(Time.time >= 40f){
            WinGame();
            won = true;
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
	}

    private void WinGame()
    {
        PlayWin();
        Debug.Log("Se acabo'sta monda");
    }
	
//	void OnGUI () {
//		
//	}
	
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
	
	
//	public IEnumerator PlayWin(){
//		if (audio.clip != audioGanar) {
//			audio.loop=false;
//			audio.Stop();
//			audio.clip = audioGanar;
//			audio.Play();
//		}
//        yield return new WaitForSeconds(audio.clip.length);
//	}
//	
//	public IEnumerator PlayFail() {
//		audio.loop=false;
//		audio.clip = audioFail;
//		audio.Play();
//        yield return new WaitForSeconds(audio.clip.length);
//	}
	
	public void PlayWin(){
		if (audio.clip != audioGanar) {
			audio.loop=false;
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
	
    public void LoseGame() {
        PlayFail();
        if(loseScreen != null){
            loseScreen = (GameObject)GameObject.Instantiate(loseScreen, camera.transform.position + Vector3.forward * 10, Quaternion.identity);
        }       
    }
	
}