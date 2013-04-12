using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Boomlagoon.JSON;
using BlowFishCS;
using System.Text;

public class Register : MonoBehaviour, IObserver {

    private static string serviceURL = "http://innovaentretenimiento2013.info/InnovaServer/services/PlayerService.php";
    private string document = "", name = "", lastNames = "", email = "", institution = "";
    public WebServiceHelper ws;
    public GUISkin skin;
    public GUISkin rankingSkinDafuq;
    private bool acceptConditions = false;
    public AudioClip chicken;
    private int ranking;
    private bool registered = false, clearedData = true;
    public GameObject ticket;

    void Start() {
        if(!Player.GotTicket){
            ticket.renderer.material.color = Color.black;
            ticket.GetComponent<ParticleSystem>().enableEmission = false;
        }
        GameObject playerGo = GameObject.Find("Girl(Clone)");
        if(playerGo == null){
            playerGo = GameObject.Find("Boy(Clone)");
        }
        Destroy(playerGo);

        if(PlayerData.hasInstance()){
            JSONObject player = PlayerData.Data;
            document = player.GetString("document");
            name = player.GetString("name");
            lastNames = player.GetString("lastName");
            email = player.GetString("email");
            institution = player.GetString("institution");
            if(PlayerData.Data.GetNumber("score") < getScore()){
                PlayerData.Data.Remove("score");
                PlayerData.Data.Add("score", getScore());
            }
            clearedData = false;
        }
    }

    void OnGUI() {
        if(skin!=null){
            GUI.skin = skin;
        }

        #region
        int groupWidth = 4 * Screen.width/6;
        int groupHeight = Screen.height / 2;
        GUI.BeginGroup(new Rect(Screen.width/10,Screen.height/3 - groupHeight/2, groupWidth, groupHeight));
        GUI.Box(new Rect(0, 0, groupWidth, groupHeight), "");
        GUI.Label(new Rect(2*groupWidth/5,0,groupWidth/5, groupHeight/7), "FORMULARIO");
        GUI.Label(new Rect(groupWidth/10, groupHeight/7, 2 * groupWidth/5, groupHeight/7), "CEDULA"); // 1/10 de margen
        string documentInput = GUI.TextField(new Rect(groupWidth / 5 + groupWidth / 7, groupHeight / 7, 3 * groupWidth / 5 - groupWidth / 20 +10, groupHeight / (2 * 7) + groupHeight / (2 * 7*3)), document, 25);

        if (!document.Equals(documentInput) && !clearedData)
        {
            clearData();
        }
        document = documentInput;
        
        GUI.Label(new Rect(groupWidth / 10, 2 * groupHeight / 7, 2 * groupWidth / 5, groupHeight / 7), "NOMBRE"); // 1/10 de margen
        string nameInput = GUI.TextField(new Rect(groupWidth / 5 + groupWidth / 7, 2 * groupHeight / 7, 3 * groupWidth / 5 - groupWidth / 20 + 10, groupHeight / (2 * 7) + groupHeight / (2 * 7 * 3)), name, 30);
        if (!name.Equals(nameInput) && !clearedData)
        {
            clearData();
        }

        name = nameInput;
    
        GUI.Label(new Rect(groupWidth / 10, 3 * groupHeight / 7, 2 * groupWidth / 5, groupHeight / 7), "APELLIDOS"); // 1/10 de margen
        string lastNamesInput = GUI.TextField(new Rect(groupWidth / 5 + groupWidth / 7, 3 * groupHeight / 7, 3 * groupWidth / 5 - groupWidth / 20 + 10, groupHeight / (2 * 7) + groupHeight / (2 * 7 * 3)), lastNames, 30);
        if (!lastNames.Equals(lastNamesInput) && !clearedData)
        {
            clearData();
        }

        lastNames = lastNamesInput;
        
        GUI.Label(new Rect(groupWidth / 10, 4 * groupHeight / 7, 2 * groupWidth / 5, groupHeight / 7), "EMAIL"); // 1/10 de margen
        string emailInput = GUI.TextField(new Rect(groupWidth / 5 + groupWidth / 7, 4 * groupHeight / 7, 3 * groupWidth / 5 - groupWidth / 20 + 10, groupHeight / (2 * 7) + groupHeight / (2 * 7 * 3)), email, 256);
        if (!email.Equals(emailInput) && !clearedData)
        {
            clearData();
        }
        email = emailInput;

        GUI.Label(new Rect(groupWidth / 10, 5 * groupHeight / 7, 2 * groupWidth / 5, groupHeight / 7), "INSTITUCION"); // 1/10 de margen
        string institutionInput = GUI.TextField(new Rect(groupWidth / 5 + groupWidth / 7, 5 * groupHeight / 7, 3 * groupWidth / 5 - groupWidth / 20 + 10, groupHeight / (2 * 7) + groupHeight / (2 * 7 * 3)), institution, 20);
        if (!institution.Equals(institutionInput) && !clearedData)
        {
            clearData();
        }
        institution = institutionInput;
     
        #endregion
        if (!registered)
        {
            if (GUI.Button(new Rect(groupWidth / 10, 6 * groupHeight / 7, 2 * groupWidth / 5, groupHeight / (2 * 7)), "SUBMIT SCORE"))
            {
                if (checkValidInput())
                {
                    registerPlayer();
                    getPlayerRanking();
                    registered = true;
                }
                else
                {
                    playChicken();
                }
            }
            if (GUI.Button(new Rect(groupWidth / 10 + 2 * groupWidth / 5  + 10, 6 * groupHeight / 7, 2 * groupWidth / 5, groupHeight / (2 * 7)), "TRY AGAIN"))
            {
                Application.LoadLevel("CharacterSelection");
            }
        }
        else 
        {
            if (GUI.Button(new Rect(groupWidth / 10, 6 * groupHeight / 7, 4 * groupWidth / 5, groupHeight / (2 * 7)), "TRY AGAIN")) {
                Application.LoadLevel("CharacterSelection");
            }
        }

        GUI.EndGroup();
        int group2Width = 4*Screen.width/5, group2Height = Screen.height/4;
        GUI.BeginGroup(new Rect(Screen.width/10, 3*Screen.height/4 - Screen.height/7, group2Width, group2Height));
        GUI.Box(new Rect(0, 0, group2Width, group2Height), "");
        GUI.Label(new Rect(3*group2Width/7,0, group2Width/4, group2Height/6), "CONDICIONES");
        string text = "\t6to congreso innova entretenimiento 2013 - (Certificación internacional).\n*\tLa entrada no es transferible bajo ningún motivo.\n*\tLa entrada se sorteará entre los 25 mejores puntajes.\n*\tEl equipo de trabajo se reserva el derecho de anular participantes sin previo aviso.";
        GUI.TextArea(new Rect(group2Width/16, group2Height/6, group2Width-group2Width/8, group2Height/2+group2Height/12), text);
        acceptConditions = GUI.Toggle(new Rect(group2Width/16, 3*group2Height/4, group2Width-group2Width/8, group2Height/8+group2Height/24), acceptConditions, "Acepto estas condiciones.");
        GUI.EndGroup();

        GUI.BeginGroup(new Rect(groupWidth + Screen.width / 10 + 10, Screen.height / 3 - groupHeight / 2, Screen.width / 5, groupHeight / 2), "");
        GUI.Box(new Rect(0, 0, Screen.width / 7 + 20, groupHeight / 4), "");
        if (registered)
        {
            GUI.Label(new Rect(15, 0, group2Width - groupWidth - 10, groupHeight / (2 * 4)), "RANKING");
        }
        else 
        {
            GUI.Label(new Rect(15, 0, group2Width - groupWidth - 10, groupHeight / (2 * 4)), "SCORE");
        }

        if (rankingSkinDafuq != null) {
            GUI.skin = rankingSkinDafuq;
        }

        if (registered)
        {
            string rank = (ranking != 0) ? ranking.ToString() : "";
            GUI.TextField(new Rect(10, (groupHeight / (2 * 4) -5), Screen.width / 7, Screen.height / 16), rank);
        }
        else 
        {
            GUI.TextField(new Rect(10, (groupHeight / (2 * 4) -5), Screen.width / 7, Screen.height / 16), ((int)getScore()).ToString());
        }
        GUI.EndGroup();
    }

