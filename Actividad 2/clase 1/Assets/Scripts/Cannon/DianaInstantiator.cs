using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eCannonState
{
    playing,
    stopped
}

public class DianaInstantiator : MonoBehaviour
{
    public GameObject diana;
    const float _MINXAPPARITION = -2f, _MAXXAPPARITION = 8f,
        _MINYAPPARITION = -2f, _MAXYAPPARITION = 5f;

    // Estado del juego
    public eCannonState myGame;

    // Objetos del juego
    public TextMesh message;
    public TextMesh counter;

    // Contadores
    int _dianaCount = 0;
    int _dianaMin = 1;
    int _dianaMax = 5;
    int score = 0;
    const string messageFinish = "Presione ENTER para terminar su partida.";
    const string messageRepeat = "Presione R para volver a iniciar";

    // WebService
    CannonWebService _web;

    private void Start()
    {
        myGame = eCannonState.playing;
        message.text = messageFinish;
        counter.text = score.ToString();
        _web = GetComponent<CannonWebService>();
    }

    private void Update()
    {
        if (myGame.Equals(eCannonState.playing))
        {
            verifyDianaQuantity();
            // Para terminar el juego presione enter
            if (Input.GetKeyDown(KeyCode.Return))
            {
                endLevel();
            }
        } else
        {
            // Para volver a empezar el luego presione R
            if (Input.GetKeyDown(KeyCode.R))
            {
                // Reiniciar nivel
                resetLevel();
            }

        }
    }

    void resetLevel()
    {
        // Buscar todas las dianas.
        GameObject[] group = GameObject.FindGameObjectsWithTag("target");
        foreach (GameObject ob in group)
        {
            Destroy(ob.gameObject);
        }
        message.text = messageRepeat;
        myGame = eCannonState.playing;
    }

    void endLevel()
    {
        // Aqui se envian los datos al webservice
        _web.SaveScore(score);

        // Reiniciar todo.
        _dianaCount = 0;
        score = 0;
        message.text = messageRepeat;
        counter.text = score.ToString();
        
        myGame = eCannonState.stopped;
    }

    void verifyDianaQuantity()
    {
        if (_dianaCount < _dianaMin)
        {
            int i;
            for (i = _dianaCount; i <= _dianaMax; i++)
            {
                // Instanciar dianas segun se necesiten.
                Instantiate(diana, new Vector3(Random.Range(_MINXAPPARITION, _MAXXAPPARITION), Random.Range(_MINYAPPARITION, _MAXYAPPARITION), 0),
                Quaternion.identity);
            }
            _dianaCount += i - _dianaCount;
        }
    }

    public void deleteDiana()
    {
        _dianaCount--;
        score++;
        counter.text = score.ToString();

    }
}
