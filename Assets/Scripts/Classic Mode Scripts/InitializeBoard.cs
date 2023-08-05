using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeBoard : MonoBehaviour
{
    [SerializeField] GameObject[] simonButtons;
    private StartGame startGameScript;
    private int numberOfButtons;
    private GameObject mainCanvas;

    void Awake()
    {
        startGameScript = this.gameObject.GetComponent<StartGame>();
        mainCanvas = this.gameObject.transform.parent.gameObject;
    }

    private void Start()
    {
        numberOfButtons = startGameScript.GameButtons;
        BoardInit();
    }

    void BoardInit()
    {
        for (int i = 0; i < numberOfButtons; i++)
        {
            GameObject childObject = Instantiate(simonButtons[i]);
            childObject.transform.SetParent(mainCanvas.transform, false);
        }
    }
}
