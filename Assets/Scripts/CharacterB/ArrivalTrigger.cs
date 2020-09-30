using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ArrivalTrigger : MonoBehaviour
{
    [SerializeField] private Door marketDoor;
    [SerializeField] private GameObject blackBg;
    [SerializeField] private GameObject carAI;
    [SerializeField] private GameObject counterBlocker;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 pos;
    private bool doOnce = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player") && doOnce)
        {
            LeanTween.rotateY(marketDoor.gameObject, 0f, 1f);
            StoryHandlerB.instance.PrintSubtitle();
            marketDoor.sPlayerLock = true;
            marketDoor.isOpen = false;
            StartCoroutine(TeleportBackToSpawn());
            doOnce = false;
        }
    }

    private IEnumerator TeleportBackToSpawn()
    {
        player.GetComponent<FirstPersonController>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;
        yield return new WaitForSeconds(3);
        blackBg.SetActive(true);
        player.transform.position = pos;
        player.transform.rotation = Quaternion.Euler(new Vector3(0, 180f, 0));
        player.transform.GetChild(0).rotation = Quaternion.identity;
        yield return new WaitForSeconds(1);
        player.GetComponent<FirstPersonController>().enabled = true;
        player.GetComponent<CharacterController>().enabled = true;
        blackBg.SetActive(false);
        counterBlocker.SetActive(true);
        carAI.SetActive(true);
        Destroy(this);
    }
}
