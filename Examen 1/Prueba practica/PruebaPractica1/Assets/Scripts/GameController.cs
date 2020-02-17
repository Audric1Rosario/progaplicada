using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    private TextMesh LifeText;
    private TextMesh ScoreText;
    private GameObject GameOver;
    private CubeGenerator detener;
    public int lifes;
    public int score;
    void Start()
    {
        LifeText = GameObject.Find("Lifes").GetComponent<TextMesh>();
        ScoreText = GameObject.Find("Score").GetComponent<TextMesh>();
        GameOver = GameObject.Find("GameOver");
        detener = GameObject.Find("CubeGenerator").GetComponent<CubeGenerator>();
        LifeText.text = "Lifes: 3";
        ScoreText.text = "0";
        lifes = 3;
        score = 0;
        GameOver.SetActive(false);
    }

    internal void decreaseLife()
    {
        lifes = lifes - 1 < 0 ? 0 : lifes - 1;
        LifeText.text = $"Lifes: {lifes}";
        GameOver.SetActive(true);
        detener.go = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("update");
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Borrando");
            StartCoroutine("deleteCubes");
        }
    }

    private IEnumerator deleteCubes()
    {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");
        yield return null;

        for (int i = 0; i < cubes.Length; i++)
        {
            Debug.Log(cubes[i].GetComponent<Cube>().deletable);
            if (cubes[i].GetComponent<Cube>().deletable)
            {
                Destroy(cubes[i].gameObject);
                score++;
                ScoreText.text = score.ToString();
            }
            yield return 0.02f;
        }
    }
}

