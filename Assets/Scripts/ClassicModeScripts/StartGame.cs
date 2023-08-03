using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private Queue<ButtonColor> roundColors;
    private Queue<ButtonColor> roundColorsTurnCheck;
    private ButtonPresser buttonpresser;
    private int RoundNumber = 1;
    private float pressGapTime = 0.3f;
    private float roundDelay = 2f;
    private bool playersTurn = true;

    void Start()
    {
        buttonpresser = this.gameObject.GetComponent<ButtonPresser>();
        roundColors = new Queue<ButtonColor>();
        roundColorsTurnCheck = new Queue<ButtonColor>();

        // Subscribe to the OnButtonPress event for every button
        foreach (KeyValuePair<ButtonColor, SimonButton> kvp in buttonpresser.ColorToButtonScriptDictionary)
        {
            kvp.Value.OnButtonPress += ButtonPressed;
        }
    }

    public void StartRound()
    {
        // Add new color to queue
        roundColors.Enqueue(RandomColor());

        // Copy queue to check input without destroying original
        roundColorsTurnCheck.Clear();
        roundColorsTurnCheck = new Queue<ButtonColor>(roundColors);

        // Start button routine
        StartCoroutine(RunSequence());
        RoundNumber++;
    }

    private IEnumerator RunSequence()
    {
        playersTurn = false;
        foreach (ButtonColor variableName in roundColors)
        {
            buttonpresser.pressSimonButton(variableName);
            yield return new WaitForSeconds(pressGapTime);
        }
        playersTurn = true;
    }

    private ButtonColor RandomColor()
    {
        Array values = Enum.GetValues(typeof(ButtonColor));
        return (ButtonColor)values.GetValue(new System.Random().Next(values.Length));
    }

    public void ButtonPressed(ButtonColor color)
    {
        Debug.Log($"Button with color {color} pressed");

        if (playersTurn && roundColorsTurnCheck.Count > 0)
        {
            if (roundColorsTurnCheck.Peek() == color)
            {
                roundColorsTurnCheck.Dequeue();
                Debug.Log("Successful press");

                if (roundColorsTurnCheck.Count == 0)
                {
                    StartCoroutine(StartRoundWithDelay());
                    Debug.Log("Successful round");
                }
            }
            else
            {
                Debug.Log("You lose - need to restart");
            }
        }
        else
        {
            Debug.Log("Wait for your turn - cant press button while sequence is running");
        }
        
    }

    private IEnumerator StartRoundWithDelay()
    {
        yield return new WaitForSeconds(roundDelay);  // Wait for 'roundDelay' seconds
        StartRound();
    }
}
