using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Dumpster : MonoBehaviour
{
    [SerializeField] private GameObject dumpsterBag;
    [SerializeField] private GameObject fpsTrash;
    [SerializeField] private GameObject arrivalTrigger;

    public void DisposeTrash()
    {
        if (fpsTrash.activeInHierarchy)
        {
            GetComponent<Collider>().enabled = false;
            dumpsterBag.SetActive(true);
            fpsTrash.SetActive(false);
            arrivalTrigger.SetActive(true);
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
