using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class StoryHandlerB : MonoBehaviour
{
    public static StoryHandlerB instance;
    private string[] data;
    private int currentSubtitle;

    private void Awake()
    {
        instance = this;
        LoadTextFromFile();
        currentSubtitle = 0;
    }

    private void LoadTextFromFile()
    {
        TextAsset textFile = Resources.Load<TextAsset>("subtitles-chapterII");
        string rawData = Encoding.UTF8.GetString(textFile.bytes, 0, textFile.bytes.Length);
        data = rawData.Split('\n');
    }

    public float PrintSubtitle(int index = default)
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
        return subtitle.time;
    }

    private void Start()
    {
        PrintSubtitle();//0
        PrintSubtitle();//1
    }
}
