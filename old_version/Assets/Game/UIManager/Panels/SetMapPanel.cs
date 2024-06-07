using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetMapPanel : BaseUIPanel
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
        gameLogic.SetMapToGame();
    }

    public void OnBackButtonClicked()
    {
        gameLogic.SetMapToSetAnchor();
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
