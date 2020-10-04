using UnityEngine;
using System.IO;

public class SaveData : MonoBehaviour
{
    public static SaveData data;
    [SerializeField] private string saveDataFile;
    private string savePath;

    private void Start()
    {
        data = this;
        DontDestroyOnLoad(gameObject);
        savePath = System.IO.Path.Combine(Application.persistentDataPath, saveDataFile);
    }

    public void WriteData(char ending, int progress)
    {
        string xml = "ENDING=" + ending + "\n" + "PROGRESS=" + progress;
        File.WriteAllText(savePath, xml);
    }

    public string ReadData()
    {
        string xml = File.ReadAllText(savePath);
        string[] parsedData = xml.Split('\n');
        return parsedData[0].Split('=')[1];
    }
}
