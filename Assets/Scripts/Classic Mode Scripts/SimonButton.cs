using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonButton : MonoBehaviour
{
    public delegate void ButtonPressHandler(ButtonColor color);
    public event ButtonPressHandler OnButtonPress;

    [SerializeField] AudioSource buttonAudio;
    [SerializeField] ButtonColor buttonColor;

    private GameObject musicPlayerObj;
    private MusicPlayer musicPlayerScript;

    void Start()
    {
        musicPlayerObj = GameObject.FindGameObjectWithTag("MusicPlayer");
        musicPlayerScript = musicPlayerObj.GetComponent<MusicPlayer>();
    }

    public ButtonColor GetColor()
    {
        return buttonColor;
    }

    public void ButtonSound()
    {
        if (musicPlayerScript.playBottonSounds)
        {
            buttonAudio.Play();
        }
    }

    public void PressButton()
    {
        if (musicPlayerScript.playBottonSounds)
        {
            buttonAudio.Play();
        }

        OnButtonPress?.Invoke(buttonColor);
    }
}
