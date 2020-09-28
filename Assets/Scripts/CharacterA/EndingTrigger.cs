using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

public class EndingTrigger : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject steveCut;
    [SerializeField] private GameObject markCut;
    [SerializeField] private GameObject cutCamera;
    [SerializeField] private GameObject uiBlackBars;
    [SerializeField] private GameObject blackBG;
    [SerializeField] private GameObject blood;
    [SerializeField] private NavMeshAgent steveAgent;
    [SerializeField] private NavMeshAgent markAgent;
    [SerializeField] private Transform dest;
    [SerializeField] private AudioClip horrorTrans;
    [SerializeField] private AudioClip gunShot;
    [SerializeField] private AudioClip oof;
    [SerializeField] private GameObject[] uiDisable;
    [SerializeField] private GameObject[] uiIgnore;
    [SerializeField] private Collider[] ragCol;
    [SerializeField] private Rigidbody[] ragRig;
    public static bool isActive = false;
    private bool doOnce = true;
    private bool runSeq = false;

    private void Start()
    {
        foreach (Collider col in ragCol)
        {
            col.enabled = false;
        }
        foreach (Rigidbody rig in ragRig)
        {
            rig.isKinematic = true;
        }
    }

    private void Update()
    {
        if(runSeq == true)
        {
            steveAgent.SetDestination(markAgent.transform.position);
            markAgent.SetDestination(dest.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && doOnce && isActive)
        {
            print("End Cutscene Trigger");
            FirstPersonController.pausePlayer = true;
            AudioManager.instance.PlayAudio(horrorTrans, 1.0f);
            StartCoroutine(EndingSetup());
            doOnce = true;
        }
    }

    private IEnumerator EndingSetup()
    {
        steveCut.SetActive(true);
        steveCut.GetComponent<Animator>().SetTrigger("bat");
        markCut.SetActive(true);
        player.SetActive(false);
        cutCamera.SetActive(true);
        uiBlackBars.SetActive(true);
        foreach (GameObject ui in uiDisable)
        {
            ui.SetActive(false);
        }
        yield return new WaitForSeconds(.75f);
        StoryHandlerA.instance.PrintSubtitle(15);
        yield return new WaitForSeconds(4.5f);
        StoryHandlerA.instance.PrintSubtitle(16);
        yield return new WaitForSeconds(4f);
        Option A = new Option("Shoot Him", 'A', ShootHim);
        Option B = new Option("Spare Him", 'B', SpareHim);
        OptionPopup.instance.SetupDecision(A, B);
        TimeLimiter.instance.SetupTimeLimiter(12.5f, "DECIDE HIS FATE", IgnoreShootHim);
    }

    private string IgnoreShootHim()
    {
        foreach (GameObject ui in uiIgnore)
        {
            ui.SetActive(false);
        }
        StartCoroutine(ShootEnding());
        TimeLimiter.instance.StopTimer();
        return null;
    }

    private string ShootHim()
    {
        StartCoroutine(ShootEnding());
        TimeLimiter.instance.StopTimer();
        return null;
    }

    private IEnumerator ShootEnding()
    {
        markCut.GetComponent<Animator>().SetTrigger("shoot");
        yield return new WaitForSeconds(1.5f);
        AudioManager.instance.PlayAudio(gunShot, 1.0f);
        yield return new WaitForSeconds(.5f);
        AudioManager.instance.PlayAudio(oof, 1.0f);
        steveCut.GetComponent<Animator>().enabled = false;
        foreach (Collider col in ragCol)
        {
            col.enabled = true;
        }
        foreach (Rigidbody rig in ragRig)
        {
            rig.isKinematic = false;
        }
        yield return new WaitForSeconds(2f);
        blood.SetActive(true);
        cutCamera.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(4.5f);
        blackBG.SetActive(true);
    }

    private string SpareHim()
    {
        StartCoroutine(SpareEnding());
        TimeLimiter.instance.StopTimer();
        return null;
    }

    private IEnumerator SpareEnding()
    {
        steveCut.transform.GetComponent<Animator>().SetTrigger("run");
        markCut.transform.GetComponent<Animator>().SetTrigger("run");
        runSeq = true;
        yield return new WaitForSeconds(5f);
        cutCamera.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(4.5f);
        blackBG.SetActive(true);
    }
}