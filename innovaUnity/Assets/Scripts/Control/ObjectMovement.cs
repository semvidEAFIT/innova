using UnityEngine;
using System.Collections;

public class ObjectMovement : MonoBehaviour {
    public GameObject[] scenery;
    public Transform spawnPoint;
    public float displacementSpeed = 10.0f;
    public AudioClip[] chickenSounds;
    public float[] speedMultiplier;
    private ArrayList chickens;
    public float chickenSpawnTime = 10.0f;
    private float elapsedTime = 0.0f;
    public GameObject chickenPrefab;
    public GUISkin skin;
    public bool spawning = true;

    void Start() {
        chickens = new ArrayList();
    }

    void Update() {
        for (int i = 0; i < scenery.Length; i++)
        {
            scenery[i].transform.Translate(Vector3.left * displacementSpeed * speedMultiplier[i] * Time.deltaTime);
        }

        foreach (GameObject c in chickens)
        {
            c.transform.Translate(Vector3.right * displacementSpeed * 4 * Time.deltaTime);
        }
        elapsedTime += Time.deltaTime; 
           
            int spawnRate = Random.Range(0, 2);
            if(elapsedTime > chickenSpawnTime){
                foreach (GameObject prev in chickens)
                {
                    Destroy(prev);
                }
                chickens.Clear();
                if(chickenPrefab != null && spawnRate > 0 && spawning){
                    GameObject chicken = Instantiate(chickenPrefab, spawnPoint.position, chickenPrefab.transform.rotation) as GameObject;
                    chickens.Add(chicken);
                    audio.Stop();
                    int n = Random.Range(0, chickenSounds.Length);
                    audio.clip = chickenSounds[n];
                    audio.Play();
                }
                elapsedTime = 0.0f;
            }

        if(Time.time > 174){
            Time.timeScale = 0.0f;
        }
    }

    void OnGUI() {
        if (!spawning) return;
        if(skin!=null){
            GUI.skin = skin;
        }

        if(GUI.Button(new Rect(Screen.width/6, Screen.height/6, Screen.width/2, Screen.height/6), "PLAY GAME")){
            Application.LoadLevel("Intro");    
            /*GetComponent<Tutorial>().enabled = true;
            spawning = false;*/
        }

        if(GUI.Button( new Rect(Screen.width/2-Screen.width/6, Screen.height/2, Screen.width/2, Screen.height/6), "CREDITS")){
            Application.LoadLevel("Credits");
        }
    }
}
