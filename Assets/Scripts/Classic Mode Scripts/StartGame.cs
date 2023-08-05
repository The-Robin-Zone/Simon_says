using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameObject gameOverMenuObj;
    [SerializeField] GameObject PointsObj;

    private GameObject configReader;
    private ConfigReader configReaderScript;

    private Queue<ButtonColor> roundColors;
    private Queue<ButtonColor> roundColorsTurnCheck;

    private ButtonPresser buttonpresser;
    private TextMeshProUGUI PointsText;
    private int RoundNumber = 1;
    private float pressGapTime = 0.2f;
    private float roundDelay = 1f;
    private bool playersTurn = true;
    private int currentPoints = 0;
    private bool initButtonPresser = false;

    // Game difficulty paramaters
    private int gameButtons;
    private int pointPerRound;
    private int gameTime;
    private bool reaperMode;
    private float gameSpeed;

    // Timer
    [SerializeField] GameObject timerMeterObj;
    private Image timerMeter;
    private float timeRemaining;
    private bool roundStarted = false;

    // LeaderBoard
    [SerializeField] GameObject LeaderboardObj;
    private Leaderboard leaderboardScript;

    void Awake()
    {
        configReader = GameObject.FindGameObjectWithTag("ConfigReader");
        configReaderScript = configReader.GetComponent<ConfigReader>();
        buttonpresser = this.gameObject.GetComponent<ButtonPresser>();
        roundColors = new Queue<ButtonColor>();
        roundColorsTurnCheck = new Queue<ButtonColor>();
        timerMeter = timerMeterObj.GetComponent<Image>();
        PointsText = PointsObj.GetComponent<TextMeshProUGUI>();
        leaderboardScript = LeaderboardObj.GetComponent<Leaderboard>();
        
        AssignDifficultyParameters();
        timeRemaining = gameTime;
        pressGapTime = pressGapTime * (1 / gameSpeed);

        //DEBUGING
        Debug.Log("pressGapTime is:" + pressGapTime);
    }

    private void SubscribeToButtonPressEvent()
    {
        foreach (KeyValuePair<ButtonColor, SimonButton> kvp in buttonpresser.ColorToButtonScriptDictionary)
        {
            kvp.Value.OnButtonPress += ButtonPressed;
        }
    }

    private void Update()
    {
        // Time Bar
        if (roundStarted == true)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                timerMeter.fillAmount = timeRemaining / gameTime;
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

        if (!initButtonPresser)
        {
            buttonpresser.ButtonPresserInit();
            SubscribeToButtonPressEvent();
            initButtonPresser = true;
        }


        // Add new color to queue
        ButtonColor randomColor = RandomColor(gameButtons);
        roundColors.Enqueue(randomColor);

        // Copy queue to check input without destroying the original
        roundColorsTurnCheck.Clear();
        roundColorsTurnCheck = new Queue<ButtonColor>(roundColors);

        // Start button routine
        StartCoroutine(RunSequence(randomColor));
        RoundNumber++;
    }

    private IEnumerator RunSequence(ButtonColor randomColor)
    {
        playersTurn = false;
        if (reaperMode)
        {
            foreach (ButtonColor variableName in roundColors)
            {
                buttonpresser.pressSimonButton(variableName);
                yield return new WaitForSeconds(pressGapTime);
            }
        }
        else
        {
            buttonpresser.pressSimonButton(randomColor);
        }

        playersTurn = true;
    }

    private ButtonColor RandomColor(int gameButtons)
    {
        if (gameButtons < 1 || gameButtons > Enum.GetNames(typeof(ButtonColor)).Length)
        {
            throw new ArgumentOutOfRangeException("Invalid gameButtons");
        }

        ButtonColor[] colors = (ButtonColor[])Enum.GetValues(typeof(ButtonColor));
        return colors[UnityEngine.Random.Range(0, gameButtons)];
    }

    public void ButtonPressed(ButtonColor color)
    {
        if (playersTurn && roundColorsTurnCheck.Count > 0)
        {
            if (roundColorsTurnCheck.Peek() == color)
            {
                roundColorsTurnCheck.Dequeue();
                Debug.Log("Successful press");

                // Finished round succesfully
                if (roundColorsTurnCheck.Count == 0)
                {
                    currentPoints = currentPoints + pointPerRound;
                    PointsText.text = currentPoints.ToString();
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
        leaderboardScript.SetLeaderboardEnty(configReaderScript.playerName, currentPoints);
    }

    private void AssignDifficultyParameters()
    {
        GameDifficulty gamediff = configReaderScript.gameDifficulties.Difficulty[configReaderScript.difficultyChoosen];

        gameButtons = gamediff.GameButtons;             
        pointPerRound = gamediff.PointsEachStep;       
        gameTime = gamediff.GameTime;                   
        reaperMode = gamediff.RepeatMode;               
        gameSpeed = gamediff.GameSpeed;
    }

    public int GameButtons
    {
        get { return gameButtons; }
    }
}
