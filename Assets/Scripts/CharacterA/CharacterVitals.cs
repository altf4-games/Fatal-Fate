using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterVitals : MonoBehaviour
{
    public static CharacterVitals instance;
    public int money = 0;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private GameObject[] stars = new GameObject[3];

    private void Awake()
    {
        instance = this;
    }

    public void AddMoney(int amnt)
    {
        money += amnt;
        moneyText.gameObject.SetActive(true);
        moneyText.text = money.ToString();
    }

    public void Add3Stars()
    {
        foreach (GameObject star in stars)
        {
            star.SetActive(true);
            TwitchStar(star.GetComponent<CanvasGroup>());
        }
    }

    private void TwitchStar(CanvasGroup cg)
    {
        LeanTween.alphaCanvas(cg, 0, .35f).setOnComplete(() =>
        {
            LeanTween.alphaCanvas(cg, 1, .35f).setOnComplete(() =>
            {
                TwitchStar(cg);
            });
        });
    }
}