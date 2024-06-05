using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ��ʱ��
/// 
/// ����ʱ����duration�Լ��ص�����
/// Start����MonoBehaviour��Update�е���OnUpdate����ִ�ж�ʱ����
/// </summary>
public class Timer
{
    private float start;
    private float current;

    private float duration;
    private List<UnityAction> actions;



    public Timer(float duration, List<UnityAction> actions)
    {
        this.duration = duration;
        this.actions = actions;
    }

    public Timer(float duration, UnityAction action)
    {
        this.duration = duration;
        this.actions = new List<UnityAction> { action };
    }
    public void OnUpdate(float delta)
    {
        current += delta;
        if (current - start > duration)
        {
            foreach (UnityAction action in actions) { action.Invoke(); }
            start = current;
        }
    }

    public void Start()
    {
        start = Time.time;
        current = start;
    }

}
