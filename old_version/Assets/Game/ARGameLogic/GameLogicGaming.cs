using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public partial class GameLogic : MonoBehaviour
{
    float epochTimeThreshold = 8f;
    float epochStartTime = 0f;
    Coroutine generateEnemiesCoroutine = null;

    public int destroyEnemyNumber = 0;

    public float gameStartTime;
    public void GameUpdate()
    {
        // 每隔5秒生成一波敌人，每一波敌人生成5个
        // 每生成一个敌人，等待0.8秒后再生成下一个
        // 生成一波敌人后，再过8秒生成下一波敌人
        // 创建敌人后，使用Invoke，等待若干秒后将gameObject设置为Active
        float time = Time.time;
        if (time - epochStartTime > epochTimeThreshold)
        {
            generateEnemiesCoroutine = StartCoroutine(CreateEnemies());
            epochStartTime = time;
        }
    }


    public void Skill()
    {
        Debug.Log("角色释放战技");
    }

    public void Ultimate()
    {
        Debug.Log("角色释放终结技");
    }

    public void SwitchCharacter(CharacterEnum ce)
    {
        switch (ce)
        {
            case CharacterEnum.A:
                Debug.Log("Make A Active");
                break;
            case CharacterEnum.B:
                PlacingCharStart();
                break;
            case CharacterEnum.C:
                Debug.Log("Make C Active");
                break;
            default:
                break;
        }
    }

    public void GameStart()
    {
        gameStartTime = Time.time;
        mapBehaviour.SetActive(true);
        uiManager.OpenPanel(PanelItems.game_GamePanel);
    }

    public void GameFinish()
    {
        // 防止重启游戏时继续生成敌人。
        if(generateEnemiesCoroutine != null)
        {
            StopCoroutine(generateEnemiesCoroutine);
        }

        // 清除所有敌人
        foreach(var enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }

        gameStartTime = Time.time;
        destroyEnemyNumber = 0;

        enemies.Clear();
        mapBehaviour.SetActive(false);
        uiManager.ClosePanel();
    }


    // 在Start时调用该方法
    public IEnumerator CreateEnemies()
    {
        for (int i = 1; i <= 5; ++i)
        {
            GameObject enemy = Instantiate(emenyPrefab, mapBehaviour.transform);
            BaseEnemyBehaviour beb = enemy.GetComponent<BaseEnemyBehaviour>();
            beb.SetGameLogic(this);
            beb.SetMapBehaviour(mapBehaviour);
            beb.SetActive(true);
            enemies.Add(beb);
            yield return new WaitForSeconds(0.8f); // 每一波中，每生成一个敌人，等待一秒
        }
    }

    public string GetStartTime()
    {
        int seconds = (int)(Time.time - gameStartTime);

        return $"{seconds / 60}:{seconds % 60}";
    }

    public int GetDestroyedEnemyNum()
    {
        return destroyEnemyNumber;
    }


    /**
     * Gaming 阶段事件响应函数
     * **/

    public void GamingPressCallback()
    {
    }

    public void GamingLongPressCallback()
    {

    }

    public void GamingClickCallback()
    {

    }

    public void GamingReleaseCallback()
    {
    }
}
