using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Drawer : MonoBehaviour
{
    [SerializeField] private float openPosZ;
    [SerializeField] private float closedPosZ;
    [SerializeField] private AudioClip drawerClip;
    [SerializeField] private float speed = .75f;
    [SerializeField] private bool isLocked = false;
    [HideInInspector] public bool isOpen = false;

    private void Start()
    {
        if(closedPosZ == 0)
        {
            closedPosZ = transform.localPosition.z;
        }
    }

    public void OpenDrawer()
    {
        if (isLocked) return;
        isOpen = !isOpen;
        if(isOpen == true)
        {
            AudioManager.instance.PlayAudio(drawerClip, 1.0f);
            LeanTween.moveLocalZ(gameObject, openPosZ, speed);
        }
        else
        {
            AudioManager.instance.PlayAudio(drawerClip, 1.0f);
            LeanTween.moveLocalZ(gameObject, closedPosZ, speed);
        }
    }
}
