using Aura2API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class BoardPlan : MonoBehaviour
{
    [SerializeField] private Camera boardCamera;
    [SerializeField] private GameObject boardObj;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject blackBg;
    private bool acceptInput;

    private void Update()
    {
        if(Input.anyKeyDown && acceptInput)
        {
            FirstPersonController.pausePlayer = false;
            boardCamera.gameObject.SetActive(false);
            player.transform.position = boardCamera.transform.position;
            player.SetActive(true);
            StoryHandlerA.instance.PrintSubtitle();
            Destroy(this);
        }
    }

    public void PlanningPhase()
    {
        StartCoroutine(Seq());
    }

    private IEnumerator Seq()
    {
        FirstPersonController.pausePlayer = true;
        blackBg.SetActive(true);
        yield return new WaitForSeconds(1f);
        blackBg.SetActive(false);
        player.SetActive(false);
        boardCamera.GetComponent<AuraCamera>().enabled = true;
        boardCamera.gameObject.SetActive(true);
        yield return new WaitForSeconds(7.5f);
        blackBg.SetActive(true);
        yield return new WaitForSeconds(2f);
        blackBg.SetActive(false);
        boardObj.SetActive(true);
        acceptInput = true;
    }
}
