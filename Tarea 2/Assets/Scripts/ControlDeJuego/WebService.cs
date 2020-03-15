using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class WebService : MonoBehaviour
{
    [Serializable]
    public class ArcheryScore        // Clase proxy
    {
        public int Id;                 // Clase intermedia, entre lo que esta en el modelo y la aplicacion.
        public string Nombre;
        public int Tipo;
        public int Puntaje;
    }
    UnityWebRequest www;
    const string webserviceURL = "https://localhost:44383/api/values/5";    // Ej: http: //localhost.64198/api/values

    // Update is called once per frame
    public IEnumerator SendWebRequest(string PlayerName, int nivel, int puntaje)
    {
        ArcheryScore newScore = new ArcheryScore();
        newScore.Id = 0;
        newScore.Nombre = PlayerName;
        newScore.Tipo = nivel;
        newScore.Puntaje = puntaje;
        www = UnityWebRequest.Put(webserviceURL, JsonUtility.ToJson(newScore)); // Mi objeto esta en el servicio web
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        // JsonUtility.ToJson(); // Con esta función le mandas un objeto, y lo conbierte a una string formato json
        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);
    }
}
