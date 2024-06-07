using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPanel : BaseUIPanel
{
    public Button playButton;
    public Button exitButton;

    public override void Awake()
    {
        
    }

    private void OnEnable()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
    }

    private void Update()
    {

    }

    public void OnPlayButtonClicked()
    {
        
    }

    public void OnExitButtonClicked()
    {
        
    }

    public override void EnableInteraction()
    {
        playButton.interactable = true;
        exitButton.interactable = true;
    }

    public override void DisableInteraction()
    {
        playButton.interactable = false;
        exitButton.interactable = false;
    }
}
