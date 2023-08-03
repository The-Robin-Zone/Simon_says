using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameObject gameOverMenuObj;
    [SerializeField] GameObject PointsObj;

    private Queue<ButtonColor> roundColors;
    private Queue<ButtonColor> roundColorsTurnCheck;
    private ButtonPresser buttonpresser;
    private TextMeshProUGUI PointsText;
    private int RoundNumber = 1;
    private float pressGapTime = 0.3f;
    private float roundDelay = 2f;
    private bool playersTurn = true;
    private int Points = 0;

    // Timer
    [SerializeField] GameObject timerMeterObj;
    private Image timerMeter;
    private float maxTime = 30f;
    private float timeRemaining;
    private bool roundStarted = false;

    void Start()
    {
        buttonpresser = this.gameObject.GetComponent<ButtonPresser>();
        roundColors = new Queue<ButtonColor>();
        roundColorsTurnCheck = new Queue<ButtonColor>();
        timerMeter = timerMeterObj.GetComponent<Image>();
        PointsText = PointsObj.GetComponent<TextMeshProUGUI>();
        timeRemaining = maxTime;

        // Subscribe to the OnButtonPress event for every button
        foreach (KeyValuePair<ButtonColor, SimonButton> kvp in buttonpresser.ColorToButtonScriptDictionary)
        {
            kvp.Value.OnButtonPress += ButtonPressed;
        }
    }

    private void Update()
    {
        if (roundStarted == true)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                timerMeter.fillAmount = timeRemaining / maxTime;
            }
            else
            {
                GameOver();
            }
        }

    }

    public void StartRound()
    {
        roundStarted = true;

        // Add new color to queue
        roundColors.Enqueue(RandomColor());

        // Copy queue to check input without destroying the original
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

                // Finished round succesfully
                if (roundColorsTurnCheck.Count == 0)
                {
                    Points++;
                    PointsText.text = Points.ToString();
                    StartCoroutine(StartRoundWithDelay()); 
                }
            }
            else
            {
                GameOver();
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

    private void GameOver()
    {
        StopAllCoroutines();
        buttonpresser.ButtonsDisabler();
        gameOverMenuObj.SetActive(true);
    }
}
