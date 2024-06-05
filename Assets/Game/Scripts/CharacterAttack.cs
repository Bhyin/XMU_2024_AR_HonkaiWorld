using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class CharacterAttack : MonoBehaviour
{
    bool isAttacking = false;
    

    List<EnemyBehaviour> enemyTargets = new List<EnemyBehaviour>();
    List<EnemyBehaviour> enemyToClean = new List<EnemyBehaviour>();
    EnemyBehaviour activeTarget = null;
    float activeTargetLength = int.MaxValue;

    public GameObject bulletPrefab;
    public Transform bulletEmitter;
    public CharacterBehaviour characterBehaviour;

    public PlayableDirector attackDirector;



    private void OnEnable()
    {
        attackDirector.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!ScoreCounter.gameStart)
        {
            attackDirector.Stop();
            return;
        }

        if (!characterBehaviour.alive)
        {
            enemyTargets.Clear();

            if(attackDirector.state == PlayState.Playing)
                attackDirector.Stop();
            return;
        }

        for(int i = 0; i < enemyTargets.Count; i++)
        {
            if (!enemyTargets[i].alive)
            {
                enemyToClean.Add(enemyTargets[i]);
                continue;
            }

            // 选则一个最近的物体作为目标
            float distance = Vector3.Distance(transform.position, enemyTargets[i].transform.position);

            //if(distance > 5f)
            //{
            //    enemyToClean.Add(enemyTargets[i]);
            //    continue;
            //}

            if (distance < activeTargetLength)
            {
                activeTarget = enemyTargets[i];
                activeTargetLength = distance;
            }
        }

        foreach (EnemyBehaviour target in enemyToClean)
        {
            enemyTargets.Remove(target);
        }
        enemyToClean.Clear();

        if(activeTarget)
        {
            characterBehaviour.transform.LookAt(activeTarget.transform.position);

            if(activeTarget.alive)
            {
                isAttacking = true;
            }
            else
            {
                isAttacking = false;
            }
        }
        else
        {
            isAttacking = false;
        }

        if (!ScoreCounter.gameStart) return;

        if (isAttacking)
        {
            if (attackDirector.state == PlayState.Paused)
            {
                attackDirector.Play();
            }
        }
        else
        {
            if (attackDirector.state == PlayState.Playing)
            {
                attackDirector.Stop();
            }
        }
    }

    public void OnAttack()
    {
        GameObject attack = Instantiate(bulletPrefab);
        attack.SetActive(true);
        attack.transform.position = bulletEmitter.position;
        
        attack.transform.localScale = transform.parent.localScale;

        if(activeTarget == null)
        {
            attack.transform.forward = bulletEmitter.transform.forward;
        }
        else
        {
            attack.transform.forward = (activeTarget.transform.position + new Vector3(0.0f, 0.5f, 0.0f) - bulletEmitter.transform.position).normalized;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.CompareTag("Enemy"))
        {
            EnemyBehaviour behaviour = target.GetComponent<EnemyBehaviour>();
            if (!enemyTargets.Contains(behaviour) && behaviour.alive)
            {
                enemyTargets.Add(behaviour);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.CompareTag("Enemy"))
        {
            EnemyBehaviour behaviour = target.GetComponent<EnemyBehaviour>();
            if (!enemyTargets.Contains(behaviour) && behaviour.alive)
            {
                enemyTargets.Add(behaviour);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject target = other.gameObject;
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyBehaviour behaviour = target.GetComponent<EnemyBehaviour>();
            if (enemyTargets.Contains(behaviour))
            {
                enemyTargets.Remove(behaviour);
            }
        }
    }
}
