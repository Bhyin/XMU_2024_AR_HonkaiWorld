using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.ARFoundation;

public partial class GameLogic : MonoBehaviour
{
    public void SetAnchorUpdate()
    {
        // 若检测到平面
        if (arPlaneManager.trackables.count > 0)
        {
            // 若未激活锚点，则激活，并将锚点移动到第一个trackable的中心
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
                // 锚点随手指移动而在ARPlane上移动
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
            // 未检测到平面,始终不激活信标
            anchorBehaviour.SetActive(false);
        }
    }

    /// <summary>
    /// 开始设置锚点
    /// </summary>
    public void SetAnchorStart()
    {
        anchorBehaviour.SetActive(true);
        uiManager.OpenPanel(PanelItems.game_SetAnchorPanel);

        StartArDetection();
    }

    /// <summary>
    /// 设置锚点结束
    /// </summary>
    public void SetAnchorFinish()
    {
        // 如果未能检测到平面就将anchor移动到相机前方一定距离
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
     * Set Anchor阶段的事件响应函数
     * 
     * **/

    public void SetAnchorPressCallback()
    {

    }

    public void SetAnchorLongPressCallback()
    {
        // 未被选中
        if (!anchorBehaviour.selected)
        {
            // 射线检测
            Vector2 screenPosition = interactionManager.position;
            Ray ray = mainCamera.ScreenPointToRay(screenPosition);
            // 如果检测到物体，且检测到anchor，选中
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == anchorBehaviour.gameObject)
            {
                anchorBehaviour.Select();
            }
        }
    }

    public void SetAnchorClickCallback()
    {
        // 已经被选中
        if (anchorBehaviour.selected)
        {
            // 射线检测
            Vector2 screenPosition = interactionManager.position;
            Ray ray = mainCamera.ScreenPointToRay(screenPosition);
            // 射线检测不成功，或检测成功但检测到的物体不是anchor，将其设置未选中。
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
