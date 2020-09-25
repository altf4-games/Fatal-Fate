using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interact : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI primaryText;
    [SerializeField] private TextMeshProUGUI secondaryText;
    [SerializeField] private GameObject mouseIndicator;
    [SerializeField] private float maxDistance = 2f;
    private CharacterType character;

    private void Start()
    {
        character = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().character;
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, LayerMask.GetMask("Interact")))
        {
            if (hit.transform.GetComponent<Interactable>() != null)
            {
                mouseIndicator.SetActive(true);
                primaryText.text = hit.transform.GetComponent<Interactable>().actionText;
                secondaryText.text = hit.transform.GetComponent<Interactable>().name;
            }

            if (Input.GetMouseButtonDown(KeyBinds.rmb))
            {
                if(character == CharacterType.Murderer) {
                    CharacterAInts(hit);
                } else {
                    CharacterBInts(hit);
                }
            }
        }
        else
        {
            mouseIndicator.SetActive(false);
            primaryText.text = "";
            secondaryText.text = "";
        }
    }

    private void CharacterAInts(RaycastHit hit)
    {
        if (hit.transform.CompareTag("Door"))
        {
            hit.transform.GetComponent<Door>().OpenDoor();
        }
        if (hit.transform.CompareTag("VehicleDoor"))
        {
            hit.transform.GetComponent<VehicleDoor>().EnterVehicleDoor();
        }
        if (hit.transform.CompareTag("Surveillance"))
        {
            hit.transform.GetComponent<Surveillance>().SetupSurveillance();
        }
        if (hit.transform.CompareTag("NPC_S"))
        {
            hit.transform.GetComponent<NPCSteve>().TalkToNPC();
        }
        if (hit.transform.CompareTag("Milk"))
        {
            hit.transform.GetComponent<Milk>().PickUpMilk();
        }
    }

    private void CharacterBInts(RaycastHit hit)
    {
        /*if (hit.transform.CompareTag("Door"))
        {
            hit.transform.GetComponent<Door>().OpenDoor();
        }*/
    }
}