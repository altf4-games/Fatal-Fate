using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI markText;
    [SerializeField] private TextMeshProUGUI steveText;
    [SerializeField] private GameObject[] checkBox;
    [HideInInspector] public bool isActive = false;
    private GameObject endPanel;

    private void Start()
    {
        endPanel = markText.transform.parent.gameObject;
    }

    public void GetFinalPercentage(int OPTION_A, int OPTION_B, char OptionType)
    {
        endPanel.SetActive(true);
        if (OptionType == 'A') checkBox[0].SetActive(true);
        if (OptionType == 'B') checkBox[1].SetActive(true);

        float total = OPTION_A + OPTION_B;
        //print(OPTION_B / total * 100);

        float markPercent = OPTION_B / total * 100;
        float stevePercent = Mathf.Abs(markPercent - 100);

        markText.text = "You Killed Mark - " + Mathf.RoundToInt(markPercent) + "%";
        steveText.text = "You Killed Steve - " + Mathf.RoundToInt(stevePercent) + "%";
    }
}
