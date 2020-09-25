using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Milk : MonoBehaviour
{
    [SerializeField] private GameObject fpsMilk;
    [SerializeField] private GameObject counterPlaceTrigger;
    [SerializeField] private GameObject[] arrows;
    private Milk[] allTheMilk;

    private void Start()
    {
        allTheMilk = Object.FindObjectsOfType<Milk>();
    }

    public void PickUpMilk()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        fpsMilk.SetActive(true);
        counterPlaceTrigger.SetActive(true);
        foreach (GameObject item in arrows)
        {
            item.SetActive(false);
        }
        foreach (Milk mlk in allTheMilk)
        {
            mlk.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
