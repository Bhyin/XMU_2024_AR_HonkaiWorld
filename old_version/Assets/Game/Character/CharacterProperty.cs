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
            POINT, // �Ե�ͼĳ��Ϊ���ģ�һ���뾶�ĵ�����
            LINE, // �Խ�ɫ����λ��Ϊ��㣬���ɫ������������length����λ�����Ϊwidth
        }

        [Serializable]
        public class PropertyItem : BasePropertyItem<Type>
        {
            [Tooltip("��ɫԤ����")]
            public GameObject characterPrefab;
        }

        [CreateAssetMenu(fileName = "Character Property", menuName = "Game/Character Property")]
        public class CharacterProperty : BaseProperty<Type, PropertyItem>
        {

        }
    }

    
}

