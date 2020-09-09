using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class TrafficSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Transform[] lanes = new Transform[4];
    [SerializeField] private int maxTraffic = 30;
    [HideInInspector] public List<GameObject> traffic;

    private void Start()
    {
        traffic = new List<GameObject>();
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (traffic.Count < maxTraffic)
        {
            int laneGenie = Random.Range(1, 3);
            if(laneGenie == 1)
            {
                Transform designatedLane = lanes[Random.Range(0, 2)];
                Vector3 spawnPoint = designatedLane.GetChild(1).transform.position;

                Collider[] hitColliders = Physics.OverlapSphere(spawnPoint, 1f);
                if(hitColliders.Length == 0)
                {
                    GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Length)]);
                    obj.GetComponent<VehicleBase>().path = designatedLane;
                    obj.GetComponent<VehicleBase>().tf = this;
                    obj.transform.position = spawnPoint;
                    obj.transform.parent = transform;
                    traffic.Add(obj);
                }
            }
            else
            {
                Transform designatedLane = lanes[Random.Range(2, 4)];
                Vector3 spawnPoint = designatedLane.GetChild(1).transform.position;

                Collider[] hitColliders = Physics.OverlapSphere(spawnPoint, 1f);
                if (hitColliders.Length == 0)
                {
                    GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Length)]);
                    obj.GetComponent<VehicleBase>().path = designatedLane;
                    obj.GetComponent<VehicleBase>().tf = this;
                    obj.transform.position = spawnPoint;
                    obj.transform.parent = transform;
                    obj.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    traffic.Add(obj);
                }
            }
            yield return new WaitForSeconds(2);
        }
    }
}
