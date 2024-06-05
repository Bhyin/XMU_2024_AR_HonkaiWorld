using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using static AttackBehaviour;

public class CharacterBehaviour : MonoBehaviour
{

    [Header("角色血量")]
    public float maxHp = 100;
    public float healUnit = 15f;
    public float healDuration = 1f;
    float currentHp = 100;
    public GameObject hpBar;
    public Shader hpShader;
    Material hpMat;
    Timer healTimer;

    public bool alive = true;
    SkinnedMeshRenderer meshRenderer;


    [Header("重生时间")]
    public float recovertime = 10f;
    float remainRecoverTime = 0f;

    bool isHealing = false;
    bool isCharging = false;

    public ParticleSystem healDirector;
    public ParticleSystem chargeDirector;

    ScoreCounter scoreCounter;

    public enum State
    {
        Battel,
        Idle,
        dead
    }

    private void OnEnable()
    {
        healDirector.Stop();
        chargeDirector.Stop();
    }

    private void Awake()
    {
        currentHp = maxHp;
        hpMat = new Material(hpShader);
        hpBar.GetComponent<MeshRenderer>().sharedMaterial = hpMat;
        healTimer = new Timer(healDuration, OnHeal);

        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        scoreCounter = GameObject.FindGameObjectWithTag("ScoreCounter").GetComponent<ScoreCounter>();
    }


    private void Update()
    {
        if (!ScoreCounter.gameStart)
        {
            healDirector.Stop();
            chargeDirector.Stop();
            return;
        };

        if (!alive)
        {
            isCharging = false;
            isHealing = false;
            meshRenderer.enabled = false;
            hpBar.SetActive(false);

            remainRecoverTime += Time.deltaTime;

            if(remainRecoverTime > recovertime)
            {
                alive = true;
                currentHp = maxHp;
                hpBar.SetActive(true);
                meshRenderer.enabled = true;
                remainRecoverTime = 0f;
            }
            return;
        }

        if (isHealing)
        {
            if (!healDirector.isPlaying)
            {
                healTimer.Start();
                healDirector.Play();
            }
            healTimer.OnUpdate(Time.deltaTime);
        }
        else
        {
            if (healDirector.isPlaying)
            {
                healDirector.Stop();
            }
        }

        if (isCharging)
        {
            if (!chargeDirector.isPlaying)
            {
                chargeDirector.Play();
            }
        }
        else
        {
            if (chargeDirector.isPlaying)
            {
                chargeDirector.Stop();
            }
        }


        hpBar.transform.rotation = Quaternion.LookRotation(transform.up, Camera.main.transform.position - hpBar.transform.position);
        hpMat.SetFloat("_Hp", currentHp / maxHp);
    }


    //复活用函数
    public void OnHeal()
    {
        currentHp += healUnit;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
    }


    public void OnAttacked(float damage)
    {
        currentHp -= damage;

        if (currentHp <= 0)
        {
            alive = false;
            scoreCounter.AddMonstersScore(1);
        }

        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
    }

    //检测器
    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.transform.parent == transform.parent) return;
        if (target.CompareTag("Heal"))
        {
            isHealing = true;
        }
        else if (target.CompareTag("Charge"))
        {
            isCharging = true;
        }
        else if (target.CompareTag("Attack"))
        {
            AttackBehaviour behaviour = target.GetComponent<AttackBehaviour>();
            if (behaviour.sourceType == SourceType.Enemy) OnAttacked(behaviour.damage);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        GameObject target = other.gameObject;
        if (target.transform.parent == transform.parent) return;
        if (target.CompareTag("Heal"))
        {
            isHealing = true;
        }
        else if (target.CompareTag("Charge"))
        {
            isCharging = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.transform.parent == transform.parent) return;
        if (target.CompareTag("Heal"))
        {
            isHealing = false;
        }
        else if (target.CompareTag("Charge"))
        {
            isCharging = false;
        }
    }

    public void OnDead()
    {

    }
}
