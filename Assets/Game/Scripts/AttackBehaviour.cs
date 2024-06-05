using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AttackBehaviour : MonoBehaviour
{
    // 攻击来源
    public enum SourceType
    {
        Enemy,
        Character
    }

    public SourceType sourceType;
    public int damage;
    public Vector3 attackDirection;
    public float moveSpeed = 2;
    public float maxMoveDistance = 50;
    public float maxMoveTime = 10;

    public GameObject hitPrefab;
    PlayableDirector hitDirector;


    private void Start()
    {
        GameObject hit = Instantiate(hitPrefab);
        hit.tag = "Bullet";
        hit.transform.localScale = transform.localScale * 0.5f;
        hitDirector = hit.GetComponent<PlayableDirector>();
        hitDirector.Stop();
    }

    public void OnEnemyCreate()
    {
        Debug.Log("Create Emeny");
    }

    // Update is called once per frame
    void Update()
    {
        if (!ScoreCounter.gameStart)
        {
            hitDirector.Stop();
            return;
        }
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    public void OnAttack()
    {
        hitDirector.transform.position = transform.position;
        hitDirector.Play();
        hitDirector.gameObject.tag = "InvBullet";

        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.CompareTag("Enemy") && sourceType == SourceType.Character)
        {
            OnAttack();
        }
        else if(target.CompareTag("Character") && sourceType == SourceType.Enemy)
        {
            OnAttack();
        }
    }


    // 找到距离最近的敌人
    GameObject FindNearestEnemy(GameObject[] enemies)
    {
        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }
}
