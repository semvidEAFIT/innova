using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

    public GameObject player, jumpable, slidable, chicken;
    public float pacingBetweenShots = 5.0f;
    private float elapsedTime = 0.0f;
    private int stage = 0;
    public Transform[] targets;
    public Transform spawnPoint;
    private PlayerTutorial playerT;

    void Start() {
        playerT = player.GetComponent<PlayerTutorial>();
        playerT.enabled = true;
    }

    void Update() {
        switch (stage) {
            case 0:
                if (playerT.Target == null)
                {
                    playerT.Target = targets[0];
                    stage++;
                }
                break;
            case 1:
                if (playerT.Target == null)
                {
                    jumpable = Instantiate(jumpable, spawnPoint.position, jumpable.transform.rotation) as GameObject;
                    playerT.Status1 = PlayerTutorial.Status.Jump;
                    playerT.Target = jumpable.transform;
                    stage++;
                }
                break;
            
        }
    }
}
