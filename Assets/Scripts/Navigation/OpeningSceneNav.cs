using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningSceneNav : MonoBehaviour
{
    [SerializeField] GameObject enterNameWindow;
    [SerializeField] GameObject inputFieldObj;
    private TMP_InputField inputField;
    private GameObject configReader;
    private ConfigReader configReaderScript;

    private void Awake()
    {
        configReader = GameObject.FindGameObjectWithTag("ConfigReader");
        configReaderScript = configReader.GetComponent<ConfigReader>();
        inputField = inputFieldObj.GetComponent<TMP_InputField>();
    }

    public void ShowEnetNameWindow()
    {
        enterNameWindow.SetActive(true);
    }

    /// <summary>
    /// This method assigns input string as a 'playerName' in the config script.
    /// </summary>
    public void SendName()
    {
        if (!string.IsNullOrEmpty(inputField.text) && !string.IsNullOrWhiteSpace(inputField.text))
        {
            configReaderScript.playerName = inputField.text;
            SceneManager.LoadScene("1 - MainMenu");
        }
        else
        {
            Debug.Log("Enter a valid name");
        }
    }
}