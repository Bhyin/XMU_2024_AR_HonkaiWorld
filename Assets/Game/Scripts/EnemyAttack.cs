using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnemyAttack : MonoBehaviour
{
    bool isAttacking = false;

    List<CharacterBehaviour> characterTargets = new List<CharacterBehaviour>();
    List<CharacterBehaviour> characterToClean = new List<CharacterBehaviour>();
    CharacterBehaviour activeTarget = null;
    float activeTargetLength = int.MaxValue;

    public GameObject bulletPrefab;
    public Transform bulletEmitter;
    public EnemyBehaviour enemyBehaviour;

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

        if (!enemyBehaviour.alive)
        {
            characterTargets.Clear();
            attackDirector.Pause();
            return;
        }

        for (int i = 0; i < characterTargets.Count; i++)
        {
            if (!characterTargets[i].alive)
            {
                characterToClean.Add(characterTargets[i]);
                continue;
            }

            // 选则一个最近的物体作为目标
            float distance = Vector3.Distance(transform.position, characterTargets[i].transform.position);

            if (distance < activeTargetLength)
            {
                activeTarget = characterTargets[i];
                activeTargetLength = distance;
            }
        }

        foreach (CharacterBehaviour target in characterToClean)
        {
            characterTargets.Remove(target);
        }
        characterToClean.Clear();

        

        if (activeTarget)
        {
            enemyBehaviour.transform.LookAt(activeTarget.transform.position, transform.up);

            if (activeTarget.alive)
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
    }

    private void LateUpdate()
    {
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

        if (activeTarget == null)
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
        if (target.CompareTag("Character"))
        {
            CharacterBehaviour behaviour = target.GetComponent<CharacterBehaviour>();
            if (!characterTargets.Contains(behaviour) && behaviour.alive)
            {
                characterTargets.Add(behaviour);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.CompareTag("Character"))
        {
            CharacterBehaviour behaviour = target.GetComponent<CharacterBehaviour>();
            if (!characterTargets.Contains(behaviour) && behaviour.alive)
            {
                characterTargets.Add(behaviour);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject target = other.gameObject;
        if (other.gameObject.CompareTag("Character"))
        {
            CharacterBehaviour behaviour = target.GetComponent<CharacterBehaviour>();
            if (characterTargets.Contains(behaviour))
            {
                characterTargets.Remove(behaviour);
            }
        }
    }

}
