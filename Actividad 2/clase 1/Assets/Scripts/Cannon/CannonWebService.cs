using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class CannonWebService : MonoBehaviour
{
    [Serializable]
    public class CannonTable        // Clase proxy
    {
        public int Id;                 // Clase intermedia, entre lo que esta en el modelo y la aplicacion.
        public string PlayerName;
        public double Score;
    }
    UnityWebRequest www;
    const string webserviceURL = "https://localhost:44374/api/values/5";    // Ej: http: //localhost.64198/api/values

    public void SaveScore(int puntaje)
    {
        StartCoroutine(SendWebRequest("Unity Player", puntaje));
    }

    public IEnumerator SendWebRequest(string Nombre, int Puntaje)
    {
        CannonTable newScore = new CannonTable();
        newScore.Id = 0;
        newScore.PlayerName = Nombre;
        newScore.Score = Puntaje;
        www = UnityWebRequest.Put(webserviceURL, JsonUtility.ToJson(newScore)); // Mi objeto esta en el servicio web
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        // JsonUtility.ToJson(); // Con esta función le mandas un objeto, y lo conbierte a una string formato json
        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);
    }
}
