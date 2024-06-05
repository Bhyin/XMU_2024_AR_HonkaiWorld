using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("角色血量")]
    public float maxHp = 100;
    float currentHp = 100;
    public GameObject hpBar;
    public Shader hpShader;
    Material hpMat;

    [Header("重生时间")]
    public float recovertime = 10f;
    float remainRecoverTime = 0f;

    public bool alive = true;
    SkinnedMeshRenderer meshRenderer;

    ScoreCounter scoreCounter;

    private void Awake()
    {
        currentHp = maxHp;
        hpMat = new Material(hpShader);
        hpBar.GetComponent<MeshRenderer>().sharedMaterial = hpMat;

        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        scoreCounter = GameObject.FindGameObjectWithTag("ScoreCounter").GetComponent<ScoreCounter>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnAttacked(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            alive = false;
            scoreCounter.AddValkyriesScore(1);
        }
           

        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ScoreCounter.gameStart)
        {

            return;
        }

        if (!alive)
        {
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

        hpBar.transform.rotation = Quaternion.LookRotation(transform.up, Camera.main.transform.position - hpBar.transform.position);
        hpMat.SetFloat("_Hp", currentHp / maxHp);
    }




    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        if(target.CompareTag("Attack"))
        {
            AttackBehaviour behaviour = target.GetComponent<AttackBehaviour>();
            if (behaviour.sourceType == AttackBehaviour.SourceType.Character) OnAttacked(behaviour.damage);
        }
    }
}
