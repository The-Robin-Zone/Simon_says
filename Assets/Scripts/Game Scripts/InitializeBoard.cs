using UnityEngine;

public class InitializeBoard : MonoBehaviour
{
    [SerializeField] GameObject[] simonButtons;
    [SerializeField] GameObject simonButtonParent;
    private StartGame startGameScript;
    private int numberOfButtons;
    private Vector2[] buttonLayout;
    private static System.Random rnd;

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

    /// <summary>
    /// This method instantiates the Simon buttons prefabs and sets thier position.
    /// </summary>
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

    /// <summary>
    /// This method shuffles the Simon buttons position.
    /// </summary>
    public void BoardShuffle()
    {
        ShuffleCoordinatesVector();
        for (int i = 0; i < numberOfButtons; i++)
        {
            simonButtons[i].transform.SetLocalPositionAndRotation(buttonLayout[i], this.gameObject.transform.parent.transform.rotation);
        }
    }

    /// <summary>
    /// This method shuffles the coordinates vector2 array.
    /// </summary>
    public void ShuffleCoordinatesVector()
    {
        rnd = new System.Random();
        int n = buttonLayout.Length;
        while (n > 1)
        {
            n--;
            int k = rnd.Next(n + 1);
            Vector2 value = buttonLayout[k];
            buttonLayout[k] = buttonLayout[n];
            buttonLayout[n] = value;
        }
    }

    /// <summary>
    /// This method assigns the buttons locations (Vector2)array (hard-coded). 
    /// </summary>
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
}
