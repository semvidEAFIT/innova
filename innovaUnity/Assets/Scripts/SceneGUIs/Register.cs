using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Boomlagoon.JSON;
using BlowFishCS;
using System.Text;

public class Register : MonoBehaviour, IObserver {

    private static string serviceURL = "http://localhost/InnovaServer/services/PlayerService.php";
    private string document = "1127235505", name = "Rodrigo", lastNames = "Diaz Bermudez", email = "pericodiaz89@gmail.com", institution = "SemVid EAFIT";
    public WebServiceHelper ws;
    public GUISkin skin;

    void OnGUI() {
        if(skin!=null){
            GUI.skin = skin;
        }
        int groupWidth = Screen.width / 3;
        int groupHeight = Screen.height / 2;
        GUI.BeginGroup(new Rect(Screen.width/3,Screen.height/4, groupWidth, groupHeight));
        GUI.Box(new Rect(0, 0, groupWidth, groupHeight), "");
        GUI.Label(new Rect(groupWidth/10, groupHeight/7, 2 * groupWidth/5, groupHeight/7), "Cédula"); // 1/10 de margen
        document = GUI.TextField(new Rect(groupWidth / 5 + groupWidth / 7, groupHeight / 7, 3 * groupWidth / 5 - groupWidth / 20, groupHeight / (2 * 7)), document, 15);
        GUI.Label(new Rect(groupWidth / 10, 2 * groupHeight / 7, 2 * groupWidth / 5, groupHeight / 7), "Nombre"); // 1/10 de margen
        name = GUI.TextField(new Rect(groupWidth / 5 + groupWidth / 7, 2 * groupHeight / 7, 3 * groupWidth / 5 - groupWidth / 20, groupHeight / (2 * 7)), name, 30);
        GUI.Label(new Rect(groupWidth / 10, 3 * groupHeight / 7, 2 * groupWidth / 5, groupHeight / 7), "Apellidos"); // 1/10 de margen
        lastNames = GUI.TextField(new Rect(groupWidth / 5 + groupWidth / 7, 3 * groupHeight / 7, 3 * groupWidth / 5 - groupWidth / 20, groupHeight / (2 * 7)), lastNames, 30);
        GUI.Label(new Rect(groupWidth / 10, 4 * groupHeight / 7, 2 * groupWidth / 5, groupHeight / 7), "Email"); // 1/10 de margen
        email = GUI.TextField(new Rect(groupWidth / 5 + groupWidth / 7, 4 * groupHeight / 7, 3 * groupWidth / 5 - groupWidth / 20, groupHeight / (2 * 7)), email, 256);
        GUI.Label(new Rect(groupWidth / 10, 5 * groupHeight / 7, 2 * groupWidth / 5, groupHeight / 7), "Institución"); // 1/10 de margen
        institution = GUI.TextField(new Rect(groupWidth / 5 + groupWidth / 7, 5 * groupHeight / 7, 3 * groupWidth / 5 - groupWidth / 20, groupHeight / (2 * 7)), institution, 20);
        if (GUI.Button(new Rect(groupWidth / 10, 6 * groupHeight / 7, 4 * groupWidth / 5,  groupHeight / (2 * 7)), "Register"))
        {
            //Se debe programar una manera de validar las entradas del usuario quizá usando RegEx
            if (checkValidInput())
            {
                registerPlayer();
                getPlayerRanking();
            }else{
                Debug.Log("Nope");
            }
        }
        GUI.EndGroup();
    }

    private bool checkValidInput()
    {
        return document.Trim() != "" && name.Trim() != "" && lastNames.Trim() != "" && isEmail(email) && institution.Trim() != "";
    }

    private bool isEmail(string email) { 
        Regex r = new Regex("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?.)+(?:[A-Z]{2}|com|org|net|edu|gov|mil|biz|info|mobi|name|aero|asia|jobs|museum)\\b");
        return r.IsMatch(email);
    }

    private void registerPlayer() {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        string player = encrypt(generateJsonFormat(document, name, lastNames, email, institution).ToString());
        parameters.Add("Player", player);
        parameters.Add("Service", Service.Register);
        ws.callServicePost(serviceURL, parameters, this);   
    }

    private string encrypt(string part)
    {
        string hexKey = "04B915BA43FEB5B6";
        BlowFish b = new BlowFish(hexKey);
        string encryptedData = b.Encrypt_CBC(part);
        return encryptedData;
    }

    private void getPlayerRanking() {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add("document", document);
        parameters.Add("Service", Service.PlayerRanking);
        ws.callServiceGet(serviceURL, parameters, this);
    }

    private JSONObject generateJsonFormat(string document, string name, string lastNames, string email, string institution)
    {
        JSONObject player = new JSONObject();
        player.Add("id", "");
        player.Add("document", document);
        player.Add("name", name);
        player.Add("lastName", lastNames);
        player.Add("email", email);
        player.Add("institution", institution);
        player.Add("score", getScore());
        player.Add("playCount", 1);
        player.Add("lastDate", "");
        return player;
    }

    private float getScore()
    {
        return Player.getScore();
    }

    public void UpdateObserver(Observable target)
    {
        Queue<string> responses = ((WebServiceHelper)target).Responses[this]; 
        while(responses.Count > 0){
            Debug.Log(responses.Dequeue());
        }
    }
}
