using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.AR
{
    public enum Type
    {
        PLANE,
        HAND,
        OBJECT
    }

    [Serializable]
    public class PropertyItem : BasePropertyItem<Type>
    {
        public GameObject detectorPrefab;
    }

    [CreateAssetMenu(fileName = "AR Detector Property", menuName = "Game/AR Detector Property")]
    public class ARDetectorProperty : BaseProperty<Type, PropertyItem> { }

}


