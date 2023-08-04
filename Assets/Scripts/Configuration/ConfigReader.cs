using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ConfigReader : MonoBehaviour
{
    public TextAsset textJSON;
    public GameDifficultyList gameDifficulties;
    public static ConfigReader Instance { get; private set; }
    public int difficultyChoosen;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        LoadJson();
    }

    void LoadJson()
    {
        gameDifficulties = JsonUtility.FromJson<GameDifficultyList>(textJSON.text);
    }
}
