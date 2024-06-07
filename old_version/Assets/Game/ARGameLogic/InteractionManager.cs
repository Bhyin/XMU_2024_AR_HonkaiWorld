using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
#if UNITY_EDITOR

#else
using ITouch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using ITouchPhase = UnityEngine.InputSystem.TouchPhase;
#endif

public class InteractionManager : MonoBehaviour
{
    [Serializable]
    public class InteractionEvent : UnityEvent
    {
        public static InteractionEvent operator +(InteractionEvent ent, UnityAction callback)
        {
            ent.AddListener(callback);
            return ent;
        }

        public static InteractionEvent operator -(InteractionEvent ent, UnityAction callback)
        {
            ent.RemoveListener(callback);
            return ent;
        }
    }


    public enum State
    {
        Waiting, // �ȴ�����
        Press, // ��ס
        LongPress // ����
    }

    public class Context
    {
        public State state = State.Waiting;
        public float pressStartTime = 0f;
        public Vector2 pressStartPosition = Vector2.zero;
        public Vector2 pressCurrPosition = Vector2.zero;
        public Vector2 delta = Vector2.zero;

        public readonly static float longPressTimeThreshold = 0.8f;
        public readonly static float longPressDeltaThreshold = 10f; // 10������
    }

    public InteractionEvent pressEvent;
    public InteractionEvent longPressEvent;
    public InteractionEvent clickEvent;
    public InteractionEvent releaseEvent;

    private Context context;

    /// <summary>
    /// û�д���
    /// </summary>
    public bool waiting => context.state == State.Waiting;

    /// <summary>
    /// ����
    /// </summary>
    public bool touching => press || longPress;

    public bool press => context.state == State.Press;

    public bool longPress=> context.state == State.LongPress;


    public Vector2 position => context.pressCurrPosition;

    public Vector2 pressPosition => context.pressStartPosition;

    public Vector2 startPosition => context.pressStartPosition;

    public Vector2 delta => context.delta;

    // ����󴥷������¼���״̬�ӵȴ���Ϊ��ס
    // ��ס��ʼ��һ��ʱ�䣬������λ��û��̫��仯�������ʱ������󣬴��������¼���Ȼ���Ϊ����
    // ��ס��ʼ��һ��ʱ�䣬һ��������λ�÷����仯��״̬������Ϊ�����������������¼�
    // ��ס����һ���¼����ɿ��������ɿ��¼���״̬��Ϊû��ס
    // �ɿ�ʱ�������סʱ��С��ĳ����ֵ����������¼������ʱ�䳬����ֵ����Ҳ�ᴥ�������¼���״̬��Ϊû��ס��

#if UNITY_EDITOR
    Mouse mouse;
#else
    ITouch touch;
#endif

    private void Awake()
    {
        context = new Context();

        if (pressEvent == null) pressEvent = new InteractionEvent();
        if (longPressEvent == null) longPressEvent = new InteractionEvent();
        if (clickEvent == null) clickEvent = new InteractionEvent();
        if (releaseEvent == null) releaseEvent = new InteractionEvent();
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
        pressEvent.RemoveAllListeners();
        longPressEvent.RemoveAllListeners();
        clickEvent.RemoveAllListeners();
        releaseEvent.RemoveAllListeners();
    }

    private bool SetInteractor()
    {
#if UNITY_EDITOR
        mouse = Mouse.current;
        return mouse != null;
#else
        if (ITouch.activeTouches.Count == 0)
        {
            return false;
        }
        touch = ITouch.activeTouches[0];
        return true;
#endif
    }

    private Vector2 screenPosition
    {
        get
        {
#if UNITY_EDITOR
            return mouse.position.ReadValue();
#else
            return touch.screenPosition;
#endif
        }
    }

    private Vector2 screenDelta
    {
        get
        {
#if UNITY_EDITOR
            return mouse.delta.ReadValue();
#else
            return touch.delta;
#endif
        }
    }

    private bool pressed
    {
        get
        {
#if UNITY_EDITOR
            return mouse.leftButton.wasPressedThisFrame;
#else
            return touch.phase == ITouchPhase.Began;
#endif
        }
    }

    private bool moved
    {
        get
        {
#if UNITY_EDITOR
            return mouse.leftButton.isPressed;
#else
            return touch.phase == ITouchPhase.Moved || touch.phase == ITouchPhase.Stationary;
#endif
        }
    }

    private bool released
    {
        get
        {
#if UNITY_EDITOR
            return mouse.leftButton.wasReleasedThisFrame;
#else
            return touch.phase == ITouchPhase.Ended;
#endif
        }
    }

    private void Update()
    {
        if (!SetInteractor()) return;

        if (pressed)
        {
            // ����
            context.state = State.Press; // ״̬�ӵȴ���Ϊ����
            context.pressStartTime = Time.time; // ��¼��ʼ���µ�ʱ��
            context.pressStartPosition = screenPosition; // ��¼��ʼ���µ�λ��
            context.pressCurrPosition = context.pressStartPosition;
            pressEvent.Invoke(); // �������¼�
        }
        else if (moved)
        {
            context.pressCurrPosition = screenPosition;
            context.delta = screenDelta;

            if (context.state == State.Press)
            {
                if (Time.time - context.pressStartTime > Context.longPressTimeThreshold)
                {
                    // ��ס״̬�£���סʱ��ﵽ������ֵ���������������¼������볤��״̬
                    context.state = State.LongPress;
                    longPressEvent.Invoke();
                }
                else if (Vector2.Distance(context.pressStartPosition, context.pressCurrPosition) > Context.longPressDeltaThreshold)
                {
                    // ��ס״̬�£�һ������λ�ñ仯�����������볤��״̬
                    context.state = State.LongPress;
                }
            }
        }
        else if (released)
        {
            if (context.state == State.Press)
            {
                // ���볤��״̬֮ǰ�ɿ����򴥷�����¼�
                clickEvent.Invoke();
            }
            else
            {
                // ���볤��״̬֮���ɿ��������ɿ��¼�
                releaseEvent.Invoke();
            }
            context.state = State.Waiting;
        }
        else
        {
            context.state = State.Waiting;
        }
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
