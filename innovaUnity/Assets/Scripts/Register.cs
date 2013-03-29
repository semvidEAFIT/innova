using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using JSONSharp;
using JSONSharp.Collections;
using JSONSharp.Values;

public class Register : MonoBehaviour {

    private static string serviceURL = "http://localhost/InnovaServer/services/PlayerService.php";
    private string document = "", name = "", lastNames = "", email = "", institution = "";

    void OnGUI() {
        int groupWidth = Screen.width / 3;
        int groupHeight = Screen.height / 2;
        GUI.BeginGroup(new Rect(Screen.width/3,Screen.height/4, groupWidth, groupHeight));
        GUI.Box(new Rect(0, 0, groupWidth, groupHeight), "");
        GUI.Label(new Rect(groupWidth/10, groupHeight/7, 2 * groupWidth/5, groupHeight/7), "Cédula"); // 1/10 de margen
        document = GUI.TextField(new Rect(groupWidth / 5 + groupWidth / 7, groupHeight / 7, 3 * groupWidth / 5 - groupWidth / 20, groupHeight / (2 * 7)), document, 11);
        GUI.Label(new Rect(groupWidth / 10, 2 * groupHeight / 7, 2 * groupWidth / 5, groupHeight / 7), "Nombre"); // 1/10 de margen
        name = GUI.TextField(new Rect(groupWidth / 5 + groupWidth / 7, 2 * groupHeight / 7, 3 * groupWidth / 5 - groupWidth / 20, groupHeight / (2 * 7)), name, 30);
        GUI.Label(new Rect(groupWidth / 10, 3 * groupHeight / 7, 2 * groupWidth / 5, groupHeight / 7), "Apellidos"); // 1/10 de margen
        lastNames = GUI.TextField(new Rect(groupWidth / 5 + groupWidth / 7, 3 * groupHeight / 7, 3 * groupWidth / 5 - groupWidth / 20, groupHeight / (2 * 7)), lastNames, 30);
        GUI.Label(new Rect(groupWidth / 10, 4 * groupHeight / 7, 2 * groupWidth / 5, groupHeight / 7), "Email"); // 1/10 de margen
        email = GUI.TextField(new Rect(groupWidth / 5 + groupWidth / 7, 4 * groupHeight / 7, 3 * groupWidth / 5 - groupWidth / 20, groupHeight / (2 * 7)), email, 20);
        GUI.Label(new Rect(groupWidth / 10, 5 * groupHeight / 7, 2 * groupWidth / 5, groupHeight / 7), "Institución"); // 1/10 de margen
        institution = GUI.TextField(new Rect(groupWidth / 5 + groupWidth / 7, 5 * groupHeight / 7, 3 * groupWidth / 5 - groupWidth / 20, groupHeight / (2 * 7)), institution, 20);
        if (GUI.Button(new Rect(groupWidth / 10, 6 * groupHeight / 7, 4 * groupWidth / 5,  groupHeight / (2 * 7)), "Register"))
        {
            //Se debe programar una manera de validar las entradas del usuario quizá usando RegEx
            if (document.Trim() != "" && name.Trim() != "" && lastNames.Trim() != "" && email.Trim() != "" && institution.Trim() != "")
            {
                registerPlayer();   
            }
        }
        GUI.EndGroup();

    }

    private void registerPlayer()
    {
        JSONObjectCollection json = generateJsonFormat(document, name, lastNames, email, institution);
        WebRequest request = WebRequest.Create(serviceURL);
        request.Method = "POST";
        string postData = "Player=" + json.ToString().Replace(' ', '+');
        Debug.Log("PostData: "+postData);
        byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        request.ContentType = "application/json";
        request.ContentLength = byteArray.Length;
        
        Stream dataStream = request.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Flush();
        dataStream.Close();

        WebResponse response = request.GetResponse();
        dataStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(dataStream);

        string responseFromServer = reader.ReadToEnd();
        Debug.Log(responseFromServer);

        reader.Close();
        dataStream.Close();
        response.Close();
    }

    private JSONObjectCollection generateJsonFormat(string document, string name, string lastNames, string email, string institution)
    {
        Dictionary<JSONStringValue, JSONValue> jsonKeyValuePairs = new Dictionary<JSONStringValue, JSONValue>();

        #region Keys & Values initialization
        JSONStringValue idKey = new JSONStringValue("id");
        JSONStringValue documentKey = new JSONStringValue("document");
        JSONStringValue nameKey = new JSONStringValue("name");
        JSONStringValue lastNamesKey = new JSONStringValue("lastNames");
        JSONStringValue emailKey = new JSONStringValue("email");
        JSONStringValue institutionKey = new JSONStringValue("institution");
        JSONStringValue scoreKey = new JSONStringValue("score");
        JSONStringValue playCountKey = new JSONStringValue("playCount");
        JSONStringValue lastDateKey = new JSONStringValue("lastDate");

        JSONNumberValue idValue = new JSONNumberValue(0);
        JSONStringValue documentValue = new JSONStringValue(document);
        JSONStringValue nameValue = new JSONStringValue(name);
        JSONStringValue lastNamesValue = new JSONStringValue(lastNames);
        JSONStringValue emailValue = new JSONStringValue(email);
        JSONStringValue institutionValue = new JSONStringValue(institution);
        JSONNumberValue scoreValue = new JSONNumberValue(getScore());
        JSONNumberValue playCountValue = new JSONNumberValue(1);
        JSONStringValue lastDateValue = new JSONStringValue("");

        #endregion

        jsonKeyValuePairs.Add(idKey, idValue);
        jsonKeyValuePairs.Add(documentKey, documentValue);
        jsonKeyValuePairs.Add(nameKey, nameValue);
        jsonKeyValuePairs.Add(lastNamesKey, lastNamesValue);
        jsonKeyValuePairs.Add(emailKey, emailValue);
        jsonKeyValuePairs.Add(institutionKey, institutionValue);
        jsonKeyValuePairs.Add(scoreKey, scoreValue);
        jsonKeyValuePairs.Add(playCountKey, playCountValue);
        jsonKeyValuePairs.Add(lastDateKey, lastDateValue);

        JSONObjectCollection playerJsonObject = new JSONObjectCollection(jsonKeyValuePairs);

        return playerJsonObject;
    }

    private int getScore()
    {
        return 42;
    }

}
