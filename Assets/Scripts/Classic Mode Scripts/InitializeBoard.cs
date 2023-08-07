using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeBoard : MonoBehaviour
{
    [SerializeField] GameObject[] simonButtons;
    [SerializeField] GameObject simonButtonParent;
    private StartGame startGameScript;
    private int numberOfButtons;
    private Vector2[] buttonLayout;
    private static System.Random rng = new System.Random();

    void Awake()
    {
        startGameScript = this.gameObject.GetComponent<StartGame>();
    }

    private void Start()
    {
        numberOfButtons = startGameScript.GameButtons;
        SetButtonCorrdinates();
        BoardInit();
    }

    public void BoardInit()
    {
        for (int i = 0; i < numberOfButtons; i++)
        {
            GameObject childObject = Instantiate(simonButtons[i]);
            simonButtons[i] = childObject;
            childObject.transform.SetParent(simonButtonParent.transform, false);
            childObject.transform.SetLocalPositionAndRotation(buttonLayout[i], this.gameObject.transform.parent.transform.rotation);
        }
    }

    public void BoardShuffle()
    {
        ShuffleCoordinatesVector();
        for (int i = 0; i < numberOfButtons; i++)
        {
            simonButtons[i].transform.SetLocalPositionAndRotation(buttonLayout[i], this.gameObject.transform.parent.transform.rotation);
        }
    }

    private void SetButtonCorrdinates()
    {
        switch (numberOfButtons)
        {
            case 2:
                buttonLayout = new Vector2[2] {new Vector2(200, 0),
                                                new Vector2(-200, 0) };
                break;
            case 3:
                buttonLayout = new Vector2[3] {new Vector2(200, 100),
                                                new Vector2(0, -250),
                                                new Vector2(-200, 100)};
                break;
            case 4:
                buttonLayout = new Vector2[4] {new Vector2(200, 200),
                                                new Vector2(200, -200),
                                                new Vector2(-200, -200),
                                                new Vector2(-200, 200)};
                break;
            case 5:
                buttonLayout = new Vector2[5] {new Vector2(200, 200),
                                                new Vector2(200, -200),
                                                new Vector2(-200, -200),
                                                new Vector2(-200, 200),
                                                new Vector2(0, 0)};
                break;
            case 6:
                buttonLayout = new Vector2[6] {new Vector2(125, 200),
                                                new Vector2(125, -200),
                                                new Vector2(-125, -200),
                                                new Vector2(-125, 200),
                                                new Vector2(250, 0),
                                                new Vector2(-250, 0)};
                break;
        }
    }

    public void ShuffleCoordinatesVector()
    {
        int n = buttonLayout.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Vector2 value = buttonLayout[k];
            buttonLayout[k] = buttonLayout[n];
            buttonLayout[n] = value;
        }
    }
}
