using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPizza : MonoBehaviour
{
    public static TriggerPizza instance;
    [SerializeField] private GameObject pizzaPrefab;
    [SerializeField] private GameObject fpsPizza;
    [SerializeField] private Door marketDoor;
    [SerializeField] private Trash trashBag;
    [SerializeField] private List<GameObject> placeSpots = new List<GameObject>();
    public static bool isDone = false;
    private int currentPizza;

    private void Start()
    {
        instance = this;
    }

    public void PlacePizza()
    {
        if (fpsPizza.activeInHierarchy)
        {
            Instantiate(pizzaPrefab, placeSpots[currentPizza].transform.position,
            placeSpots[currentPizza].transform.rotation);
            placeSpots[currentPizza].GetComponent<MeshRenderer>().enabled = false;
            currentPizza++;
            if (currentPizza == placeSpots.Count)
            {
                isDone = true;
                marketDoor.sPlayerLock = false;
                StoryHandlerB.instance.PrintSubtitle();
                trashBag.gameObject.layer = LayerMask.NameToLayer("Interact");
            }
        }
    }
}
