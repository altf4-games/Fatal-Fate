using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeLimiter : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private TextMeshProUGUI timeText;
    public static TimeLimiter instance;
    private float divider;
    private Func<string> method;

    private void Awake()
    {
        instance = this;
    }

    public void SetupTimeLimiter(float time, string text, Func<string> _method)
    {
        progressBar.gameObject.SetActive(true);
        progressBar.fillAmount = Mathf.Clamp01(time / .9f);
        timeText.text = text;
        divider = time - 1;
        method = _method;
        StartCoroutine(TickTime(time));
    }

    public void StopTimer()
    {
        StopAllCoroutines();
        ResetF();
    }

    private IEnumerator TickTime(float time)
    {
        while(time > 0)
        {
            yield return new WaitForSeconds(1f);
            progressBar.fillAmount = Mathf.Clamp01(time / divider);
            time--;
        }
        ResetF();
        method.Invoke();
        method = null;
    }

    private void ResetF()
    {
        progressBar.gameObject.SetActive(false);
        timeText.text = "";
    }
}
