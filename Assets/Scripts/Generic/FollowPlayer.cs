using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float offset;
    private bool isVoid;

    private void Start()
    {
        isVoid = (LayerMask.LayerToName(gameObject.layer) == "FogOfWar") ? true : false;
        offset = transform.position.z - player.position.z;
    }

    private void Update()
    {
        if(!isVoid)
        {
            transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(player.position.z + offset, 0.1f, 2048f));
        }
    }
}
