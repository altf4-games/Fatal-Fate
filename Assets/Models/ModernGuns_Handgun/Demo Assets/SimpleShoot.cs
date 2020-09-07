using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject casingPrefab;
    [SerializeField] private GameObject muzzleFlashPrefab;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;
    [SerializeField] private AudioClip gunShot;
    [SerializeField] private float shotPower = 100f;

    private void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;
    }

    private void Update()
    {
        //TESTING
        if (Input.GetButtonDown("Fire1"))
        {
            GetComponent<Animator>().SetTrigger("Fire");
            AudioManager.instance.PlayAudio(gunShot, 1.0f);
        }
    }

    private void Shoot()
    {
        //GameObject bullet;
        //bullet = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);
        //bullet.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

        //GameObject tempFlash;
        //GameObject bullet = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);
        //bullet.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
        //Destroy(bullet, 7.5f);
        //tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);
        //Destroy(tempFlash, 0.5f);
        //Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation).GetComponent<Rigidbody>().AddForce(casingExitLocation.right * 100f);
       
    }

    private void CasingRelease()
    {
        GameObject casing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation);
        casing.GetComponent<Rigidbody>().AddExplosionForce(550f, (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        casing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(10f, 1000f)), ForceMode.Impulse);
        Destroy(casing, 7.5f);
    }
}
