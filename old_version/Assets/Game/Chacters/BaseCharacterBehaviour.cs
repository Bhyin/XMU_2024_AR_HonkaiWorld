using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterBehaviour : MonoBehaviour
{
    // 角色固定后，会对最近的敌人发射子弹造成伤害
    public int row, col; // 角色的位置
    public BaseEnemyBehaviour target; // 距离角色最近的目标,由GameLogic计算得到

    public GameObject bulletPrefab; // 子弹预制体
    public GameObject shootingStartPoint;

    GameLogic gamaLogic;

    public float skillCooldownThreshold = 1f; // 发射冷却阈值
    public float skillStartTime = 0f; // 发射时间


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckTarget();
        if (target != null)
        {
            Debug.Log("target: " + target.row + ", " + target.col);
            if (
                Mathf.Abs(target.row - row) < 4
                && Mathf.Abs(target.col - col) < 4 &&
                Time.time - skillStartTime > skillCooldownThreshold
                )
            {
                // 发射子弹
                ShootStart();
                skillStartTime = Time.time; // 重置发射时间
            }
        }
    }

    void CheckTarget()
    {
        float distance = 1e10f;

        foreach (var enemy in gamaLogic.enemies)
        {
            float d = Vector3.Distance(transform.position, enemy.transform.position);
            if (d < distance)
            {
                target = enemy;
                distance = d;
            }
        }
    }

    void ShootStart()
    {
        GameObject bullet = Instantiate(bulletPrefab, gamaLogic.mapBehaviour.transform);
        bullet.SetActive(true);
        bullet.GetComponent<BaseBulletBehaviour>().target = target;
        bullet.transform.position = shootingStartPoint.transform.position;
        Vector3 direction = new Vector3(target.transform.position.x, 0, target.transform.position.z);
        direction = direction - new Vector3(bullet.transform.position.x, 0, bullet.transform.position.z);
        bullet.transform.forward = direction.normalized;
    }


    public void SetGameLogic(GameLogic gl)
    {
        gamaLogic = gl;
    }
}
