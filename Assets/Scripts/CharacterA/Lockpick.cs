using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Lockpick : MonoBehaviour
{
    [SerializeField] private GameObject lockPickCam;
    [SerializeField] private GameObject paperClip;
    [SerializeField] private GameObject fpsCam;
    [SerializeField] private AudioSource audioS;
    public bool unlocked = false;
    private bool acceptInput = false;
    private const int chance = 420;

    private void Update()
    {
        if(acceptInput)
        {
            float rotationZ = Mathf.Clamp(transform.localEulerAngles.y + Input.GetAxis("Mouse X") * 5, -360, 360);
            paperClip.transform.localEulerAngles = new Vector3(0, -90f, rotationZ);
            if(Input.GetAxisRaw("Mouse X") >= .1f || Input.GetAxisRaw("Mouse X") <= -.1f)
            {
                audioS.Play();
                if (LockCheck())
                {
                    unlocked = true;
                    GetComponent<Door>().isLocked = false;
                    ResetView();
                }
            }
            else
            {
                audioS.Stop();
            }
        }
    }

    private bool LockCheck()
    {
        return Random.Range(0, 999) == chance;
    }

    public void SetupLockpick()
    {
        FirstPersonController.pausePlayer = true;
        fpsCam.SetActive(false);
        lockPickCam.SetActive(true);
        paperClip.SetActive(true);
        acceptInput = true;
    }

    private void ResetView()
    {
        audioS.Stop();
        FirstPersonController.pausePlayer = false;
        fpsCam.SetActive(true);
        lockPickCam.SetActive(false);
        paperClip.SetActive(false);
        acceptInput = false;
    }
}
