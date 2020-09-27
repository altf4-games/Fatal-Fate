using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    [SerializeField] private Transform camera;
    [SerializeField] private GameObject ammoCount;
    [SerializeField] private GameObject casingPrefab;
    [SerializeField] private Transform casingExitLocation;
    [SerializeField] private AudioClip gunShot;
    [SerializeField] private AudioClip dryFire;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private float swaySpeed = 3f;
    [SerializeField] private float fireRate = 1f;
    private int bullets = 7;
    private Vector3 initialPos;
    private bool canUse = true;
    private bool canFire = true;
    private bool empty = false;

    private void Start()
    {
        initialPos = weapon.transform.localPosition;
        ammoCount.SetActive(true);
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("PlayerState") == 0)
        {
            float movementX = -Input.GetAxis("Mouse X") * 0.1f;
            float movementY = -Input.GetAxis("Mouse Y") * 0.1f;

            Vector3 finalPos = new Vector3(movementX, movementY, 0);
            weapon.transform.localPosition = Vector3.Slerp(weapon.transform.localPosition, finalPos + initialPos, swaySpeed * Time.deltaTime);
        }

        if (PlayerPrefs.GetInt("PlayerState") == 0)
        {
            if (canUse == true)
            {
                if(bullets != 0)
                {
                    if (Input.GetMouseButtonDown(KeyBinds.lmb) && canFire)
                    {
                        GetComponent<Animator>().SetTrigger("Fire");
                        canFire = false;
                        AudioManager.instance.PlayAudio(gunShot, 1.0f);
                        Invoke("Shoot", fireRate);
                        bullets--;
                        ammoText.text = bullets.ToString() + "/0";
                        ShootRayCast();
                    }
                }
                else
                {
                    if (Input.GetMouseButtonDown(0) && canFire)
                    {
                        AudioManager.instance.PlayAudio(dryFire, 1.0f);
                        if(empty == false)
                        {
                            empty = true;
                        }
                    }
                }
            }
        }
    }

    private void Shoot()
    {
        canFire = true;
        CasingRelease();
    }

    private void ShootRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.transform.forward, out hit, 25))
        {
            //Debug.DrawRay(camera.position, camera.transform.forward * 25, Color.red,4);
            if (hit.transform.tag == "Cash")
            {
                hit.transform.GetComponent<CashRegister>().SpawnMoneyBags();
            }
        }
    }

    private void CasingRelease()
    {
        GameObject casing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation);
        casing.GetComponent<Rigidbody>().AddExplosionForce(550f, (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        casing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(10f, 1000f)), ForceMode.Impulse);
        Destroy(casing, 7.5f);
    }
}
