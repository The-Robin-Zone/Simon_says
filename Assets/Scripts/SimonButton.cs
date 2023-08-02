using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonButton : MonoBehaviour
{
    public AudioSource buttonAudio;
    public ButtonColor buttonColor;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PressButton()
    {
        buttonAudio.Play();
    }
}
