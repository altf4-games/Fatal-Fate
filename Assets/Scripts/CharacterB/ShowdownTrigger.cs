using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ShowdownTrigger : MonoBehaviour
{
    [SerializeField] private GameObject markCut;
    [SerializeField] private GameObject markGun;
    [SerializeField] private AudioClip gunShot;
    [SerializeField] private AudioClip oof;
    [SerializeField] private AudioClip flatline;
    [SerializeField] private Transform runDest;
    [SerializeField] private GameObject pickUpTruck;
    [SerializeField] private Collider staticColls;
    [SerializeField] private CanvasGroup bg;
    private char ending;
    private NavMeshAgent agent;
    private bool doOnce = true;
    private bool activateOnce = true;
    private bool chaseSequence = false;

    private void Start()
    {
        ending = SaveData.data.ReadData().Item1[0];
        agent = markCut.GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player") && doOnce)
        {
            doOnce = false;
            if (ending == 'A')
            {
                StartCoroutine(EndingA());
            }
            if (ending == 'B')
            {
                StartCoroutine(EndingB());
            }
        }
    }

    private IEnumerator EndingA()
    {
        markCut.SetActive(true);
        markGun.SetActive(true);
        Client.instance.PostToDatabase("Option-A");
        yield return new WaitForSeconds(2f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets.Characters
        .FirstPerson.FirstPersonController>().enabled = false;
        Subtitle subS1 = new Subtitle { msg = "You picked the wrong house fool. Drop the money and I'll let you go", time = 4 };
        SubtitleManager.instance.AddInQue(subS1);
        yield return new WaitForSeconds(4f);
        Subtitle subM1 = new Subtitle { msg = "HAHAHAHA. You're gonna risk your life for a shitty job", time = 3 };
        SubtitleManager.instance.AddInQue(subM1);
        yield return new WaitForSeconds(3f);
        markCut.GetComponent<Animator>().SetTrigger("shoot");
        yield return new WaitForSeconds(1.5f);
        AudioManager.instance.PlayAudio(gunShot, 1.0f);
        yield return new WaitForSeconds(.5f);
        AudioManager.instance.PlayAudio(oof, 1.0f);
        yield return new WaitForSeconds(.5f);
        bg.alpha = 0f;
        bg.GetComponent<Image>().color = Color.red;
        bg.gameObject.SetActive(true);
        LeanTween.alphaCanvas(bg, 1.0f, 1f);
        AudioManager.instance.PlayAudio(flatline, 1.0f);
        Client.instance.RetriveData();
        Client.instance.GetComponent<EndScreen>().isActive = true;
        yield return null;
    }

    private IEnumerator EndingB()
    {
        markCut.SetActive(true);
        markGun.SetActive(true);
        Client.instance.PostToDatabase("Option-B");
        yield return new WaitForSeconds(2f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets.Characters
        .FirstPerson.FirstPersonController>().enabled = false;
        Subtitle subS1 = new Subtitle { msg = "You picked the wrong house fool. Drop the money and I'll let you go", time = 4 };
        SubtitleManager.instance.AddInQue(subS1);
        yield return new WaitForSeconds(4f);
        Subtitle subM1 = new Subtitle { msg = "HAHAHAHA. You're gonna risk your life for a shitty job", time = 3 };
        SubtitleManager.instance.AddInQue(subM1);
        yield return new WaitForSeconds(3f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets.Characters
        .FirstPerson.FirstPersonController>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets.Characters
        .FirstPerson.FirstPersonController>().m_CachedRunSpeed = 7f;
        GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets.Characters
        .FirstPerson.FirstPersonController>().m_RunSpeed = 7f;
        Subtitle subS2 = new Subtitle { msg = "HEY!! COME BACK", time = 3 };
        SubtitleManager.instance.AddInQue(subS2);
        staticColls.enabled = false;
        agent.SetDestination(runDest.position);
        chaseSequence = true;
        markCut.GetComponent<Animator>().SetTrigger("run");
        yield return null;
    }

    private void Update()
    {
        if(chaseSequence)
        {
            if (Vector3.Distance(agent.transform.position,runDest.position) <= 110f && activateOnce)
            {
                pickUpTruck.SetActive(true);
                activateOnce = false;
                Destroy(this);
            }
        }
    }
}
