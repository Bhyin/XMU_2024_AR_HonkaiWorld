using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Game
{
    namespace Enemy
    {
        [RequireComponent(typeof(Collider), typeof(Rigidbody))]
        public class BaseEnemyBehaviour : MonoBehaviour
        {
            [SerializeField] float maxHp = 100;
            [SerializeField] GameObject hpBar;
            [SerializeField] Shader hpShader;
            Material hpMat;
            float hp;


            List<Vector3> path = new List<Vector3>();
            [SerializeField] float speed = 1f; // 每秒走过的格子数
            [SerializeField] Transform m_Center;

            int pathIndex = 0; // 当前所在方块对应路径的下标
            // 运动单位，即路径上两个方块中心的实际距离和实际向量，不再使用单位向量进行位移,地图越大，单位越大
            float unitDistance;
            Vector3 unitVector;
            float movementThreshold;
            float speedFactor;

            private bool running = true;

            public Vector3 Position => transform.position;
            public Vector3 CenterPosition => m_Center.position;

            private void OnEnable()
            {
                hp = maxHp;
                hpMat = new Material(hpShader);
                hpBar.GetComponent<MeshRenderer>().sharedMaterial = hpMat;
            }

            public void SetScale(float scale)
            {
                transform.localScale *= scale;
            }

            public void Pause()
            {
                running = false;
            }

            public void Continue()
            {
                running = true;
            }

            void Update()
            {
                if (!running) return;

                speedFactor = speed * Time.deltaTime;
                movementThreshold = unitDistance * speedFactor * 1.2f;

                Vector3 targetPos = transform.position + speedFactor * unitVector;
                if (Vector3.Distance(targetPos, path[pathIndex + 1]) < movementThreshold)
                {
                    ++pathIndex;

                    if (pathIndex >= path.Count - 1)
                    {
                        Dead();
                        running = false;
                        return;
                    }

                    targetPos = path[pathIndex];
                    CalMovementUnit();
                }
                transform.position = targetPos;
            }

            private void LateUpdate()
            {
                // HP Bar
                hpBar.transform.rotation = Quaternion.LookRotation(transform.up, Camera.main.transform.position - hpBar.transform.position);
                hpMat.SetFloat("_Hp", hp / maxHp);
            }

            public void SetActive(bool active)
            {
                gameObject.SetActive(active);
            }

            public void Dead()
            {
                Destroy(gameObject);
            }


            public void SetPath(List<Vector3> p)
            {
                path = p;
                transform.position = path[pathIndex];
                CalMovementUnit();
            }

            void CalMovementUnit()
            {
                unitVector = path[pathIndex + 1] - path[pathIndex];
                unitDistance = unitVector.magnitude;
            }

            public void Damage(float damage)
            {
                hp -= damage;
                if (hp < 0)
                {
                    Dead();
                }
            }
        }
    }
}

