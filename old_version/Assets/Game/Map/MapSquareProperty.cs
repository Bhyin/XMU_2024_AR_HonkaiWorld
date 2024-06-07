using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    namespace Map
    {
        public enum Type
        {
            START, // 怪物创建的起点，无图案，颜色为绿色，亮度恒定
            END, // 怪物移动的终点， 无图案，颜色为绿色，亮度恒定
            VIRTUAL_BARRIER, // 虚拟障碍，图案为‘X’，颜色为红色，亮度恒定
            REAL_BARRIER, // 真实障碍，无图案，颜色为红色，亮度恒定，无实例
            ROAD, // 怪物移动的路径，图案为一个标记运动方向的箭头，类似于“->”，颜色为绿色，亮度周期性变化，像波一样从起点向终点传播，无实例
            SPACE, // 可供放置角色的方块，无图案，颜色为灰色，亮度恒定，无实例
            CHARACTER, // 已被角色占用的方块，图案为一个圆环，颜色为蓝色，一般情况下亮度恒定，释放技能时亮度增加，且角色攻击范围内的方块的颜色都会变为蓝色
            PLACINGCHAR, // 处于放置角色的状态时，所有SPACE方块的状态都会变为PLACING状态，无图案，颜色为紫色，无实例
            PLACINGTARGET, // 处于放置角色的状态时，放置的目标位置的方块的状态，图案为圆圈，颜色为紫色，高亮，无实例。
        }

        public enum Direction
        {
            RIGHT = 0,
            UP = 1,
            LEFT = 2,
            DOWN = 3,
        }


        [Serializable]
        public class PropertyItem : BasePropertyItem<Type>
        {
            public Texture2D icon;
            public Color color;
            public float emission;
        }

        [CreateAssetMenu(fileName = "Map Square Property", menuName = "Game/Map Square Property")]
        public class MapSquareProperty : BaseProperty<Type, PropertyItem> 
        {
            [Header("Map Square Shader")]
            public Shader shader;
            public string uniformIcon = "_Icon";
            public string uniformColor = "_Color";
            public string uniformEmisson = "_Emisson";
            public string uniformTransparency = "_Transparency";
            public string uniformDirection = "_Direction";
        }
    }
}
