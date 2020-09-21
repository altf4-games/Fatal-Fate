using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHead : MonoBehaviour
{
    [SerializeField] private Transform headBone;
    [SerializeField] private Transform player;

    private void LateUpdate()
    {
        headBone.LookAt(player);
        Vector3 rot = headBone.transform.rotation.eulerAngles;
        rot.x = 0f;
        rot.z = 0f;
        headBone.transform.rotation = Quaternion.Euler(rot);
    }
}
