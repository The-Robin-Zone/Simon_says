using System.Collections.Generic;
using System.Xml.Serialization;

[System.Serializable]
public class GameDifficultyList
{
    [XmlElement("Difficulty")]
    public List<GameDifficulty> Difficulty;
}

[System.Serializable]
public class Wrapper
{
    public GameDifficultyList DifficultyList;
}

