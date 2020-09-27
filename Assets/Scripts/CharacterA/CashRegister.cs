using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegister : MonoBehaviour
{
    [SerializeField] private GameObject moneyBagPrefab;
    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private GameObject moneyTray;
    [SerializeField] private AudioClip ding;
    [SerializeField] private int moneyLimit;
    private bool openOnce = false;
    private int moneyBags;

    public void SpawnMoneyBags()
    {
        if(moneyBags < moneyLimit)
        {
            Instantiate(moneyBagPrefab, spawnPoint, Quaternion.identity);
            AudioManager.instance.PlayAudio(ding, 1.0f);
            moneyBags++;
        }
        if(openOnce == false)
        {
            LeanTween.moveLocalX(moneyTray, -1.82f, .25f);
            openOnce = true;
        }
    }
}
