using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    [SerializeField] GameObject mainMenuButtons;
    [SerializeField] GameObject difficultyButtons;
    [SerializeField] GameObject settingsMenuButtons;

    public void ShowDifficultyButtons()
    {
        mainMenuButtons.SetActive(false);
        difficultyButtons.SetActive(true);
    }

    public void ShowMainMenuButtons()
    {
        difficultyButtons.SetActive(false);
        mainMenuButtons.SetActive(true); 
    }

    public void ShowSetting()
    {
        settingsMenuButtons.SetActive(true);
    }

    public void HideSetting()
    {
        settingsMenuButtons.SetActive(false);
    }
}
