using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTE : MonoBehaviour
{
    public static QTE instance;
    [SerializeField] private RectTransform moverImage;
    [SerializeField] private float speed = 100f;
    [SerializeField] private float maxSpeed = 337.5f;
    [SerializeField] private Door door;
    [SerializeField] private GameObject self;
    [SerializeField] private GameObject spaceBar;
    [SerializeField] private GameObject cutsceneTrigger;
    private bool isOverlapping = false;
    private bool isWorking = false;

    private void Start()
    {
        instance = this;
    }

    public void ToggleQTE(bool toggle)
    {
        isWorking = toggle;
        self.GetComponent<Image>().enabled = toggle;
        moverImage.GetComponent<Image>().enabled = toggle;
        self.GetComponent<PolygonCollider2D>().enabled = toggle;
        moverImage.GetComponent<PolygonCollider2D>().enabled = toggle;
        spaceBar.SetActive(toggle);
        GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets.Characters
        .FirstPerson.FirstPersonController>().enabled = !toggle;
    }

    private void Update()
    {
        if (!isWorking) return;
        Vector3 rotation = new Vector3(0f, 0f, speed * Time.deltaTime);
        moverImage.Rotate(rotation);

        if (Input.GetKeyDown(KeyBinds.action))
        {
            if(isOverlapping == true)
            {
                door.isLocked = false;
                door.sPlayBreakThrough = false;
                door.OpenDoor();
                ToggleQTE(false);
                cutsceneTrigger.SetActive(true);
                self.SetActive(false);
            }
            else
            {
                speed = speed * 1.5f;
                speed = Mathf.Clamp(speed, 100, maxSpeed);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Collider")
            isOverlapping = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Collider")
            isOverlapping = false;
    }
}
