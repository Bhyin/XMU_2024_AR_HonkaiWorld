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

            // 当前批次的敌人类型
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
                // 每隔一段时间，修改标志变量
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
                /// 删除所有敌人
                foreach (var enemy in m_Enemies.GetComponentsInChildren<BaseEnemyBehaviour>())
                {
                    enemy.Dead();
                }
            }

            /// <summary>
            /// 创建敌人批次
            /// </summary>
            public void CreateEnemyBatch()
            {
                // 每隔一批次的时间，将标志变量取反
                m_CreateEnemies = !m_CreateEnemies;
                if (m_CreateEnemies) m_IndividualTimer.Start();
            }

            private BaseEnemyBehaviour __eb;
            /// <summary>
            /// 创建敌人个体
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

