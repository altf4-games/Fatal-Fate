using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.Characters.FirstPerson;
using TMPro;

public class OptionPopup : MonoBehaviour
{
    public static OptionPopup instance;
    [SerializeField] private GameObject optionA;
    [SerializeField] private GameObject optionB;
    [SerializeField] private GameObject bgBlur;
    [SerializeField] private TextMeshProUGUI convoText;
    private Option cachedA;
    private Option cachedB;
    private bool acceptInput;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!acceptInput) return;

        if(Input.GetKeyDown(KeyBinds.optA))
        {
            ButtonPress(cachedA.optionType);
        }

        if (Input.GetKeyDown(KeyBinds.optB) && cachedB != null)
        {
            ButtonPress(cachedB.optionType);
        }
    }

    public void SetupDecision(Option A,Option B = default,string convo = default)
    {
        cachedA = A;
        if (B != null) cachedB = B;
        bgBlur.SetActive(true);
        optionA.SetActive(true);
        if(B != null) optionB.SetActive(true);

        optionA.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = A.str;
        if (B != null) optionB.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = B.str;

        convoText.text = convo;

        FirstPersonController.pausePlayer = true;
        acceptInput = true;
    }

    private void ButtonPress(char type)
    {
        if (type == 'A')
        {
            cachedA.method();
            cachedA = null;
        }
        else
        {
            cachedB.method();
            cachedB = null;
        }
        cachedA = null;
        cachedB = null;
        ResetOpt();
    }

    private void ResetOpt()
    {
        bgBlur.SetActive(false);
        optionA.SetActive(false);
        optionB.SetActive(false);
        convoText.text = "";
        FirstPersonController.pausePlayer = false;
        acceptInput = false;
    }
}

public class Option
{
    public string str;
    public char optionType;
    public Func<string> method;

    public Option(string _str,char _optionType ,Func<string> _method)
    {
        str = _str;
        optionType = _optionType;
        method = _method;
    }
}