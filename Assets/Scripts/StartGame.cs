using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private Queue<ButtonColor> roundColors;
    private ButtonPresser buttonpresser;
    private int RoundNumber = 1;
    private float pressGapTime = 0.3f;

    void Start()
    {
        buttonpresser = this.gameObject.GetComponent<ButtonPresser>();
        roundColors = new Queue<ButtonColor>();
    }

    public void StartRound()
    {
        //for (int i = 0; i < RoundNumber; i++)
        //{
            roundColors.Enqueue(RandomColor());
        //}
        StartCoroutine(RunSequence());
        RoundNumber++;
    }

    private IEnumerator RunSequence()
    {
        foreach (ButtonColor variableName in roundColors)
        {
            buttonpresser.pressSimonButton(variableName);
            yield return new WaitForSeconds(pressGapTime);
        }
    }

    private ButtonColor RandomColor()
    {
        Array values = Enum.GetValues(typeof(ButtonColor));
        return (ButtonColor)values.GetValue(new System.Random().Next(values.Length));
    }

}
