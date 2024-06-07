using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    namespace Enemy
    {
        [AddComponentMenu("Game/Enemy/Enemy Manager")]
        public class EnemyManager : MonoBehaviour
        {
            [SerializeField] EnemyProperty m_Property;
            [SerializeField] Transform m_Enemies;
            private Dictionary<Type, GameObject> m_EnemyPrefabDict = new Dictionary<Type, GameObject>();
            private float scaleFactor = 1f;

            private Utils.Timer m_IndividualTimer;
            private Utils.Timer m_BatchTimer;

            private bool m_CreateEnemies = false;

            // ��ǰ���εĵ�������
            private Type m_ActiveBatchEnemyType = Type.ORDINARY;

            private List<Vector3> path;

            public void SetScale(float scale)
            {
                scaleFactor = scale;
            }

            public void SetActive(bool active)
            {

                if (active)
                {
                    m_BatchTimer.Start();
                }
                {
                    
                }
            }

            public void Pause()
            {
                foreach(var eb in m_Enemies.GetComponentsInChildren<BaseEnemyBehaviour>())
                {
                    eb.Pause();
                }
            }

            public void Continue()
            {
                foreach (var eb in m_Enemies.GetComponentsInChildren<BaseEnemyBehaviour>())
                {
                    eb.Continue();
                }
            }

            public void CreateEnemy()
            {
                // ÿ��һ��ʱ�䣬�޸ı�־����
                m_BatchTimer.OnUpdate();

                if (m_CreateEnemies) m_IndividualTimer.OnUpdate();

            }

            public void SetPath(List<Vector3> path)
            {
                this.path = path;
            }

            public void Restart()
            {
                SetActive(false);
                /// ɾ�����е���
                foreach (var enemy in m_Enemies.GetComponentsInChildren<BaseEnemyBehaviour>())
                {
                    enemy.Dead();
                }
            }

            /// <summary>
            /// ������������
            /// </summary>
            public void CreateEnemyBatch()
            {
                // ÿ��һ���ε�ʱ�䣬����־����ȡ��
                m_CreateEnemies = !m_CreateEnemies;
                if (m_CreateEnemies) m_IndividualTimer.Start();
            }

            private BaseEnemyBehaviour __eb;
            /// <summary>
            /// �������˸���
            /// </summary>
            public void CreateEnemyIndividual()
            {
                GameObject enemy = Instantiate(m_EnemyPrefabDict[m_ActiveBatchEnemyType], m_Enemies);
                __eb = enemy.GetComponent<BaseEnemyBehaviour>();
                __eb.SetPath(path);
                __eb.SetScale(scaleFactor);
                __eb.SetActive(true);
            }

            public void Load()
            {
                PropertyItem item;
                foreach (Type type in m_Property.Keys)
                {
                    item = m_Property[type];
                    m_EnemyPrefabDict.Add(type, item.prefab);
                    m_IndividualTimer = new Utils.Timer(item.indiviDuration, CreateEnemyIndividual);
                    m_BatchTimer = new Utils.Timer(item.batchDuration, CreateEnemyBatch);
                }
            }
        }
    }
}

