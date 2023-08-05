using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> names;
    [SerializeField] List<TextMeshProUGUI> scores;
    private string currName;
    private int currScore;
    private string publicLeaderboardKey = "3848635f9db235e379b1b00964ec9cffea2b2f2860d9eddaa349a9ca045b1e84";

    private void Start()
    {
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) => {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopLength; ++i)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();

                // Highlight current score
                if (names[i].text == currName && scores[i].text == currScore.ToString())
                {
                    names[i].text = "<mark=#ffff00aa>" + names[i].text + "</mark>";
                    scores[i].text = "<mark=#ffff00aa>" + scores[i].text + "</mark>";
                }
            }
            }));
    }

    public void SetLeaderboardEnty(string username, int score)
    {
        currName = username;
        currScore = score;
        
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((msg) =>
        {
            GetLeaderboard();
        }));
    }
}
