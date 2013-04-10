using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{

    public GameObject[] slides;
    public float timeToChange;

    public TextMesh[] scripts;

    private float timeElapsed;
    private int curSlide;

    private bool firstTime;
    private string[] scriptsDialogs = { "Que hace toda esa gente reunida?", "\"Se agotan las entradas para el congreso de\nentretenimiento innova, consigue la tuya ahora,\nYo voy a ir... ¿y tu?\"", "Necesito conseguir mi entrada antes\nque toda esta gente las agote!" };
    private Vector3 dir;
    private bool moving;
    private float velocity;
    private Vector3 deltaTransform;
    public float typeSpeed = 0.5f;
    private float elapsedTime = 0.0f;
    public float distanceToZoom = 10;
    public float toleranceToSnap = 0.3f;
    private int index = 0, currentChar = 0;
    //public variable to multiply volocity from the editor
    public float speed = 1.0f;

    // Use this for initialization
    void Start()
    {
        timeElapsed = 0;
        curSlide = 0;
        firstTime = true;
        moving = false;
        speed = 1;
        NextSlide();
        speed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        //timeElapsed+=Time.deltaTime;
        //if (Input.anyKeyDown || timeElapsed>=timeToChange){
        elapsedTime += Time.deltaTime;
        if (elapsedTime > typeSpeed)
        {
            if (index <= scripts.Length && index > 0 && currentChar < scriptsDialogs[index - 1].Length)
            {
                currentChar++;
                scripts[index - 1].text = scriptsDialogs[index - 1].Substring(0, currentChar);
                elapsedTime = 0.0f;
            }
        }
        Debug.DrawLine(transform.position, dir * 100, Color.white);
        Debug.DrawLine(new Vector3(0, 0, 0), transform.position, Color.red);

        if (firstTime && curSlide > 0)
        {
            if (camera.orthographicSize > distanceToZoom)
            {
                camera.orthographicSize -= Mathf.Abs(camera.orthographicSize - distanceToZoom) / 10;
            }
            else camera.orthographicSize = distanceToZoom;
        }

        if (moving)
        {
            deltaTransform.x = transform.position.x - slides[curSlide - 1].transform.position.x;
            deltaTransform.y = transform.position.y - slides[curSlide - 1].transform.position.y;
            velocity = Time.deltaTime * deltaTransform.magnitude * speed;

            dir = -transform.position + slides[curSlide - 1].transform.position;
            dir = dir.normalized;
            dir.z = 0;

            transform.Translate(velocity * dir);

            if (deltaTransform.magnitude < toleranceToSnap)
            {
                transform.position = new Vector3(slides[curSlide - 1].transform.position.x, slides[curSlide - 1].transform.position.y, 0);
                moving = false;
            }
        }

        if (Input.anyKeyDown)
        {
            if (curSlide < slides.Length)
            {
                //timeElapsed=0;
                NextSlide();
            }
            else
            {
                //change scene
                Application.LoadLevel("CharacterSelection");
            }
        }
    }

    private void NextSlide()
    {
        //		if (firstTime) {
        //			camera.orthographicSize=10;
        //			firstTime=false;
        //		}
        //		if (curSlide==3){
        //			camera.orthographicSize=12;
        //			transform.position = new Vector3 (slides[curSlide].transform.position.x, slides[curSlide].transform.position.y+1, 0) ;
        //		} else {
        //			transform.position = new Vector3 (slides[curSlide].transform.position.x, slides[curSlide].transform.position.y + 3, 0) ;
        //		}

        setFullText();
        index++;
        dir = -transform.position + slides[curSlide].transform.position;
        dir = dir.normalized;
        dir.z = 0;

        moving = true;

        curSlide++;
    }

    private void setFullText()
    {
        if (index > scripts.Length || index == 0) return;
        scripts[index - 1].text = scriptsDialogs[index - 1];
        currentChar = 0;
    }
}
