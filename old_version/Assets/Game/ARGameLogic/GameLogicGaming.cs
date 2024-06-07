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
        // ÿ��5������һ�����ˣ�ÿһ����������5��
        // ÿ����һ�����ˣ��ȴ�0.8�����������һ��
        // ����һ�����˺��ٹ�8��������һ������
        // �������˺�ʹ��Invoke���ȴ��������gameObject����ΪActive
        float time = Time.time;
        if (time - epochStartTime > epochTimeThreshold)
        {
            generateEnemiesCoroutine = StartCoroutine(CreateEnemies());
            epochStartTime = time;
        }
    }


    public void Skill()
    {
        Debug.Log("��ɫ�ͷ�ս��");
    }

    public void Ultimate()
    {
        Debug.Log("��ɫ�ͷ��սἼ");
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
        // ��ֹ������Ϸʱ�������ɵ��ˡ�
        if(generateEnemiesCoroutine != null)
        {
            StopCoroutine(generateEnemiesCoroutine);
        }

        // ������е���
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


    // ��Startʱ���ø÷���
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
            yield return new WaitForSeconds(0.8f); // ÿһ���У�ÿ����һ�����ˣ��ȴ�һ��
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
     * Gaming �׶��¼���Ӧ����
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
