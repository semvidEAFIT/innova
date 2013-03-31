using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Boomlagoon.JSON;

public class Register : MonoBehaviour {

    private static string serviceURL = "http://localhost/InnovaServer/services/PlayerService.php";
    private string document = "1127235505", name = "Rodrigo", lastNames = "Diaz Bermudez", email = "pericodiaz89@gmail.com", institution = "SemVid EAFIT";

    void OnGUI() {
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
            if (document.Trim() != "" && name.Trim() != "" && lastNames.Trim() != "" && email.Trim() != "" && institution.Trim() != "")
            {
                registerPlayer();
                getPlayerRanking();
            }
        }
        GUI.EndGroup();
    }

    private void registerPlayer() {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        JSONObject json = generateJsonFormat(document, name, lastNames, email, institution);
        parameters.Add("Player", json);
        parameters.Add("Service", PostService.Register);
        string responseString = WebServiceHelper.callServicePost(serviceURL, parameters);
        Debug.Log(responseString);    
    }

    private void getPlayerRanking() {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add("document", document);
        parameters.Add("Service", GetService.PlayerRanking);
        string responseString = WebServiceHelper.callServiceGet(serviceURL, parameters);
        JSONObject responseJson = JSONObject.Parse(responseString);
        int ranking = int.Parse(responseJson.GetString("ranking"));
        Debug.Log(ranking); 
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

    private int getScore()
    {
        return 42;
    }

    private enum PostService
    {
        Register
    }

    private enum GetService
    { 
        Ranking, List, PlayerRanking
    }
}
