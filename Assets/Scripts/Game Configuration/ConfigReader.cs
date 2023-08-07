using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class ConfigReader : MonoBehaviour
{
    [SerializeField] TextAsset textJSON;
    [SerializeField] TextAsset textXML;
    public GameDifficultyList gameDifficulties;
    public static ConfigReader Instance { get; private set; }
    public bool readFromJSON;
    public int difficultyChoosen;
    public string playerName;
    public bool hardcoreMode = false;

    /// <summary>
    /// This method makes sure that the object is a singleton.
    /// </summary>
    private void Awake()
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

        loadData();
    }

    /// <summary>
    /// This method loads data from JSON or XML file according to 'readFromJSON' boolean.
    /// </summary>
    private void loadData()
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

        // Check loaded values
        Debug.Log(gameDifficulties.Difficulty[0].ToString());
        Debug.Log(gameDifficulties.Difficulty[1].ToString());
        Debug.Log(gameDifficulties.Difficulty[2].ToString());
    }
}
