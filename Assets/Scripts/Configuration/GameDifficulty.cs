using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[System.Serializable]
public class GameDifficulty
{
    [SerializeField] private string Mode;
    [SerializeField] private int gameButtons;
    [SerializeField] private int pointsEachStep;
    [SerializeField] private int gameTime;
    [SerializeField] private bool repeatMode;
    [SerializeField] private float gameSpeed;

    public string GetMode()
    {
        return Mode;
    }

    public int GetGameButtons()
    {
        return gameButtons;
    }

    public int GetPointsEachStep()
    {
        return pointsEachStep;
    }

    public int GetGameTime()
    {
        return gameTime;
    }

    public bool GetRepeatMode()
    {
        return repeatMode;
    }

    public float GetGameSpeed()
    {
        return gameSpeed;
    }

    public override string ToString()
    {
        return $"Mode: {Mode}, Game Buttons: {gameButtons}, Points Each Step: {pointsEachStep}, Game Time: {gameTime}, Repeat Mode: {repeatMode}, Game Speed: {gameSpeed}";
    }
}
