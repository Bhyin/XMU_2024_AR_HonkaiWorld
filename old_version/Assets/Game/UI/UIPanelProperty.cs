using Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    namespace UI
    {
        public enum Type
        {
            SCAN, // ɨ�����
            CONFIG, // ���ý���
            GAME, // ��Ϸ����
            PAUSE, // ��ͣ����
            SETTING, // ���ý���
        }

        [Serializable]
        public class PropertyItem : BasePropertyItem<Type>
        {
            public GameObject panelPrefab;
        }

        [CreateAssetMenu(fileName = "UIPanel Property", menuName = "Game/UIPanel Property")]
        public class UIPanelProperty : BaseProperty<Type, PropertyItem> { }
    }

}
