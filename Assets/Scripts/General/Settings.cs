using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private Toggle toggle;

    void Start()
    {
        toggle = this.gameObject.GetComponent<Toggle>();
    }

    public void SoundOnOff()
    {
        if (!toggle.isOn)
        {
            Debug.Log("Sound On");
        }
        else
        {
            Debug.Log("Sound off");
        } 
    }

    public void VibrationOnOff()
    {
        if (!toggle.isOn)
        {
            Debug.Log("Vibration On");
        }
        else
        {
            Debug.Log("Vibration off");
        }
    }

    public void MusicOnOff()
    {
        if (!toggle.isOn)
        {
            Debug.Log("Music On");
        }
        else
        {
            Debug.Log("Music off");
        }
    }
}
