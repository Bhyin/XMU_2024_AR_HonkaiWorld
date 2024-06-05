using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Clean : MonoBehaviour
{

    GameObject[] invalidObjects = null;
    PlayableDirector director = null;

    // Update is called once per frame
    void Update()
    {
        invalidObjects = GameObject.FindGameObjectsWithTag("InvBullet");
        for(int i = 0; i < invalidObjects.Length; i++)
        {
            director = invalidObjects[i].GetComponent<PlayableDirector>();
            if(director != null && director.state != PlayState.Playing)
            {
                Destroy(director.gameObject);
            }
        }
    }
}
