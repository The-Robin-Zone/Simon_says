using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] GameObject enterNameWindow;
    [SerializeField] TextMeshPro inputField;
    private GameObject configReader;
    private ConfigReader configReaderScript;

    private void Awake()
    {
        configReader = GameObject.FindGameObjectWithTag("ConfigReader");
        configReaderScript = configReader.GetComponent<ConfigReader>();
    }

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadHomeScene()
    {
        SceneManager.LoadScene("1 - MainMenu");
    }

    public void LoadClassicScene(int difficulty)
    {
        configReaderScript.difficultyChoosen = difficulty;
        SceneManager.LoadScene("2 - ClassicMode");
    }

    public void LoadHardcoreScene(int difficulty)
    {
        configReaderScript.difficultyChoosen = difficulty;
        SceneManager.LoadScene("3 - HardcoreMode");
    }

    public void ShowEnetNameWindow()
    {
        enterNameWindow.SetActive(true);
    }

    public void SendName()
    {
        configReaderScript.playerName = "Shaked";
        LoadHomeScene();
    }
}
