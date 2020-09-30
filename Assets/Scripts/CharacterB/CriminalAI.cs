using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CriminalAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform eyePos;
    [SerializeField] private Transform nodeParent;
    [SerializeField] private AudioClip[] footsteps;
    private Transform[] nodes;
    private int nodeIndex;
    private float velocity;
    private Vector3 previousPos;

    private void Start()
    {
        nodes = new Transform[nodeParent.childCount];
        for (int i = 0; i < nodeParent.childCount; i++)
        {
            nodes[i] = nodeParent.GetChild(i);
        }
        StartCoroutine(FSM());
    }

    private void Update()
    {
        RaycastHit doorHit;
        Debug.DrawRay(eyePos.transform.position, eyePos.transform.forward * 24, Color.red);
        if (Physics.Raycast(eyePos.transform.position, eyePos.transform.forward, out doorHit, 4f))
        {
            if (doorHit.transform.CompareTag("Door"))
            {
                if (doorHit.transform.GetComponent<Door>().isLocked == false && doorHit.transform.GetComponent<Door>().isOpen == false)
                {
                    doorHit.transform.GetComponent<Door>().sPlayerLock = false;
                    doorHit.transform.GetComponent<Door>().OpenDoor();
                }
            }
        }

        velocity = ((transform.position - previousPos).magnitude) / Time.deltaTime;
        previousPos = transform.position;
        animator.SetFloat("velocity", velocity);
    }

    private IEnumerator FSM()
    {
        agent.SetDestination(nodes[nodeIndex].position);
        if (agent.remainingDistance <= .1f)
        {
            if (nodeIndex < nodes.Length - 1)
            {
                nodeIndex++;
                yield return new WaitForSeconds(2);
                if(nodeIndex == 12)
                {
                    NPC_Init01();
                    agent.speed = 0;
                }
            }
        }
        yield return null;
        StartCoroutine(FSM());
    }

    private void NPC_Init01()
    {
        Option A = new Option("Yeah. But why'd you ask that?", 'A', NPC_RespoA1);
        Option B = new Option("No. Also why should I answer you?", 'B', null, true);
        OptionPopup.instance.SetupDecision(A, B, "Are you working alone today?");
    }

    private string NPC_RespoA1()
    {
        print("done");
        return null;
    }

    public void PlayFootstep(int foot)
    {
        AudioManager.instance.PlayAudio(footsteps[foot], 1f, true, 20f, transform.position);
    }
}
