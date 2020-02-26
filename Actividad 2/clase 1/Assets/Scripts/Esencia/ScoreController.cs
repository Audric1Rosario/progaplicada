using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    // Start is called before the first frame update
    public int[] Scores = new int[6];
    public TextMesh BlueScoreText, GreenScoreText, OrangeScoreText,
                      PurpleScoreText, RedScoreText, YellowScoreText;
    public void IncrementScore(EssenceType color)
    {
        Scores[(int)color]++;
        switch (color)
        {
            case EssenceType.Blue:
                BlueScoreText.text = Scores[(int)color].ToString();
                break;

            case EssenceType.Green:
                GreenScoreText.text = Scores[(int)color].ToString();
                break;

            case EssenceType.Orange:
                OrangeScoreText.text = Scores[(int)color].ToString();
                break;

            case EssenceType.Purple:
                PurpleScoreText.text = Scores[(int)color].ToString();
                break;

            case EssenceType.Red:
                RedScoreText.text = Scores[(int)color].ToString();
                break;

            case EssenceType.Yellow:
                YellowScoreText.text = Scores[(int)color].ToString();
                break;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
