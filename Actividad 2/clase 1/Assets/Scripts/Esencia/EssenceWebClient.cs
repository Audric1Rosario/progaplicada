using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class EssenceScore
{
    public int Id;
    public string PlayerName;
    public int BlueScore;
    public int GreenScore;
    public int OrangeScore;
    public int PurpleScore;
    public int RedScore;
    public int YellowScore;
}

public class EssenceWebClient : MonoBehaviour
{
    UnityWebRequest www;
    const string webserviceURL = "https://localhost:44364/api/Essence/5";    // Ej: http: //localhost.64198/api/values
    // Start is called before the first frame update
    private ScoreController scores;
    private void Awake()
    {
        scores = GetComponent<ScoreController>();
    }
    public void SaveScore()
    {
        StartCoroutine("SendScores");
    }

    public IEnumerator SendScores()
    {
        EssenceScore newScore = new EssenceScore();
        newScore.Id = 1;
        newScore.PlayerName = "Unity Player";
        newScore.BlueScore = scores.Scores[0];
        newScore.GreenScore = scores.Scores[1];
        newScore.OrangeScore = scores.Scores[2];
        newScore.PurpleScore = scores.Scores[3];
        newScore.RedScore = scores.Scores[4];
        newScore.YellowScore = scores.Scores[5];
        www = UnityWebRequest.Put(webserviceURL, JsonUtility.ToJson(newScore)); // Mi objeto esta en el servicio web
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        // JsonUtility.ToJson(); // Con esta función le mandas un objeto, y lo conbierte a una string formato json
        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);
    }
}
