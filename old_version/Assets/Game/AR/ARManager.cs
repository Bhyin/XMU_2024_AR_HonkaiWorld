using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Game
{
    namespace AR
    {
        [AddComponentMenu("Game/AR/AR Manager")]
        public class ARManager : MonoBehaviour
        {
            [SerializeField] ARDetectorProperty m_Property;
            [SerializeField] Transform m_Detectors; 

            Dictionary<Type, BaseDetectorBehaviour> m_DetectorDict = new Dictionary<Type, BaseDetectorBehaviour>();

            public BaseDetectorBehaviour GetBehaviour(Type type)
            {
                BaseDetectorBehaviour behaviour;
                if(m_DetectorDict.TryGetValue(type, out behaviour))
                {
                    return behaviour;
                }
                return default;
            }

            public void DetectPlane(out Vector3 position, out Vector3 normal)
            {
                ARPlaneDetectorBehaviour behaviour = GetBehaviour(Type.PLANE) as ARPlaneDetectorBehaviour;
                behaviour.Detect(out position, out normal);
            }

            public void Pause()
            {
                foreach (var detector in m_DetectorDict.Values.ToList())
                {
                    detector.Pause();
                }
            }

            public void Continue()
            {
                foreach (var detector in m_DetectorDict.Values.ToList())
                {
                    detector.Continue();
                }
            }

            public void SetActive(bool active)
            {
                foreach (var detector in m_DetectorDict.Values.ToList())
                {
                    detector.SetActive(active);
                }
            }

            public void Restart()
            {
                SetActive(false);
            }

            public void Load()
            {
                GameObject detector;
                PropertyItem item;
                foreach(Type type in m_Property.Keys)
                {
                    item = m_Property[type];

                    detector = Instantiate(item.detectorPrefab, m_Detectors);

                    BaseDetectorBehaviour db = detector.GetComponent<BaseDetectorBehaviour>();
                    db.Load();

                    m_DetectorDict.Add(type, db);
                }
            }
        }
    }
}

