using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class StoryHandlerA : MonoBehaviour
{
    private string[] data;
    private int currentSubtitle;

    private void Awake()
    {
        LoadTextFromFile();
        currentSubtitle = 0;
    }

    private void LoadTextFromFile()
    {
        TextAsset textFile = Resources.Load<TextAsset>("subtitles-chapterI");
        string rawData = Encoding.UTF8.GetString(textFile.bytes, 0, textFile.bytes.Length);
        data = rawData.Split('\n');
    }

    public void PrintSubtitle(int index = default)
    {
        int i = (index == 0) ? currentSubtitle : index;
        string[] subData = data[i].Split(';');

        Subtitle subtitle = new Subtitle
        {
            msg = subData[0],
            time = float.Parse(subData[1]),
        };

        SubtitleManager.instance.AddInQue(subtitle);
        currentSubtitle++;
    }

    private void Start()
    {
        PrintSubtitle();//0
        PrintSubtitle();//1
    }
}
