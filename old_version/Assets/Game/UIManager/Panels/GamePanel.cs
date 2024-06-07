using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BaseUIPanel
{
    [Header("Ability")]
    public Button skillButton;
    public Button ultimateButton;

    [Header("Character")]
    public Button charAButton;
    public Button charBButton;
    public Button charCButton;

    [Header("Game State")]
    public Button pauseButton;
    public Button settingButton;

    [Header("Information")]
    public TextMeshProUGUI time;
    public TextMeshProUGUI target;

    private void OnEnable()
    {
        skillButton.onClick.AddListener(OnSkillButtonClick);
        ultimateButton.onClick.AddListener(OnUltimateButtonClick);

        charAButton.onClick.AddListener(OnCharAButtonClick);
        charBButton.onClick.AddListener(OnCharBButtonClick);
        charCButton.onClick.AddListener(OnCharCButtonClick);

        pauseButton.onClick.AddListener(OnPauseButtonClick);
        settingButton.onClick.AddListener(OnSettingButtonClick);
    }

    private void OnDisable()
    {
        skillButton.onClick.RemoveAllListeners();
        ultimateButton.onClick.RemoveAllListeners();

        charAButton.onClick.RemoveAllListeners();
        charBButton.onClick.RemoveAllListeners();
        charCButton.onClick.RemoveAllListeners();

        pauseButton.onClick.RemoveAllListeners();
        settingButton.onClick.RemoveAllListeners();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time.text = gameLogic.GetStartTime();
        target.text = "Enemy:\n" + gameLogic.GetDestroyedEnemyNum();
    }

    public void OnSkillButtonClick()
    {
        // 释放当前角色战技
        gameLogic.Skill();
        
    }

    public void OnUltimateButtonClick()
    {
        // 释放当前角色终结技
        gameLogic.Ultimate();
    }

    public void OnCharAButtonClick()
    {
        // 设置角色A为活跃角色
        gameLogic.SwitchCharacter(GameLogic.CharacterEnum.A);
    }

    public void OnCharBButtonClick()
    {
        // 设置角色B为活跃角色
        gameLogic.SwitchCharacter(GameLogic.CharacterEnum.B);
    }

    public void OnCharCButtonClick()
    {
        // 设置角色C为活跃角色
        gameLogic.SwitchCharacter(GameLogic.CharacterEnum.C);
    }

    public void OnPauseButtonClick()
    {
        gameLogic.GamePause();        
    }

    public void OnSettingButtonClick()
    {
        // 打开设置面板，可以查看游戏状态，调整游戏参数等。
        gameLogic.GameSetting();
    }
    public override void EnableInteraction()
    {
        skillButton.interactable = true;
        ultimateButton.interactable = true;

        charAButton.interactable = true;
        charBButton.interactable = true;
        charCButton.interactable = true;

        pauseButton.interactable = true;
        settingButton.interactable = true;
    }

    public override void DisableInteraction()
    {
        skillButton.interactable = false;
        ultimateButton.interactable = false;

        charAButton.interactable = false;
        charBButton.interactable = false;
        charCButton.interactable = false;

        pauseButton.interactable = false;
        settingButton.interactable = false;
    }
}
