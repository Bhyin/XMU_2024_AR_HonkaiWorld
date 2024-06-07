using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : BaseUIPanel
{
    public Button restartButton;
    public Button exitButton;
    private void OnEnable()
    {
        restartButton.onClick.AddListener(OnRestartButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
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

    public void OnRestartButtonClick()
    {

    }

    public void OnExitButtonClick()
    { 
    }

    public override void EnableInteraction()
    {
        restartButton.interactable = true;
        exitButton.interactable = true;
    }

    public override void DisableInteraction()
    {
        restartButton.interactable = false;
        exitButton.interactable = false;
    }
}
