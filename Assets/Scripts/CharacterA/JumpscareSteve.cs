using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class JumpscareSteve : MonoBehaviour
{
    [SerializeField] private GameObject steve;
    [SerializeField] private GameObject jumpscareSteve;
    [SerializeField] private FirstPersonController fpController;
    [SerializeField] private GameObject fpCamera;
    [SerializeField] private AudioClip scareClip;
    [SerializeField] private AudioClip keyClip;
    [SerializeField] private Door[] doors;
    public bool hasKey = false;
    private bool doOnce = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.tag == "Player" && doOnce)
        {
            StartCoroutine(Seq());
            doOnce = false;
        }
    }

    private IEnumerator Seq()
    {
        steve.SetActive(false);
        jumpscareSteve.SetActive(true);
        jumpscareSteve.transform.localRotation = Quaternion.identity;
        jumpscareSteve.transform.parent = fpController.transform;
        fpController.enabled = false;
        LeanTween.rotateLocal(fpCamera, new Vector3(0, fpCamera.transform.localRotation.y+ 180f, 0), .25f);
        AudioManager.instance.PlayAudio(scareClip, 1.0f);
        yield return new WaitForSeconds(.75f);

        Option A = new Option("Can I get the keys for the toilets?", 'A', ObtainKeys);
        Option B = new Option("umm Nothing", 'B', NoResponse);
        OptionPopup.instance.SetupDecision(A, B, "CAN I HELP YOU WITH ANYTHING, SIR?");
    }

    private string ObtainKeys()
    {
        steve.SetActive(true);
        jumpscareSteve.SetActive(false);
        StoryHandlerA.instance.PrintSubtitle(8);
        fpController.enabled = true;
        hasKey = true;
        foreach (Door item in doors)
        {
            item.isLocked = false;
        }

        AudioManager.instance.PlayAudio(keyClip, 1.0f);
        return null;
    }

    private string NoResponse()
    {
        StoryHandlerA.instance.PrintSubtitle(9);
        StartCoroutine(waitForOptions());
        return null;
    }

    private IEnumerator waitForOptions()
    {
        yield return new WaitForSeconds(2f);
        Option A = new Option("Can I get the keys for the toilets?", 'A', ObtainKeys);
        OptionPopup.instance.SetupDecision(A, default, "CAN I HELP YOU WITH ANYTHING, SIR?");
    }
}
