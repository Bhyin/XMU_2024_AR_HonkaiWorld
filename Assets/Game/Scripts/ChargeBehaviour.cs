using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ChargeBehaviour : MonoBehaviour
{

    public PlayableDirector chargeDirector;
    public CharacterBehaviour characterBehaviour;
    public EnemyBehaviour enemyBehaviour;


    // Start is called before the first frame update
    void Start()
    {
        chargeDirector.Stop();   
    }

    private void Update()
    {
        if (!ScoreCounter.gameStart)
        {
            chargeDirector.Stop();
        }

        if (characterBehaviour != null && !characterBehaviour.alive)
        {
            chargeDirector.Stop();
        }

        if (enemyBehaviour != null && !enemyBehaviour.alive)
        {
            chargeDirector.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        if ((target.CompareTag("Character") || target.CompareTag("Enemy")) && target.transform.parent != transform.parent)
        {
            if (chargeDirector.state == PlayState.Paused)
            {
                chargeDirector.Play();
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        GameObject target = other.gameObject;
        if ((target.CompareTag("Character") || target.CompareTag("Enemy")) && target.transform.parent != transform.parent)
        {
            if (chargeDirector.state == PlayState.Paused)
            {
                chargeDirector.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject target = other.gameObject;
        if ((target.CompareTag("Character") || target.CompareTag("Enemy")) && target.transform.parent != transform.parent)
        {
            if (chargeDirector.state == PlayState.Playing)
            {
                chargeDirector.Stop();
            }
        }
    }
}
