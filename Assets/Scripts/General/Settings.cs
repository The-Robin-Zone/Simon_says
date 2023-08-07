using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private Toggle toggle;
    private GameObject musicPlayerObj;
    private AudioSource musicPlayer;
    private MusicPlayer musicPlayerScript;

    void Start()
    {
        toggle = this.gameObject.GetComponent<Toggle>();
        musicPlayerObj = GameObject.FindGameObjectWithTag("MusicPlayer");
        musicPlayer = musicPlayerObj.GetComponent<AudioSource>();
        musicPlayerScript = musicPlayerObj.GetComponent<MusicPlayer>();
    }

    public void SoundOnOff()
    {
        if (!toggle.isOn)
        {
            Debug.Log("Sound On");
            musicPlayerScript.playBottonSounds = true;
        }
        else
        {
            Debug.Log("Sound off");
            musicPlayerScript.playBottonSounds = false;
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
            musicPlayer.Play();
            musicPlayerScript.playBackgroundMusic = true;
        }
        else
        {
            Debug.Log("Music off");
            musicPlayer.Pause();
            musicPlayerScript.playBackgroundMusic = false;
        }
    }
}
