using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class WebService : MonoBehaviour
{
    [Serializable]
    public class GravilotaScore        // Clase proxy
    {
        public int Id;                 // Clase intermedia, entre lo que esta en el modelo y la aplicacion.
        public string PlayerName;
        public int NumActions;
    }
    UnityWebRequest www;
    const string webserviceURL = "https://localhost:44395/api/values/5";    // Ej: http: //localhost.64198/api/values

    // Update is called once per frame
    public IEnumerator SendWebRequest(string PlayerName, int NumActions)
    {
        GravilotaScore newScore = new GravilotaScore();
        newScore.Id = 0;
        newScore.PlayerName = PlayerName;
        newScore.NumActions = NumActions;
        www = UnityWebRequest.Put(webserviceURL, JsonUtility.ToJson(newScore)); // Mi objeto esta en el servicio web
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        // JsonUtility.ToJson(); // Con esta función le mandas un objeto, y lo conbierte a una string formato json
        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);
    }
}
