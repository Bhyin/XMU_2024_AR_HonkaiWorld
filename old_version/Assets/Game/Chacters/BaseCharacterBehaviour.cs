using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterBehaviour : MonoBehaviour
{
    // ��ɫ�̶��󣬻������ĵ��˷����ӵ�����˺�
    public int row, col; // ��ɫ��λ��
    public BaseEnemyBehaviour target; // �����ɫ�����Ŀ��,��GameLogic����õ�

    public GameObject bulletPrefab; // �ӵ�Ԥ����
    public GameObject shootingStartPoint;

    GameLogic gamaLogic;

    public float skillCooldownThreshold = 1f; // ������ȴ��ֵ
    public float skillStartTime = 0f; // ����ʱ��


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
                // �����ӵ�
                ShootStart();
                skillStartTime = Time.time; // ���÷���ʱ��
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
