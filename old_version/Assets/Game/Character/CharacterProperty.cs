using Game.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{


    namespace Character
    {
        public enum Type
        {
            C001,
        }

        public enum SkillType
        {
            POINT, // 以地图某点为中心，一定半径的的区域
            LINE, // 以角色所在位置为起点，向角色攻击方向延伸length个单位，宽度为width
        }

        [Serializable]
        public class PropertyItem : BasePropertyItem<Type>
        {
            [Tooltip("角色预制体")]
            public GameObject characterPrefab;
        }

        [CreateAssetMenu(fileName = "Character Property", menuName = "Game/Character Property")]
        public class CharacterProperty : BaseProperty<Type, PropertyItem>
        {

        }
    }

    
}

