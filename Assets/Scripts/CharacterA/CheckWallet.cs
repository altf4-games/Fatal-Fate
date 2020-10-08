using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CheckWallet : MonoBehaviour
{
    [SerializeField] private Tutorial tutorial;
    [SerializeField] private VehicleHelper helper;
    [SerializeField] private GameObject walletImg;
    [SerializeField] private GameObject bgBlur;
    [SerializeField] private GameObject spotA;
    [SerializeField] private GameObject spotB;
    [SerializeField] private GameObject door;
    private bool walletChecc;

    private void Start()
    {
        tutorial.walletKey.SetActive(true);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyBinds.engine) && !walletChecc)
        {
            tutorial.walletKey.SetActive(false);
            bgBlur.SetActive(true);
            walletImg.SetActive(true);
            StartCoroutine(Seq());
            walletChecc = true;
        }
    }

    private IEnumerator Seq()
    {
        FirstPersonController.pausePlayer = true;
        yield return new WaitForSeconds(GetComponent<StoryHandlerA>().PrintSubtitle());
        yield return new WaitForSeconds(GetComponent<StoryHandlerA>().PrintSubtitle());

        Option a = new Option("Loot Store", 'A', AD);
        OptionPopup.instance.SetupDecision(a);
        walletImg.SetActive(false);
        spotA.SetActive(false);
    }

    private string AD()
    {
        StartCoroutine(ADseq());
        return null;
    }

    private IEnumerator ADseq()
    {
        spotB.SetActive(true);
        door.SetActive(true);
        helper.canLeaveCar = false;
        door.transform.parent = null;
        GetComponent<StoryHandlerA>().PrintSubtitle();
        yield return new WaitForSeconds(3f);
        GetComponent<StoryHandlerA>().PrintSubtitle();
    }
}
