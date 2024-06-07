using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Enter " + collision.gameObject.name);
    }

    private void OnTriggerStay(Collider collision)
    {
        Debug.Log("Stay " + collision.gameObject.name);
    }

    private void OnTriggerExit(Collider collision)
    {
        Debug.Log("Exit" + collision.gameObject.name);
    }
}
