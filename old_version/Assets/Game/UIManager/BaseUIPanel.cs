using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUIPanel : MonoBehaviour
{
    public GameLogic gameLogic;

    public virtual void Awake()
    {
        
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
    }

    private void OnDestroy()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {

    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public virtual void EnableInteraction()
    {

    }

    public virtual void DisableInteraction()
    {

    }
}
