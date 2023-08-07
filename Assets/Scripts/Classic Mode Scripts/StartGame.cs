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
    private GameObject configReader;
    private ConfigReader configReaderScript;
    private InitializeBoard initializeBoardScript;
    private Queue<eButtonColor> roundColors;
    private Queue<eButtonColor> roundColorsTurnCheck;
    private ButtonPresser buttonpresser;
    private TextMeshProUGUI PointsText;
    private float pressGapTime = 0.2f;
    private float roundDelay = 1f;
    private bool playersTurn = true;
    private int currentPoints = 0;
    private bool initButtonPresser = false;

    // Game difficulty paramaters
    private int gameButtons;
    private int pointPerRound;
    private int gameTime;
    private bool repeatMode;
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
        roundColors = new Queue<eButtonColor>();
        roundColorsTurnCheck = new Queue<eButtonColor>();
        timerMeter = timerMeterObj.GetComponent<Image>();
        PointsText = PointsObj.GetComponent<TextMeshProUGUI>();
        leaderboardScript = LeaderboardObj.GetComponent<Leaderboard>();
        initializeBoardScript = this.gameObject.GetComponent<InitializeBoard>();

        AssignDifficultyParameters();
        timeRemaining = gameTime;
        pressGapTime = pressGapTime * (1 / gameSpeed);
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

    /// <summary>
    /// This method assigns values according to the game difficulty that the player choose.
    /// </summary>
    private void AssignDifficultyParameters()
    {
        GameDifficulty gamediff = configReaderScript.gameDifficulties.Difficulty[configReaderScript.difficultyChoosen];

        gameButtons = gamediff.GameButtons;
        pointPerRound = gamediff.PointsEachStep;
        gameTime = gamediff.GameTime;
        repeatMode = gamediff.RepeatMode;
        gameSpeed = gamediff.GameSpeed;
    }

    public int GameButtons
    {
        get { return gameButtons; }
    }

    /// <summary>
    /// This method subscribs the button press events of all Simon buttons.
    /// Registers the ButtonPressed method as a listener for the OnButtonPress event of each SimonButton instance.
    /// </summary>
    private void SubscribeToButtonPressEvent()
    {
        foreach (KeyValuePair<eButtonColor, SimonButton> kvp in buttonpresser.ColorToButtonScriptDictionary)
        {
            kvp.Value.OnButtonPress += ButtonPressed;
        }
    }

    /// <summary>
    /// This method starts a new round of the game.
    /// </summary>
    public void StartRound()
    {
        roundStarted = true;

        // After player starts game disable the 'Start' button
        this.gameObject.GetComponent<Button>().interactable = false;

        if (!initButtonPresser)
        {
            buttonpresser.ButtonPresserInit();
            SubscribeToButtonPressEvent();
            initButtonPresser = true;
        }

        // Adds new color to queue
        eButtonColor randomColor = RandomColor(gameButtons);
        roundColors.Enqueue(randomColor);

        // Copy queue to check input without destroying the original
        roundColorsTurnCheck.Clear();
        roundColorsTurnCheck = new Queue<eButtonColor>(roundColors);

        // Start button routine
        StartCoroutine(RunSequence(randomColor));
    }

    /// <summary>
    /// This method runs the sequence of Simon buttons pressing at the start of each round.
    /// </summary>
    private IEnumerator RunSequence(eButtonColor i_RandomColor)
    {
        playersTurn = false;
        if (repeatMode)
        {
            foreach (eButtonColor variableName in roundColors)
            {
                buttonpresser.pressSimonButton(variableName);
                yield return new WaitForSeconds(pressGapTime);
            }
        }
        else
        {
            buttonpresser.pressSimonButton(i_RandomColor);
        }

        playersTurn = true;
    }

    /// <summary>
    /// This method generates a random color according to the number of Simon buttons in game.
    /// </summary>
    /// <param name="i_GameButtons">Number of Simon buttons in scene.</param>
    /// <returns>Random eButtonColor.</returns>
    private eButtonColor RandomColor(int i_GameButtons)
    {
        if (i_GameButtons < 1 || i_GameButtons > Enum.GetNames(typeof(eButtonColor)).Length)
        {
            throw new ArgumentOutOfRangeException("Invalid gameButtons");
        }

        eButtonColor[] o_Colors = (eButtonColor[])Enum.GetValues(typeof(eButtonColor));
        return o_Colors[UnityEngine.Random.Range(0, i_GameButtons)];
    }


    /// <summary>
    /// This method listens to the Simon button the player pressed and directs the game flow.
    /// </summary>
    /// <param name="i_Color">Color of Simon button pressed.</param>
    public void ButtonPressed(eButtonColor i_Color)
    {
        if (playersTurn && roundColorsTurnCheck.Count > 0)
        {
            if (roundColorsTurnCheck.Peek() == i_Color)
            {
                Debug.Log("Successful press");
                roundColorsTurnCheck.Dequeue();
                
                // Finished round succesfully
                if (roundColorsTurnCheck.Count == 0)
                {
                    Debug.Log("Successful round");
                    currentPoints = currentPoints + pointPerRound;
                    PointsText.text = currentPoints.ToString();
                    StartCoroutine(StartRoundWithDelay());

                    // HARDCORE MODE - shuffle board
                    if (configReaderScript.hardcoreMode)
                    {
                        initializeBoardScript.BoardShuffle();
                    }
                }
            }
            else
            {
                Debug.Log("Unsuccessful press");
                GameOver();
            }
        }
        else
        {
            Debug.Log("Wait for your turn - can't press button while sequence is running");
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
}
