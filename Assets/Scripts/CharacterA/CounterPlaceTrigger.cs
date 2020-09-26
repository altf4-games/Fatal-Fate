using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterPlaceTrigger : MonoBehaviour
{
    [SerializeField] private GameObject fpsMilk;
    [SerializeField] private GameObject counterMilk;
    [SerializeField] private GameObject backDoor;
    [SerializeField] private Door bathroomDoor;
    [SerializeField] private NPCSteve npcSteve;
    private bool doOnce = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player") && !doOnce)
        {
            fpsMilk.SetActive(false);
            counterMilk.SetActive(true);
            Option A = new Option("I'm buying this. Now open the door for me.", 'A', ReposA1);
            OptionPopup.instance.SetupDecision(A);
            doOnce = true;
        }
    }

    private string ReposA1()
    {
        StoryHandlerA.instance.PrintSubtitle(12);
        npcSteve.moveToDest = true;
        bathroomDoor.isLocked = false;
        npcSteve.GetComponent<NPCHead>().enabled = false;
        LeanTween.rotate(backDoor, new Vector3(-90f, 0, 90f), 1f);
        return null;
    }
}
