using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    [SerializeField] GameObject mainMenuButtons;
    [SerializeField] GameObject classicDifficultyButtons;
    [SerializeField] GameObject hardcoreDifficultyButtons;
    [SerializeField] GameObject settingsMenuButtons;

    public void ShowClassicDifficultyButtons()
    {
        mainMenuButtons.SetActive(false);
        classicDifficultyButtons.SetActive(true);
    }

    public void ShowHardcoreDifficultyButtons()
    {
        mainMenuButtons.SetActive(false);
        hardcoreDifficultyButtons.SetActive(true);
    }

    public void ShowMainMenuButtons()
    {
        hardcoreDifficultyButtons.SetActive(false);
        classicDifficultyButtons.SetActive(false);
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
