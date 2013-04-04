using UnityEngine;
using System.Collections;

public class SkyMovement : MonoBehaviour {

	public float speed;
	private float skyLength;
	private float skyCount;
	
//	private GameObject gameControl;
	
	void Start () {
//		gameControl = GameObject.FindGameObjectWithTag("GameController");
		speed = LevelCtrl.Instance.gameSpeed * 0.2f;
		skyCount = LevelCtrl.Instance.objectGenerator.GetComponent<ObjectGenerator>().sky.Count;
		skyLength = LevelCtrl.Instance.skyLength;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(speed, 0, 0) * Time.deltaTime);
		if(transform.position.x < -skyLength - 10){
			transform.Translate(new Vector3(-skyCount * skyLength, 0, 0));
		}
	}
}