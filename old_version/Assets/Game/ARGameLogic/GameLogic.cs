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
     * �����仯�߼�
     * **/
    
    // Set Anchor �л��� Set Map
    public void SetAnchorToSetMap()
    {
        SetAnchorFinish();
        process = GameProcess.SetMap;
        SetMapStart();
    }

    // Set Map �л��� Set Anchor
    public void SetMapToSetAnchor()
    {
        mapBehaviour.SetActive(false);
        SetMapFinish();
        process = GameProcess.SetAnchor;
        SetAnchorStart();
    }

    // Set Map �л��� Game
    public void SetMapToGame()
    {
        SetMapFinish();
        process = GameProcess.Gaming;
        GameStart();
    }

    // ��ͣ��Ϸ
    public void GamePause()
    {
        uiManager.OpenPanel(PanelItems.game_PausePanel);
        process = GameProcess.Pausing;
    }

    // ������Ϸ
    public void GameContinue()
    {
        uiManager.ClosePanel();
        process = GameProcess.Gaming;
    }

    // ������Ϸ����
    public void GameSetting()
    {
        //Debug.Log("��Ϸ����");
        //// ����Ϸ�������
        //// uiManager.OpenPanel(PanelItems.game_SettingPanel);
        //process = GameProcess.Pausing;

        // ʹ��GameContinue������Ϸ
    }


    // ��Ϸ����
    public void GameOver()
    {
        uiManager.ClosePanel();
        process = GameProcess.Pausing;
        GameFinish();
    }

    // ���¿�ʼ
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

    // �˳���Ϸ
    public void GameQuit()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}
