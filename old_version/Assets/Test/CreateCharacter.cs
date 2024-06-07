using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Jobs;

public class CreateCharacter : MonoBehaviour
{
    public GameObject cameraToFace;
    public GameObject prefab;

    [SerializeField]
    XRRayInteractor interactor;
    [SerializeField]
    XRScreenSpaceController screenSpaceController;
    bool hadSelection;

    //private void Update()
    //{
    //    if(transform.childCount < 1)
    //    {
    //        var attemp = false;

    //        var currState = interactor.xrController.currentControllerState;
    //        if (currState.selectInteractionState.activatedThisFrame)
    //        {
    //            hadSelection = interactor.hasSelection;
    //        }
    //        else if (currState.selectInteractionState.active)
    //        {
    //            hadSelection |= interactor.hasSelection;
    //        }
    //        else if (currState.selectInteractionState.deactivatedThisFrame)
    //        {
    //            attemp = !interactor.hasSelection && !hadSelection;
    //        }

    //        ARRaycastHit hit;


    //        if (attemp && interactor.TryGetCurrentARRaycastHit(out hit))
    //        {
    //            ARPlane plane = hit.trackable as ARPlane;
    //            if (plane == null) return;
    //            TryCreateObject(hit.pose.position, plane.normal);
    //        }
    //    }
    //}

    void TryCreateObject(Vector3 position, Vector3 normal)
    {
        var obj = Instantiate(prefab).transform;
        obj.position = position;
        Vector3 facePosition = cameraToFace.transform.position;
        Vector3 forward = facePosition - position;
        //BurstMathUtility.ProjectOnPlane(forward, normal, out var projectedForward);
        //obj.transform.rotation = Quaternion.LookRotation(projectedForward, normal);

        obj.SetParent(transform);
    }

    //public void DeleteAllObjects()
    //{
    //    foreach (Transform child in transform)
    //    {
    //        Destroy(child.gameObject);
    //    }
    //}
}
