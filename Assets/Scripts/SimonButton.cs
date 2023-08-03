using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonButton : MonoBehaviour
{
    [SerializeField] AudioSource buttonAudio;
    [SerializeField] ButtonColor buttonColor;

    public delegate void ButtonPressHandler(ButtonColor color);
    public event ButtonPressHandler OnButtonPress;

    public ButtonColor GetColor()
    {
        return buttonColor;
    }

    public void ButtonSound()
    {
        buttonAudio.Play();
    }

    public void PressButton()
    {
        buttonAudio.Play();
        OnButtonPress?.Invoke(buttonColor);
    }

}
