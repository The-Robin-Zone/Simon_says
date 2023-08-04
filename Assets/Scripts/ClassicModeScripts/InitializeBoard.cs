using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeBoard : MonoBehaviour
{
    [SerializeField] GameObject[] simonButtons;
    private StartGame startGameScript;
    private int numberOfButtons;
    private GameObject mainCanvas;

    void Start()
    {
        startGameScript = this.gameObject.GetComponent<StartGame>();
        numberOfButtons = startGameScript.GameButtons;
        mainCanvas = this.gameObject.transform.parent.gameObject;

        BoardInit();
    }

    void BoardInit()
    {
        for (int i = 0; i < numberOfButtons; i++)
        {
            GameObject childObject = Instantiate(simonButtons[i]);
            //childObject.transform.parent = mainCanvas.transform;
            childObject.transform.SetParent(mainCanvas.transform, false);
        }
    }
}
