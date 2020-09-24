using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(Interactable))]
public class Surveillance : MonoBehaviour
{
    [SerializeField] private GameObject survCamera;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject survPanel;
    [SerializeField] private GameObject deleteButton;
    [SerializeField] private TextMeshProUGUI display;
    [SerializeField] private AudioClip feedBack;
    [SerializeField] private GameObject[] screenOffs = new GameObject[2];
    [SerializeField] private GameObject[] screenOns = new GameObject[2];
    [SerializeField] private GameObject[] cameras = new GameObject[2];
    private bool canBeUnlocked = true;
    private const int PASSOWRD = 420;
    private string input = "";

    public void SetupSurveillance()
    {
        if (!canBeUnlocked) return;
        survCamera.SetActive(true);
        playerCamera.SetActive(false);
        UI.SetActive(false);
        FirstPersonController.pausePlayer = true;
        GameController.SetCursor(true, false);
        survPanel.SetActive(true);
    }

    public void EnterChar(int chr)
    {
        if (chr == 10)
        {
            input = "";
            display.text = null;
            return;
        }
        else if (chr == 11)
        {
            CheckAnswer();
            return;
        }
        AudioManager.instance.PlayAudio(feedBack, 1.0f);
        if (input.Length == 3) return;
        input += chr.ToString();
        display.text = input;
    }

    public void PowerOff()
    {
        AudioManager.instance.PlayAudio(feedBack, 1.0f);
        survCamera.SetActive(false);
        playerCamera.SetActive(true);
        FirstPersonController.pausePlayer = false;
        GameController.SetCursor(false, true);
        survPanel.SetActive(false);
    }

    public void DeleteButton()
    {
        AudioManager.instance.PlayAudio(feedBack, 1.0f);
        canBeUnlocked = false;
        StoryHandlerA.instance.PrintSubtitle(9);
        foreach (GameObject screen in screenOffs) {
            screen.SetActive(true);
        }
        foreach (GameObject screen in screenOns) {
            screen.SetActive(false);
        }
        foreach (GameObject camera in cameras) {
            camera.SetActive(false);
        }
    }

    private void CheckAnswer()
    {
        if (input == "") return;
        if(int.Parse(input) == PASSOWRD)
        {
            deleteButton.SetActive(true);
            input = "***";
            display.text = input;
            foreach (GameObject screen in screenOffs) {
                screen.SetActive(false);
            }
            foreach (GameObject screen in screenOns) {
                screen.SetActive(true);
            }
            foreach (GameObject camera in cameras) {
                camera.SetActive(true);
            }
        }
        else
        {
            input = "";
            display.text = input;
            return;
        }
    }
}
