using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Entities;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    GameObject _canvas;
    public InputField playerNameInputField;
    public Slider MusicVolumeSlider;
    public Slider EffectsVolumeSlider;
    public Dropdown DifficultyDropdown;
    private void Awake()
    {
        _canvas = GameObject.Find("GameOptionsDialog");
        _canvas.SetActive(false);
    }

    private void Start()
    {
        Game.CurrentGame.LoadCurrentState();
    }

    public void ShowGameOptions()
    {        
        _canvas.SetActive(true);
    }

    public void HideGameOptions()
    {
        Game.CurrentGame.SaveCurrentState();
        _canvas.SetActive(false);
    }

    public bool isCanvasActive()
    {
        return _canvas.activeSelf;
    }

    public void UpdateOptionsGUI()
    {
        playerNameInputField.text = Game.CurrentGame.PlayerName;
        MusicVolumeSlider.value = Game.CurrentGame.MusicVolume;
        EffectsVolumeSlider.value = Game.CurrentGame.EffectsVolume;
        DifficultyDropdown.value = (int)Game.CurrentGame.Difficulty;
    }
}
