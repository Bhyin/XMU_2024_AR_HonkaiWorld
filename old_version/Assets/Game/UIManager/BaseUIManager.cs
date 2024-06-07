using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUIManager : MonoBehaviour
{
    // 面板栈
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
        // 栈顶面板禁用交互
        DisablePeekInteraction();

        // 获取面板预制体
        GameObject panelPrefab = PanelManager.GetPanel(name);

        // 实例化
        GameObject panelInstance = Instantiate(panelPrefab, transform);
        panelInstance.name = name;
        BaseUIPanel panel = panelInstance.GetComponent<BaseUIPanel>();
        panel.gameLogic = gameLogic;
        panel.SetActive(true);
        panel.EnableInteraction();

        // 入栈
        panelStack.Push(panel);
    }

    public void ClosePanel()
    {
        if (panelStack.Count > 0)
        {
            // 出栈
            BaseUIPanel panel = panelStack.Pop();
            panel.DisableInteraction();
            panel.SetActive(false);

            // 销毁
            Destroy(panel.gameObject);

            // 恢复栈顶元素交互
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
