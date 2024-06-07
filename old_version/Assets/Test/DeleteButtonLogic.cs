using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DeleteButtonLogic : MonoBehaviour
{

    [SerializeField]
    XRInteractionGroup m_InteractionGroup;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void DeleteFocusedObject()
    {
        var currentFocusedObject = m_InteractionGroup.focusInteractable;
        if (currentFocusedObject != null)
        {
            Destroy(currentFocusedObject.transform.gameObject);
        }
    }
}
