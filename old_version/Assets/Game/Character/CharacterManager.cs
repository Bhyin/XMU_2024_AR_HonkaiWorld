using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Hardware;
using UnityEngine;


namespace Game
{
    namespace Character
    {
        /// <summary>
        /// 控制角色的创建和销毁，在Character和Logic之间传递消息
        /// </summary>
        [AddComponentMenu("Game/Character/Character Manager")]
        public class CharacterManager : MonoBehaviour
        {
            [SerializeField] CharacterProperty m_CharacterProperty;
            [SerializeField] Transform m_Characters;
            private Dictionary<Type, GameObject> m_CharacterPrefabDict = new Dictionary<Type, GameObject>();
            private float scaleFactor = 1f;

            public void CreateCharacter(Vector3 position, Vector3 forward, Vector3 up)
            {
                GameObject character = Instantiate(m_CharacterPrefabDict[Type.C001], m_Characters);
                BaseCharacterBehaviour cb = character.GetComponent<BaseCharacterBehaviour>();
                cb.SetScale(scaleFactor);
                cb.SetTransform(position, forward, up);
                cb.SetActive(true);
            }

            /// <summary>
            /// 设置角色缩放，用于适配地图缩放
            /// </summary>
            public void SetScale(float scale) 
            {
                scaleFactor = scale;
            }

            public void SetMovement(float length)
            {
                foreach (var character in m_Characters.GetComponentsInChildren<BaseCharacterBehaviour>())
                {
                    character.SetMovement(length);
                }
            }

            public void SetActive(bool active)
            {
                foreach (var character in m_Characters.GetComponentsInChildren<BaseCharacterBehaviour>())
                {
                    character.SetActive(active);
                }
            }

            public void Restart()
            {
                foreach (var character in m_Characters.GetComponentsInChildren<BaseCharacterBehaviour>())
                {
                    character.Restart();
                }
                SetActive(false);
            }

            /**
             * Awake
             * **/
            public void Load()
            {
                PropertyItem item;
                foreach (Type type in m_CharacterProperty.Keys)
                {
                    item = m_CharacterProperty[type];
                    m_CharacterPrefabDict.Add(type, item.characterPrefab);
                }
            }
        }
    }
}

