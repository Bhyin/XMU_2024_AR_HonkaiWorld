using Game.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character
{
    public class AttackBehaviour : MonoBehaviour
    {
        [SerializeField] float damage = 8f;
        [SerializeField] float speed = 1.0f;

        private float squareLength = 1.0f; // 格子长度
        private Vector3 direction;
        BaseEnemyBehaviour target;
        
        public void SetTarget(BaseEnemyBehaviour enemy)
        {
            target = enemy;
        }

        public void SetScale(float scale)
        {
            transform.localScale *= scale;
        }

        public void SetMovement(float length)
        {
            squareLength = length;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        private void Update()
        {
            if (target == null)
            {
                Accomplish();
                return;
            }

            direction = target.CenterPosition - transform.position;
            direction.Normalize();
            direction *= squareLength; // 方块长度

            direction *= speed * Time.deltaTime;

            if(Vector3.Distance(transform.position, target.CenterPosition) < 1.2f * direction.magnitude)
            {
                target.Damage(damage);
                Accomplish();
                return;
            }

            transform.position = transform.position + direction; 
        }

        public void Accomplish()
        {
            Destroy(gameObject);
        }
    }
}

