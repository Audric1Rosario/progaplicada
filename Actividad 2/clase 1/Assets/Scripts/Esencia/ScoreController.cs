using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    // Start is called before the first frame update
    private int[] _scores = new int[6];
    public TextMesh BlueScoreText, GreenScoreText, OrangeScoreText,
                      PurpleScoreText, RedScoreText, YellowScoreText;
    public void IncrementScore(EssenceType color)
    {
        _scores[(int)color]++;
        switch (color)
        {
            case EssenceType.Blue:
                BlueScoreText.text = _scores[(int)color].ToString();
                break;

            case EssenceType.Green:
                GreenScoreText.text = _scores[(int)color].ToString();
                break;

            case EssenceType.Orange:
                OrangeScoreText.text = _scores[(int)color].ToString();
                break;

            case EssenceType.Purple:
                PurpleScoreText.text = _scores[(int)color].ToString();
                break;

            case EssenceType.Red:
                RedScoreText.text = _scores[(int)color].ToString();
                break;

            case EssenceType.Yellow:
                YellowScoreText.text = _scores[(int)color].ToString();
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
