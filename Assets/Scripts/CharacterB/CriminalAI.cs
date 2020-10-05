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
    [SerializeField] private Transform player;
    [SerializeField] private GameObject handMilk;
    [SerializeField] private GameObject counterMilk;
    [SerializeField] private GameObject counterBlocker;
    [SerializeField] private Door backDoor;
    [SerializeField] private Door bathroomDoor;
    [SerializeField] private AudioClip powerDown;
    [SerializeField] private AudioClip[] footsteps;
    private Transform[] nodes;
    private int nodeIndex;
    private float velocity;
    private Vector3 previousPos;
    private bool followMode;

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
        if(!followMode)
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
        }
        if(followMode == true)
        {
            agent.SetDestination(player.position);
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
                if (nodeIndex == 12)
                {
                    NPC_Init01();
                    agent.speed = 0;
                    backDoor.isLocked = false;
                }
                if (nodeIndex == 18)
                {
                    AudioManager.instance.PlayAudio(powerDown, 1.0f);
                }
                if (nodeIndex == 25)
                {
                    NPC_Init02();
                    agent.speed = 0;
                }
                if (nodeIndex == 27)
                {
                    handMilk.SetActive(true);
                }
                if (nodeIndex == 30)
                {
                    yield return new WaitForSeconds(2);
                    handMilk.SetActive(false);
                    counterMilk.SetActive(true);
                    counterBlocker.SetActive(false);
                    StoryHandlerB.instance.PrintSubtitle();
                    StoryHandlerB.instance.PrintSubtitle();
                    bathroomDoor.isLocked = false;
                    agent.speed = 4.5f;
                    agent.stoppingDistance = 4;
                    followMode = true;
                    StopCoroutine("FSM");
                    yield break;
                }
            }
        }
        yield return null;
        StartCoroutine(FSM());
    }

    private void NPC_Init01()
    {
        Option A = new Option("Yeah. But why'd you ask that?", 'A', NPC_RespoA1);
        Option B = new Option("No. Also, why should I answer you?", 'B', null, true);
        OptionPopup.instance.SetupDecision(A, B, "Are you working alone today?");
    }

    private string NPC_RespoA1()
    {
        agent.speed = 2.5f;
        return null;
    }

    private void NPC_Init02()
    {
        Option A = new Option("No. The bathroom is only accessible to customers, You gotta buy something.", 'A', NPC_RespoA2);
        Option B = new Option("No. Go away!!", 'B', null, true);
        OptionPopup.instance.SetupDecision(A, B, "Can you open the bathroom doors for me, they're jammed.");
    }

    private string NPC_RespoA2()
    {
        agent.speed = 2.5f;
        return null;
    }

    public void PlayFootstep(int foot)
    {
        AudioManager.instance.PlayAudio(footsteps[foot], 1f, true, 20f, transform.position);
    }
}
