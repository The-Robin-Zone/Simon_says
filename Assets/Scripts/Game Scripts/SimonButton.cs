using UnityEngine;

public class SimonButton : MonoBehaviour
{
    [SerializeField] AudioSource buttonAudio;
    [SerializeField] eButtonColor buttonColor;
    private GameObject musicPlayerObj;
    private MusicPlayer musicPlayerScript;
    public delegate void ButtonPressHandler(eButtonColor color);
    public event ButtonPressHandler OnButtonPress;

    void Start()
    {
        musicPlayerObj = GameObject.FindGameObjectWithTag("MusicPlayer");
        musicPlayerScript = musicPlayerObj.GetComponent<MusicPlayer>();
    }

    public eButtonColor GetColor()
    {
        return buttonColor;
    }

    /// <summary>
    /// This method plays the Simon buttons sound (used for replaying button sequence). 
    /// </summary>
    public void ButtonSound()
    {
        if (musicPlayerScript.playBottonSounds)
        {
            buttonAudio.Play();
        }
    }

    /// <summary>
    /// This method sends which Simon button the user pressed to the StartGame script & plays buttons sound.
    /// </summary>
    public void PressButton()
    {
        if (musicPlayerScript.playBottonSounds)
        {
            buttonAudio.Play();
        }

        OnButtonPress?.Invoke(buttonColor);
    }
}
