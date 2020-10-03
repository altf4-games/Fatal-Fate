using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] private GameObject bat;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            bat.layer = LayerMask.NameToLayer("Interact");
            Subtitle helpSubtitle = new Subtitle{ msg = "I should grab the bat from staff room before confronting the theif", time = 4f};
            SubtitleManager.instance.AddInQue(helpSubtitle);
        }
    }
}
