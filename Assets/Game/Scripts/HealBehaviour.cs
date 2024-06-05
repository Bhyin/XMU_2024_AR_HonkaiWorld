using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class HealBehaviour : MonoBehaviour
{
    public PlayableDirector healDirector;
    public CharacterBehaviour characterBehaviour;
    public EnemyBehaviour enemyBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        healDirector.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ScoreCounter.gameStart)
        {
            healDirector.Stop();
        }

        if(characterBehaviour != null && !characterBehaviour.alive)
        {
            healDirector.Stop();
        }

        if (enemyBehaviour != null && !enemyBehaviour.alive)
        {
            healDirector.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.CompareTag("Character") && target.transform.parent != transform.parent)
        {
            if(healDirector.state == PlayState.Paused)
            {
                healDirector.Play();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.CompareTag("Character") && target.transform.parent != transform.parent)
        {
            if (healDirector.state == PlayState.Paused)
            {
                healDirector.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject target = other.gameObject;
        if (other.gameObject.CompareTag("Character") && target.transform.parent != transform.parent)
        {
            if (healDirector.state == PlayState.Playing)
            {
                healDirector.Stop();
            }
        }
    }
}
