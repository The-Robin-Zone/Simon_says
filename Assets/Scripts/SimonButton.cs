using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonButton : MonoBehaviour
{
    [SerializeField] AudioSource buttonAudio;
    [SerializeField] ButtonColor buttonColor;

    public ButtonColor GetColor()
    {
        return buttonColor;
    }

    public void PressButton()
    {
        buttonAudio.Play();
    }

}
