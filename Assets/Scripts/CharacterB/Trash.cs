using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Trash : MonoBehaviour
{
    [SerializeField] private GameObject fpsTrash;
    [SerializeField] private Dumpster dumpster;

    public void PickUpTrash()
    {
        fpsTrash.SetActive(true);
        dumpster.gameObject.layer = LayerMask.NameToLayer("Interact");
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }
}
