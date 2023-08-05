[System.Serializable]
public class GameDifficulty
{
    public string Mode;
    public int GameButtons;
    public int PointsEachStep;
    public int GameTime;
    public bool RepeatMode;
    public float GameSpeed;

    public override string ToString()
    {
        return $"Mode: {Mode}, Game Buttons: {GameButtons}, Points Each Step: {PointsEachStep}, Game Time: {GameTime}, Repeat Mode: {RepeatMode}, Game Speed: {GameSpeed}";
    }
}
