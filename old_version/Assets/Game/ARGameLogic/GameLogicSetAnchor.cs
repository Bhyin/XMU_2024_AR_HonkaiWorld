using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.ARFoundation;

public partial class GameLogic : MonoBehaviour
{
    public void SetAnchorUpdate()
    {
        // ����⵽ƽ��
        if (arPlaneManager.trackables.count > 0)
        {
            // ��δ����ê�㣬�򼤻����ê���ƶ�����һ��trackable������
            if (!anchorBehaviour.gameObject.activeInHierarchy)
            {
                anchorBehaviour.gameObject.SetActive(true);
                ARPlane plane = null;
                foreach (ARPlane p in arPlaneManager.trackables)
                {
                    plane = p;
                    if (plane != null) break;
                }
                anchorBehaviour.transform.position = plane.center;
                anchorBehaviour.transform.up = plane.normal;
            }


            if (interactionManager.longPress && anchorBehaviour.selected)
            {
                // ê������ָ�ƶ�����ARPlane���ƶ�
                aRRaycastManager.Raycast(interactionManager.position, arRaycastHits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinBounds);
                if (arRaycastHits.Count > 0)
                {
                    ARRaycastHit arHit = arRaycastHits[0];
                    ARPlane p = arHit.trackable.GetComponent<ARPlane>();
                    anchorBehaviour.transform.position = arHit.pose.position;
                    anchorBehaviour.transform.up = p.normal;
                }
                arRaycastHits.Clear();
            }
        }
        else
        {
            // δ��⵽ƽ��,ʼ�ղ������ű�
            anchorBehaviour.SetActive(false);
        }
    }

    /// <summary>
    /// ��ʼ����ê��
    /// </summary>
    public void SetAnchorStart()
    {
        anchorBehaviour.SetActive(true);
        uiManager.OpenPanel(PanelItems.game_SetAnchorPanel);

        StartArDetection();
    }

    /// <summary>
    /// ����ê�����
    /// </summary>
    public void SetAnchorFinish()
    {
        // ���δ�ܼ�⵽ƽ��ͽ�anchor�ƶ������ǰ��һ������
        if(arPlaneManager.trackables.count == 0)
        {
            anchorBehaviour.transform.position = mainCamera.transform.position + mainCamera.transform.forward;
        }


        anchorBehaviour.SetActive(false);
        uiManager.ClosePanel();

        FinishArDetection();
    }

    void StartArDetection()
    {
        arPlaneManager.SetTrackablesActive(true);
        arPlaneManager.enabled = true;

        aRRaycastManager.SetTrackablesActive(true);
        aRRaycastManager.enabled = true;
    }

    void FinishArDetection()
    {
        arPlaneManager.SetTrackablesActive(false);
        arPlaneManager.enabled = false;

        aRRaycastManager.SetTrackablesActive(false);
        aRRaycastManager.enabled = false;
    }

    /**
     * Set Anchor�׶ε��¼���Ӧ����
     * 
     * **/

    public void SetAnchorPressCallback()
    {

    }

    public void SetAnchorLongPressCallback()
    {
        // δ��ѡ��
        if (!anchorBehaviour.selected)
        {
            // ���߼��
            Vector2 screenPosition = interactionManager.position;
            Ray ray = mainCamera.ScreenPointToRay(screenPosition);
            // �����⵽���壬�Ҽ�⵽anchor��ѡ��
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == anchorBehaviour.gameObject)
            {
                anchorBehaviour.Select();
            }
        }
    }

    public void SetAnchorClickCallback()
    {
        // �Ѿ���ѡ��
        if (anchorBehaviour.selected)
        {
            // ���߼��
            Vector2 screenPosition = interactionManager.position;
            Ray ray = mainCamera.ScreenPointToRay(screenPosition);
            // ���߼�ⲻ�ɹ�������ɹ�����⵽�����岻��anchor����������δѡ�С�
            if (!Physics.Raycast(ray, out hit) || hit.collider.gameObject != anchorBehaviour.gameObject)
            {
                anchorBehaviour.UnSelect();
            }
        }
    }

    public void SetAnchorReleaseCallback()
    {

    }
}
