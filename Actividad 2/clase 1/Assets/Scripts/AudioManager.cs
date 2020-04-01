using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioManager Instance;
    public AudioSource BallExplotion;
    public AudioSource BallCaptured;

    private void Awake()
    {
        Instance = this;
    }

    public enum SoundEffect
    {
        Explote,
        Capture
    }

    public void PlaySoundEffect(SoundEffect type)
    {
        switch(type)
        {
            case SoundEffect.Capture:
                BallCaptured.Play();
                break;
            case SoundEffect.Explote:
                BallExplotion.Play();
                break;
        }
    }
}
