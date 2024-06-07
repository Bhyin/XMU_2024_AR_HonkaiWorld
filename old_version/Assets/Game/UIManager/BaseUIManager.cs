using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUIManager : MonoBehaviour
{
    // ���ջ
    Stack<BaseUIPanel> panelStack;
    GameLogic gameLogic;

    void Awake()
    {
        Initialize();
    }

    void OnDestroy()
    {
        PanelManager.ReleasePanels();
        panelStack?.Clear();
        panelStack = null;
    }

    public void OpenPanel(string name)
    {
        // ջ�������ý���
        DisablePeekInteraction();

        // ��ȡ���Ԥ����
        GameObject panelPrefab = PanelManager.GetPanel(name);

        // ʵ����
        GameObject panelInstance = Instantiate(panelPrefab, transform);
        panelInstance.name = name;
        BaseUIPanel panel = panelInstance.GetComponent<BaseUIPanel>();
        panel.gameLogic = gameLogic;
        panel.SetActive(true);
        panel.EnableInteraction();

        // ��ջ
        panelStack.Push(panel);
    }

    public void ClosePanel()
    {
        if (panelStack.Count > 0)
        {
            // ��ջ
            BaseUIPanel panel = panelStack.Pop();
            panel.DisableInteraction();
            panel.SetActive(false);

            // ����
            Destroy(panel.gameObject);

            // �ָ�ջ��Ԫ�ؽ���
            EnablePeekInteraction();
        }
    }

    public void EnablePeekInteraction()
    {
        if (panelStack.Count > 0) panelStack.Peek().EnableInteraction();
    }

    public void DisablePeekInteraction()
    {
        if (panelStack.Count > 0) panelStack.Peek().DisableInteraction();
    }

    public void Initialize()
    {
        PanelManager.LoadPanels();
        panelStack = new Stack<BaseUIPanel>();
    }

    public void SetGameLogic(GameLogic gl)
    {
        gameLogic = gl;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
