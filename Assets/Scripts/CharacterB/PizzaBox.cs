using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaBox : MonoBehaviour
{
    [SerializeField] private GameObject fpsPizzaBox;
    [SerializeField] private bool placeHolder;

    public void Interactions()
    {
        if (placeHolder) {
            PlacePizzaBox();
        } else {
            PickUpPizzaBox();
        }
    }

    public void PickUpPizzaBox()
    {
        if (fpsPizzaBox.activeInHierarchy) return;
        fpsPizzaBox.SetActive(true);
        Destroy(gameObject);
    }

    public void PlacePizzaBox()
    {
        TriggerPizza.instance.PlacePizza();
        fpsPizzaBox.SetActive(false);
    }
}
