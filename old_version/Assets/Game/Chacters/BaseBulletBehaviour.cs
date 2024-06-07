using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBulletBehaviour : MonoBehaviour
{
    public float speed = 0.1f;
    public BaseEnemyBehaviour target;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            if(Mathf.Abs( transform.position.x - target.transform.position.x) < 0.05f && Mathf.Abs(transform.position.z - target.transform.position.z) < 0.05f)
            {
                Shoot();
            }
        }

        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        if (transform.position.x > 50f || transform.position.y > 50f || transform.position.z > 50f)
        {
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        target.Hit();
        Destroy(gameObject);
    }
}
