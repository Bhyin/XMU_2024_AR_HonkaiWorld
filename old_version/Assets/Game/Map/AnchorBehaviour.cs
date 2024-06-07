using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorBehaviour : MonoBehaviour
{
    public bool selected;

    private void Awake()
    {
        selected = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void Select()
    {
        Debug.Log("Select Anchor");
        selected = true;
    }

    public void UnSelect()
    {
        Debug.Log("Unselect Anchor");
        selected = false;
    }
}
