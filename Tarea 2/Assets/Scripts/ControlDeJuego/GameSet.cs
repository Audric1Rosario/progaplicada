using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSet : MonoBehaviour
{
    // Start is called before the first frame update
    public int level;
    public GameObject winSprite;
    public GameObject loseSprite;
    public GameObject Player;
    public GameObject Bot;
    WebService summitScore;
    bool isFinish, playerWon;
    TextMesh write;
    ParabolicThrow _player;
    AIParabolic _bot;
    void Start()
    {
        isFinish = false;
        winSprite.SetActive(false);
        loseSprite.SetActive(false);
        write = GetComponent<TextMesh>();
        _player = Player.GetComponentInChildren<ParabolicThrow>();
        _bot = Bot.GetComponentInChildren<AIParabolic>();
        _player.attackDone = false;
        _bot.canAttack = false;
        write.text = "Es tu turno";

        // Web Service
        summitScore = GetComponent<WebService>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFinish)
        {
            _bot.canAttack = false;
            _player.canAttack = false;
            return;
        }

        // Turnar a los jugadores.
        if (_player.attackDone && !isFinish)
        {
            _bot.canAttack = true;
            _player.canAttack = false;
            _player.attackDone = false;
            StartCoroutine("Esperar");
        }        

        if (Bot.GetComponent<CharacterStatus>().life <= 0f)
        {
            winSprite.SetActive(true);            
            isFinish = true;
            playerWon = true;
            StartCoroutine("fin");
        }

        if (Player.GetComponent<CharacterStatus>().life <= 0f)
        {
            loseSprite.SetActive(true);
            isFinish = true;
            playerWon = false;
            StartCoroutine("fin");
        }
    }

    IEnumerator Esperar()
    {
        yield return new WaitForSeconds(3f);
        if (!isFinish)
            write.text = "Turno del contrincante";
        StartCoroutine("computadora");
    }

    IEnumerator computadora()
    {
        _bot.trigger = true;
        yield return new WaitForSeconds(3f);
        _bot.canAttack = false;
        _player.canAttack = true;
        if (!isFinish)
            write.text = "Es tu turno";
    }

    IEnumerator fin()
    {
        write.text = "Puntaje total: " + Bot.GetComponent<CharacterStatus>().LostLife().ToString();

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 3; i++)
        {
            if (playerWon)
                winSprite.SetActive(false);
            else
                loseSprite.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            if (playerWon)
                winSprite.SetActive(true);
            else
                loseSprite.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
        // Enviar los datos.
        StartCoroutine(summitScore.SendWebRequest("Unity Player", level, (int)Bot.GetComponent<CharacterStatus>().LostLife()));
    }
}