    private void clearData()
    {
        Debug.Log("YEAH");
        PlayerData.clearData();
        document = "";
        name = "";
        lastNames = "";
        email = "";
        institution = "";
        clearedData = true;
    }

    private void playChicken()
    {
        audio.Stop();
        audio.loop = false;
        audio.clip = chicken;
        audio.Play();
    }

    private bool checkValidInput()
    {
        return acceptConditions && document.Trim() != "" && name.Trim() != "" && lastNames.Trim() != "" && isEmail(email) && institution.Trim() != "";
    }

    private bool isEmail(string email) { 
        Regex r = new Regex("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?.)+(?:[A-Z]{2}|com|org|net|edu|gov|mil|biz|info|mobi|name|aero|asia|jobs|museum|co|es)\\b");
        return r.IsMatch(email);
    }

    private void registerPlayer() {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        JSONObject playerData;
        if(PlayerData.hasInstance()){
            playerData = PlayerData.Data;
        }else{
            playerData = generateJsonFormat(document, name, lastNames, email, institution);
            PlayerData.Data = playerData;
        }
        string player = encrypt(playerData.ToString());
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
        return Player.Score;
    }

    public void UpdateObserver(Observable target)
    {
        Queue<string> responses = ((WebServiceHelper)target).Responses[this]; 
        while(responses.Count > 0){
            string response = responses.Dequeue();
            //Debug.Log(response);
            JSONObject rankingObj = JSONObject.Parse(response);
            if(rankingObj!=null && rankingObj.ContainsKey("ranking")){
                this.ranking = int.Parse(rankingObj.GetValue("ranking").Str);
            }
        }
    }
}
