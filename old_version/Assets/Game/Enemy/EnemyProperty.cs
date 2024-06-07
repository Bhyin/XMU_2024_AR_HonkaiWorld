using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    namespace Enemy
    {
        /// <summary>
        /// ��������
        /// </summary>
        public enum Type
        {
            ORDINARY,
        }

        [Serializable]
        public class PropertyItem : BasePropertyItem<Type>
        {
            [Tooltip("����Ԥ����")]
            public GameObject prefab;
            [Tooltip("�����������ɼ��")]
            public float indiviDuration;
            [Tooltip("ÿ���������ɼ��")]
            public float batchDuration;
        }

        [CreateAssetMenu(fileName = "Enemy Property", menuName = "Game/Enemy Property")]
        public class EnemyProperty : BaseProperty<Type, PropertyItem> { }
    }
}

