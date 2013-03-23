using UnityEngine;
using System.Collections;

public class LevelCtrl {
	
	private LevelCtrl levelCtrl;
	public LevelCtrl Instance{
		get{
			if(levelCtrl.Equals(null)){
				levelCtrl = new LevelCtrl();
			}
			return levelCtrl;
		}
	}
	
	private Player player;
	private Crowd crowd;
	private float timeElapsed;
	
	public LevelCtrl(){
		
	}
}
