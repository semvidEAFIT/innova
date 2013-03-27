using UnityEngine;
using System.Collections;

public class Register : MonoBehaviour {

    void OnGUI() {
        int groupWidth = Screen.width / 3;
        int groupHeight = Screen.height / 2;
        GUI.BeginGroup(new Rect(Screen.width/3,Screen.height/4, groupWidth, groupHeight));
        GUI.Box(new Rect(0, 0, groupWidth, groupHeight), "");
        GUI.Label(new Rect(groupWidth/10, groupHeight/7, 2 * groupWidth/5, groupHeight/7), "Cédula"); // 1/10 de margen
        string document = GUI.TextField(new Rect(groupWidth / 5 + groupWidth / 7, groupHeight / 7, 3 * groupWidth / 5 - groupWidth / 20, groupHeight / (2 * 7)), "");
        GUI.Label(new Rect(groupWidth / 10, 2 * groupHeight / 7, 2 * groupWidth / 5, groupHeight / 7), "Nombre"); // 1/10 de margen
        string name = GUI.TextField(new Rect(groupWidth / 5 + groupWidth / 7, 2 * groupHeight / 7, 3 * groupWidth / 5 - groupWidth / 20, groupHeight / (2 * 7)), "");
        GUI.Label(new Rect(groupWidth / 10, 3 * groupHeight / 7, 2 * groupWidth / 5, groupHeight / 7), "Apellidos"); // 1/10 de margen
        string lastNames = GUI.TextField(new Rect(groupWidth / 5 + groupWidth / 7, 3 * groupHeight / 7, 3 * groupWidth / 5 - groupWidth / 20, groupHeight / (2 * 7)), "");
        GUI.Label(new Rect(groupWidth / 10, 4 * groupHeight / 7, 2 * groupWidth / 5, groupHeight / 7), "Email"); // 1/10 de margen
        string email = GUI.TextField(new Rect(groupWidth / 5 + groupWidth / 7, 4 * groupHeight / 7, 3 * groupWidth / 5 - groupWidth / 20, groupHeight / (2 * 7)), "");
        GUI.Label(new Rect(groupWidth / 10, 5 * groupHeight / 7, 2 * groupWidth / 5, groupHeight / 7), "Institución"); // 1/10 de margen
        string institution = GUI.TextField(new Rect(groupWidth / 5 + groupWidth / 7, 5 * groupHeight / 7, 3 * groupWidth / 5 - groupWidth / 20, groupHeight / (2 * 7)), "");
        if (GUI.Button(new Rect(groupWidth / 10, 6 * groupHeight / 7, 4 * groupWidth / 5,  groupHeight / (2 * 7)), "Register"))
        {
            Debug.Log("Register");
        }
        GUI.EndGroup();

    }
}
