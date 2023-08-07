using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> names;
    [SerializeField] List<TextMeshProUGUI> scores;
    private string currPlayerName;
    private int currPlayerScore;
    private string publicLeaderboardKey = "3848635f9db235e379b1b00964ec9cffea2b2f2860d9eddaa349a9ca045b1e84";

    private void Start()
    {
        GetLeaderboard();
    }

    /// <summary>
    /// This method retrives the leaderboard, display it on screen and highlights score achieved.
    /// </summary>
    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) => {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
                for (int i = 0; i < loopLength; ++i)
                {
                    names[i].text = msg[i].Username;
                    scores[i].text = msg[i].Score.ToString();

                    // Highlights the current score
                    if (names[i].text == currPlayerName && scores[i].text == currPlayerScore.ToString())
                    {
                        names[i].text = "<mark=#ffff00aa>" + names[i].text + "</mark>";
                        scores[i].text = "<mark=#ffff00aa>" + scores[i].text + "</mark>";
                    }
                }
            }));
    }

    /// <summary>
    /// This method sets a new leaderboard entry.
    /// </summary>
    /// <param name="i_Username">Player username.</param>
    /// <param name="i_Score">The players score.</param>
    public void SetLeaderboardEnty(string i_Username, int i_Score)
    {
        currPlayerName = i_Username;
        currPlayerScore = i_Score;

        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, i_Username, i_Score, ((msg) =>
        {
            GetLeaderboard();
        }));
    }
}
