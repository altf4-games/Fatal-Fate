using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CriminalDeath : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Collider normalCol;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AudioClip oof;
    [SerializeField] private AudioClip flatline;
    [SerializeField] private CanvasGroup bg;
    [SerializeField] private Rigidbody[] rbs;
    [SerializeField] private Collider[] colls;
    [SerializeField] private AudioClip[] footsteps;
    private LayerMask trafficLayer;

    private void Start()
    {
        trafficLayer = LayerMask.NameToLayer("Traffic");
        foreach (Rigidbody rb in rbs) {
            rb.isKinematic = true;
        }
        foreach (Collider coll in colls) {
            coll.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == trafficLayer)
        {
            anim.enabled = false;
            normalCol.enabled = false;
            agent.enabled = false;
            End();
            AudioManager.instance.PlayAudio(oof, 1.0f);
            foreach (Rigidbody rb in rbs) {
                rb.isKinematic = false;
            }
            foreach (Collider coll in colls) {
                coll.enabled = true;
            }
        }
    }

    private void End()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets.Characters
        .FirstPerson.FirstPersonController>().enabled = false;
        bg.alpha = 0f;
        bg.GetComponent<Image>().color = Color.black;
        bg.gameObject.SetActive(true);
        LeanTween.alphaCanvas(bg, 1.0f, .5f);
        AudioManager.instance.PlayAudio(flatline, 1.0f);
        Client.instance.RetriveData();
        Client.instance.GetComponent<EndScreen>().isActive = true;
    }

    public void PlayFootstep(int foot)
    {
        AudioManager.instance.PlayAudio(footsteps[foot], 1f, true, 20f, transform.position);
    }
}
