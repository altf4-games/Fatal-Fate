using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Interactable))]
public class NPCSteve : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform nodeParent;
    [SerializeField] private Transform eyePos;
    [SerializeField] private GameObject[] arrows;
    [SerializeField] private AudioClip[] footsteps;
    [HideInInspector] public bool moveToDest = false;
    private Milk[] allTheMilk;
    private Transform[] nodes;
    private int dialogIndex = 0;
    private int nodeIndex;
    private float velocity;
    private Vector3 previousPos;

    private void Start()
    {
        allTheMilk = Object.FindObjectsOfType<Milk>();
    }

    private void Awake()
    {
        nodes = new Transform[nodeParent.childCount];
        for (int i = 0; i < nodeParent.childCount; i++)
        {
            nodes[i] = nodeParent.GetChild(i);
        }
    }

    private void Update()
    {
        if(moveToDest)
        {
            RaycastHit doorHit;
            if (Physics.Raycast(eyePos.transform.position, transform.forward, out doorHit, 4f))
            {
                if (doorHit.transform.CompareTag("Door"))
                {
                    if (doorHit.transform.GetComponent<Door>().isLocked == false && doorHit.transform.GetComponent<Door>().isOpen == false)
                        doorHit.transform.GetComponent<Door>().OpenDoor();
                }
            }

            velocity = ((transform.position - previousPos).magnitude) / Time.deltaTime;
            previousPos = transform.position;
            animator.SetFloat("velocity", velocity);

            agent.SetDestination(nodes[nodeIndex].position);
            if (agent.remainingDistance <= 1.25f)
            {
                if(nodeIndex != nodes.Length -1)
                nodeIndex++;
            }
        }
    }

    public void TalkToNPC()
    {
        dialogIndex++;
        gameObject.layer = LayerMask.NameToLayer("Default");
        switch (dialogIndex)
        {
            case 1:
                NPC_Init01();
                break;
            /*case 2:
                NPC_Init01();
                break;*/
        }
    }

    private void NPC_Init01()
    {
        Option A = new Option("Can you open the bathroom doors for me, they're jammed.", 'A', NPC_RespoA1);
        Option B = new Option("umm Nothing", 'B', NPC_RespoA2);
        OptionPopup.instance.SetupDecision(A, B, "WHAT!?");
    }

    private string NPC_RespoA1()
    {
        StoryHandlerA.instance.PrintSubtitle(11);
        foreach (GameObject item in arrows)
        {
            item.SetActive(true);
        }
        foreach (Milk mlk in allTheMilk)
        {
            mlk.gameObject.layer = LayerMask.NameToLayer("Interact");
        }
        return null;
    }

    private string NPC_RespoA2()
    {
        StoryHandlerA.instance.PrintSubtitle(8);
        StartCoroutine(Null_RespoA1());
        return null;
    }

    private IEnumerator Null_RespoA1()
    {
        yield return new WaitForSeconds(2f);
        Option A = new Option("Can you open the bathroom doors for me, they're jammed.", 'A', NPC_RespoA1);
        OptionPopup.instance.SetupDecision(A, default, "WHAT!?");
    }

    public void PlayFootstep(int foot)
    {
        AudioManager.instance.PlayAudio(footsteps[foot], 1f, true, 20f, transform.position);
    }
}
