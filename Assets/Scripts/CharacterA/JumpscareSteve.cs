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
        fpController.enabled = false;
        LeanTween.rotateY(fpCamera, -180f, .25f);
        AudioManager.instance.PlayAudio(scareClip, 1.0f);
        yield return null;
        print("TODO: DIALOGUE");
        print("TODO: LOOK UP OTHERSIDE DIALOGUE");
    }
}
