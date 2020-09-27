using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashBag : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.gameObject.CompareTag("Player"))
        {
            if(CharacterVitals.instance.money == 0) {
                StoryHandlerA.instance.PrintSubtitle(14);
                EndingTrigger.isActive = true;
            }
            CharacterVitals.instance.AddMoney(Random.Range(1000, 5000));
            CharacterVitals.instance.Add3Stars();
            Destroy(gameObject);
        }
    }
}
