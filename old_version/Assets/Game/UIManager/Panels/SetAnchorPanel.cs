using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetAnchorPanel : BaseUIPanel
{
    public TextMeshProUGUI tip;
    public Button confirmButton;
    public Button backButton;

    void OnEnable()
    {
        confirmButton.onClick.AddListener(OnConfirmButtonClicked);
        backButton.onClick.AddListener(OnBackButtonClicked);
    }

    void OnDisable()
    {
        confirmButton.onClick.RemoveAllListeners();
        backButton.onClick.RemoveAllListeners();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnConfirmButtonClicked()
    {
        // 进入设置地图的流程
        gameLogic.SetAnchorToSetMap();
    }

    public void OnBackButtonClicked()
    {
        
    }

    public override void EnableInteraction()
    {
        confirmButton.interactable = true;
        backButton.interactable = true;
    }

    public override void DisableInteraction()
    {
        confirmButton.interactable = false;
        backButton.interactable = false;
    }
}
