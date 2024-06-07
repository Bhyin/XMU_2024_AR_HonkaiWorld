using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : BaseUIPanel
{

    public Button continueButton;
    public Button restartButton;
    public Button exitButton;


    private void OnEnable()
    {
        continueButton.onClick.AddListener(OnContinueButtonClick);
        restartButton.onClick.AddListener(OnRestartButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        continueButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnContinueButtonClick()
    {
        gameLogic.GameContinue();
    }

    public void OnRestartButtonClick()
    {
        gameLogic.GameRestart();
    }

    public void OnExitButtonClick()
    {
        gameLogic.GameOver();
        gameLogic.GameQuit();
    }

    public override void EnableInteraction()
    {
        continueButton.interactable = true;
        restartButton.interactable = true;
        exitButton.interactable = true;
    }

    public override void DisableInteraction()
    {
        continueButton.interactable= false;
        restartButton.interactable = false;
        exitButton.interactable = false;
    }
}
