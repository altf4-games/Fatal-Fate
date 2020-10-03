using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Bat : MonoBehaviour
{
    [SerializeField] private GameObject cutsceneTrigger;
    [SerializeField] private GameObject fpsBat;

    public void PickUpBat()
    {
        cutsceneTrigger.SetActive(false);
        fpsBat.SetActive(true);
        StoryHandlerB.instance.PrintSubtitle();
        Destroy(gameObject);
    }
}
