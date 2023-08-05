using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class ConfigReader : MonoBehaviour
{
    public TextAsset textJSON;
    public TextAsset textXML;
    public GameDifficultyList gameDifficulties;
    public static ConfigReader Instance { get; private set; }
    public bool readFromJSON;
    public int difficultyChoosen;
    public string playerName;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        LoadData();
    }

    void LoadData()
    {
        if (readFromJSON)
        {
            var wrapper = JsonUtility.FromJson<Wrapper>(textJSON.text);
            gameDifficulties = wrapper.DifficultyList;
        }
        else
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GameDifficultyList), new XmlRootAttribute("DifficultyList"));
            using (StringReader reader = new StringReader(textXML.text))
            {
                gameDifficulties = (GameDifficultyList)serializer.Deserialize(reader);
            }
        }

        Debug.Log("checking  what was loaded");
        Debug.Log(gameDifficulties.Difficulty[0].ToString());
        Debug.Log(gameDifficulties.Difficulty[1].ToString());
        Debug.Log(gameDifficulties.Difficulty[2].ToString());
    }
}
