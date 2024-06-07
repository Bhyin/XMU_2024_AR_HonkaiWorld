using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    namespace Enemy
    {
        /// <summary>
        /// 敌人种类
        /// </summary>
        public enum Type
        {
            ORDINARY,
        }

        [Serializable]
        public class PropertyItem : BasePropertyItem<Type>
        {
            [Tooltip("敌人预制体")]
            public GameObject prefab;
            [Tooltip("单个敌人生成间隔")]
            public float indiviDuration;
            [Tooltip("每批敌人生成间隔")]
            public float batchDuration;
        }

        [CreateAssetMenu(fileName = "Enemy Property", menuName = "Game/Enemy Property")]
        public class EnemyProperty : BaseProperty<Type, PropertyItem> { }
    }
}

