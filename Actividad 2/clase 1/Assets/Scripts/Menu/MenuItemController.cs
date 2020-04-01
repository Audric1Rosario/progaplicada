using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Scripts.Entities;

public class MenuItemController : MonoBehaviour
{
    // const float
    const float _HOVERSCALEFACTOR = 1.4f;
    MenuController _menuController;
    private void Awake()
    {
        _menuController = GameObject.Find("GlobalScriptsText").GetComponent<MenuController>();
    }
    public void OnMouseEnter()
    {
        AudioManager.Instance.PlaySoundEffect(AudioManager.SoundEffect.Explote);
        gameObject.transform.localScale *= _HOVERSCALEFACTOR;
    }

    public void OnMouseExit()
    {
        gameObject.transform.localScale /= _HOVERSCALEFACTOR;
    }

    public void OnMouseUp()
    {
        if (_menuController.isCanvasActive())
            return;
        AudioManager.Instance.PlaySoundEffect(AudioManager.SoundEffect.Capture);
        StartCoroutine(delaySelection());
    }

    IEnumerator delaySelection()
    {

        yield return new WaitForSeconds(1f);
        switch (gameObject.name)
        {
            case "PlayItem":
                SceneManager.LoadScene("ExplorationLevel");
                break;
            case "OptionItem":
                _menuController.UpdateOptionsGUI();
                _menuController.ShowGameOptions();
                break;
            case "ExitItem":
                Application.Quit();
                break;
        }
    }

    public void OkDialog()
    {
        _menuController.HideGameOptions();
    }

    public void CancelDialog()
    {
        Game.CurrentGame.LoadCurrentState();
        _menuController.HideGameOptions();
    }

    public void OnPlayerNameChanged(InputField input)
    {
        Game.CurrentGame.PlayerName = input.text;
    }

    public void OnMusicVolumeChanged(Slider slider)
    {
        Game.CurrentGame.MusicVolume = slider.value;
    }

    public void OnEffectMusicChanged(Slider slider) 
    {
        Game.CurrentGame.EffectsVolume = slider.value;
    }

    public void OnDifficultyChanged(Dropdown drop)
    {
        Game.CurrentGame.Difficulty = (Game.eDifficulty)drop.value;
    }
}
