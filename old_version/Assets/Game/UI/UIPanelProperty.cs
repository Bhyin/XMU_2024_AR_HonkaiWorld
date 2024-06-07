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
            SCAN, // 扫描界面
            CONFIG, // 配置界面
            GAME, // 游戏界面
            PAUSE, // 暂停界面
            SETTING, // 设置界面
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
