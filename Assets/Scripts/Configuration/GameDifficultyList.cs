using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameDifficultyList
{
    [SerializeField] private List<GameDifficulty> Difficulty;

    public List<GameDifficulty> GetDifficulty()
    {
        return Difficulty;
    }
}

