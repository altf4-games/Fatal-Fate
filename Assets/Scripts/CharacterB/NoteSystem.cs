using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(Interactable))]
public class NoteSystem : MonoBehaviour
{
    [SerializeField] private GameObject baseNote;
    [SerializeField] private PostProcessProfile ppf;
    private bool isOpen = false;
    private DepthOfField dof;

    private void Start()
    {
        ppf.TryGetSettings<DepthOfField>(out dof);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && isOpen)
        {
            ToggleNote();
        }
    }

    public void ToggleNote()
    {
        isOpen = !isOpen;
        baseNote.SetActive(isOpen);
        Time.timeScale = (isOpen == true) ? 0 : 1;
        dof.enabled.value = (isOpen == true) ? false : true;
    }
}