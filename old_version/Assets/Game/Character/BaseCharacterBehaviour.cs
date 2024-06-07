using Game.Enemy;
using Game.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    namespace Character
    {
        [RequireComponent(typeof(Collider), typeof(Rigidbody))]
        public class BaseCharacterBehaviour : MonoBehaviour
        {
            /// <summary>
            /// 攻击间隔
            /// </summary>
            public float attackDuration = 0.8f;
            [SerializeField] GameObject attackPrefab;
            [SerializeField] Transform m_Attacks;
            [SerializeField] Transform m_Center;

            private Timer m_AttackTimer;
            [SerializeField] int attackRadius = 3;
            //private DistanceType attackDisType;

            //// TODO 也许会用到？
            //private DistanceType skillDisType;
            //private SkillType skillType;
            //private int skillLength;
            //private int skillWidth;

            private BaseEnemyBehaviour m_Target = null;
            float scaleFactor = 1f;
            float squareLength = 1f;

            private void Awake()
            {
                BoxCollider collider = GetComponent<BoxCollider>();
                float size = 2.0f * attackRadius + 1.0f;
                collider.size = new Vector3(size, collider.size.y, size);
            }

            public void OnEnable()
            {
                m_AttackTimer = new Timer(attackDuration, Attack);
            }

            public void SetActive(bool active)
            {
                gameObject.SetActive(active);
            }

            public void Restart()
            {
                foreach (AttackBehaviour attack in m_Attacks.GetComponentsInChildren<AttackBehaviour>())
                {
                    attack.Accomplish();
                }
                Dead();
            }

            //public void SetSkill(SkillType type, DistanceType disType, int length, int width)
            //{
            //    skillType = type;
            //    skillDisType = disType;
            //    skillLength = length;
            //    skillWidth = width;
            //}

            //public void SetAttack(DistanceType disType, int radius)
            //{
            //    attackDisType = disType;
            //    attackRadius = radius;
            //}

            public void SetScale(float scale)
            {
                transform.localScale *= scale;
                scaleFactor = scale;
            }

            public void SetMovement(float squareLength)
            {
                this.squareLength = squareLength;
            }

            public void SetTransform(Vector3 position, Vector3 forward, Vector3 up)
            {
                transform.position = position;
                transform.rotation = Quaternion.LookRotation(forward, up);
            }


            // Update is called once per frame
            void Update()
            {
                // 如果目标非空，则每隔一段时间向目标发动攻击
                if (m_Target != null)
                {

                    m_AttackTimer.OnUpdate();
                }
            }

            public void Dead()
            {
                Destroy(gameObject);
            }

            public void Attack()
            {
                GameObject attack = Instantiate(attackPrefab);
                attack.transform.parent = m_Attacks;
                attack.transform.position = m_Center.position;
                AttackBehaviour attackBehaviour = attack.GetComponent<AttackBehaviour>();
                attackBehaviour.SetScale(scaleFactor);
                attackBehaviour.SetTarget(m_Target);
                attackBehaviour.SetMovement(squareLength);
                attackBehaviour.SetActive(true);
            }

            private void OnTriggerEnter(Collider other)
            {
                // 开始攻击
                BaseEnemyBehaviour eb = other.GetComponent<BaseEnemyBehaviour>();
                // 如果是敌人，且当前没有目标敌人，或它与角色的距离小于当前目标敌人，则设为目标

                if (eb == null) return;
                if (m_Target == null)
                {
                    m_Target = eb;
                    m_AttackTimer.Start();
                    return;
                }

                if (Vector3.Distance(transform.position, eb.Position) < Vector3.Distance(transform.position, m_Target.Position))
                {
                    m_Target = eb;
                    m_AttackTimer.Start();
                }
            }

            private void OnTriggerStay(Collider other)
            {
                BaseEnemyBehaviour eb = other.GetComponent<BaseEnemyBehaviour>();


                if (eb == null) return;
                if (m_Target == null)
                {
                    m_Target = eb;
                    m_AttackTimer.Start();
                    return;
                }

                if (Vector3.Distance(transform.position, eb.Position) < Vector3.Distance(transform.position, m_Target.Position))
                {
                    m_Target = eb;
                    m_AttackTimer.Start();
                }
            }

            private void OnTriggerExit(Collider other)
            {
                // 结束攻击
                BaseEnemyBehaviour eb = other.GetComponent<BaseEnemyBehaviour>();
                // 如果是敌人，且是目标敌人，则将目标设为空，否则不变
                if (eb != null && eb.Equals(m_Target)) m_Target = null;
            }
        }
    }
}

