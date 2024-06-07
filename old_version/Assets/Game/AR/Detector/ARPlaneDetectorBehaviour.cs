using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using static UnityEngine.XR.ARSubsystems.XRCpuImage;

namespace Game.AR
{
    public class ARPlaneDetectorBehaviour : BaseDetectorBehaviour
    {

        [SerializeField] GameObject anchorPrefab;

        ARPlaneManager planeManager;

        ARRaycastManager raycastManager;
        List<ARRaycastHit> arRaycastHits = new List<ARRaycastHit>();
        ARPlane plane = default;


        GameObject anchor;
        Animator animator;

        bool running = true;
        Vector3 m_Position = default, m_Normal = default;

        public override Type type => Type.PLANE;

        public void Detect(out Vector3 position, out Vector3 normal)
        {
            // ���
            Vector3 screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0.0f));
            bool result = Raycast(new Vector2(screenCenter.x, screenCenter.y));
            m_Normal = result ? plane.normal : default;

            if (running)
            {
                // �����ű�λ��
                anchor.gameObject.SetActive(result);
                anchor.transform.position = m_Position;
                anchor.transform.up = m_Normal;
            }
            anchor.gameObject.SetActive(result);

            position = m_Position;
            normal = m_Normal;
        }

        public override void Load()
        {
            anchor = Instantiate(anchorPrefab, transform);
            animator = anchor.GetComponent<Animator>();

            GameObject xrOrigin = GameObject.FindGameObjectWithTag("XRORIGIN");
            planeManager = xrOrigin.GetComponent<ARPlaneManager>();
            raycastManager = xrOrigin.GetComponent<ARRaycastManager>();
        }

        public override void SetActive(bool active)
        {
            foreach (var trackable in planeManager.trackables)
            {
                trackable.gameObject.SetActive(active);
            }

            planeManager.enabled = active;
            raycastManager.enabled = active;

            gameObject.SetActive(active);
        }

        public override void Pause()
        {
            running = false;
            animator.enabled = false;
        }

        public override void Continue()
        {
            running = true;
            animator.enabled = true;
        }

        /// AR���߼�⣬���ܼ��AR Foundation��ARTrackable�Ķ���
        /// ��������ͨ��Ϸ����ʹ��Physics.Raycast������
        private bool Raycast(Vector2 screenPosition)
        {
            arRaycastHits.Clear();
            raycastManager.Raycast(screenPosition, arRaycastHits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinBounds);
            bool result = arRaycastHits.Count > 0;
            plane = result ? arRaycastHits[0].trackable.GetComponent<ARPlane>() : default;
            m_Position = result ? arRaycastHits[0].pose.position : default;
            return result;
        }
    }
}

