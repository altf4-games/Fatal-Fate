using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public class Settings : MonoBehaviour
{
    public static Settings instance;
    [SerializeField] private PostProcessProfile profile;
    [SerializeField] private TMP_Dropdown resDropdown;
    private List<Resolution> screenResolutions;
    private int currentResolution;
    private MotionBlur motionBlur;
    private ScreenSpaceReflections ssr;
    int width, height;
    private bool rainEffects = true;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        Screen.SetResolution(480, 640, false);
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        profile.TryGetSettings<MotionBlur>(out motionBlur);
        profile.TryGetSettings<ScreenSpaceReflections>(out ssr);
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        screenResolutions = new List<Resolution>(Screen.resolutions.Length);
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            screenResolutions.Add(Screen.resolutions[i]);
            string content = Screen.resolutions[i].width + "x" + Screen.resolutions[i].height;
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.image = null;
            option.text = content;
            options.Add(option);
        }
        resDropdown.AddOptions(options);
        currentResolution = screenResolutions.Count;
        resDropdown.value = currentResolution;
        resDropdown.RefreshShownValue();
        motionBlur.enabled.value = false;
    }

    public void PlayButton()
    {
        Screen.SetResolution(width, height, true);
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void SetGraphicsSettings(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void MotionBlur(bool val)
    {
        motionBlur.active = val;
    }

    public void RainFX(bool val)
    {
        rainEffects = val;
    }

    public void SetResolution(int index)
    {
        currentResolution = index;
        width = screenResolutions[currentResolution].width;
        height = screenResolutions[currentResolution].height;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (rainEffects) return;
        if (arg0.buildIndex == 1)
        {
            GameObject.FindGameObjectWithTag("Rain").SetActive(false);
            ssr.enabled.value = false;
        }
        if (arg0.buildIndex == 2)
        {
            GameObject.FindGameObjectWithTag("Rain").SetActive(false);
            ssr.enabled.value = false;
        }
    }
}

