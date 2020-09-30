using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MarkCarAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform nodeParent;
    [SerializeField] private GameObject drawingBoard;
    [SerializeField] private GameObject criminalAI;
    [SerializeField] private Animation[] wheels;
    private Transform[] nodes;
    private int nodeIndex;

    private void Start()
    {
        nodes = new Transform[nodeParent.childCount];
        for (int i = 0; i < nodeParent.childCount; i++)
        {
            nodes[i] = nodeParent.GetChild(i);
        }
        StartCoroutine(Track());
    }

    private IEnumerator Track()
    {
        agent.SetDestination(nodes[nodeIndex].position);
        if (agent.remainingDistance <= .25f)
        {
            if (nodeIndex < nodes.Length - 1)
            {
                nodeIndex++;
                if (nodeIndex == 6)
                {
                    agent.speed = 0;
                    yield return new WaitForSeconds(5);
                    agent.speed = 10;
                }

                if (nodeIndex == 9)
                {
                    agent.enabled = false;
                    drawingBoard.SetActive(true);
                    criminalAI.SetActive(true);
                    transform.rotation = Quaternion.Euler(new Vector3(0, -90f, 0f));
                    foreach (Animation wheel in wheels)
                    {
                        wheel.enabled = false;
                    }
                    Destroy(this);
                }
            }
        }
        yield return null;
        StartCoroutine(Track());
    }
}
