using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

public partial class GameLogic : MonoBehaviour
{
    private void OnEnable()
    {
        anchorBehaviour.SetActive(false);
        mapBehaviour.SetActive(false);
        uiManager.SetActive(true);
        interactionManager.SetActive(true);
    }


    private void OnDisable()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        SetAnchorStart();
    }

    // Update is called once per frame
    void Update()
    {
        if(updateCallbacks.TryGetValue(process, out activeUpdateCallback))
        activeUpdateCallback();
    }

    private void FixedUpdate()
    {
        
    }

    private void LateUpdate()
    {
        
    }


    /**
     * 场景变化逻辑
     * **/
    
    // Set Anchor 切换到 Set Map
    public void SetAnchorToSetMap()
    {
        SetAnchorFinish();
        process = GameProcess.SetMap;
        SetMapStart();
    }

    // Set Map 切换到 Set Anchor
    public void SetMapToSetAnchor()
    {
        mapBehaviour.SetActive(false);
        SetMapFinish();
        process = GameProcess.SetAnchor;
        SetAnchorStart();
    }

    // Set Map 切换到 Game
    public void SetMapToGame()
    {
        SetMapFinish();
        process = GameProcess.Gaming;
        GameStart();
    }

    // 暂停游戏
    public void GamePause()
    {
        uiManager.OpenPanel(PanelItems.game_PausePanel);
        process = GameProcess.Pausing;
    }

    // 继续游戏
    public void GameContinue()
    {
        uiManager.ClosePanel();
        process = GameProcess.Gaming;
    }

    // 进行游戏设置
    public void GameSetting()
    {
        //Debug.Log("游戏设置");
        //// 打开游戏设置面板
        //// uiManager.OpenPanel(PanelItems.game_SettingPanel);
        //process = GameProcess.Pausing;

        // 使用GameContinue返回游戏
    }


    // 游戏结束
    public void GameOver()
    {
        uiManager.ClosePanel();
        process = GameProcess.Pausing;
        GameFinish();
    }

    // 重新开始
    public void GameRestart()
    {
        mapBehaviour.ResetMap();
        uiManager.ClosePanel();
        process = GameProcess.Pausing;
        GameFinish();
        process = GameProcess.SetAnchor;
        epochStartTime = 0f;
        SetAnchorStart();
    }

    // 退出游戏
    public void GameQuit()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}
