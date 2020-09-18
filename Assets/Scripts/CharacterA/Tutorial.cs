using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] public GameObject movementKeys;
    [SerializeField] public GameObject brakeKeys;
    [SerializeField] public GameObject engineKey;
    [SerializeField] public GameObject walletKey;
    [SerializeField] public GameObject exitKey;

    private void Start()
    {
        FadeKeys(movementKeys, 5f);
        FadeKeys(brakeKeys, 5f);
    }

    public void FadeKeys(GameObject keys,float waitTime)
    {
        StartCoroutine(Fade(keys, waitTime));
    }

    private IEnumerator Fade(GameObject keys, float waitTime)
    {
        keys.SetActive(true);
        yield return new WaitForSeconds(waitTime);

        if (keys == movementKeys)
        {
            CanvasGroup[] group = keys.GetComponentsInChildren<CanvasGroup>();
            foreach (CanvasGroup canvasItm in group)
            {
                LeanTween.alphaCanvas(canvasItm, 0.0f, .35f);
            }
        }
        else
        {
            CanvasGroup[] group = new CanvasGroup[2];
            group[0] = keys.GetComponent<CanvasGroup>();
            group[1] = keys.transform.GetChild(0).GetComponent<CanvasGroup>();
            foreach (CanvasGroup canvasItm in group)
            {
                LeanTween.alphaCanvas(canvasItm, 0.0f, .35f);
            }
        }
    }
}
