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
        Waiting, // 等待输入
        Press, // 按住
        LongPress // 长按
    }

    public class Context
    {
        public State state = State.Waiting;
        public float pressStartTime = 0f;
        public Vector2 pressStartPosition = Vector2.zero;
        public Vector2 pressCurrPosition = Vector2.zero;
        public Vector2 delta = Vector2.zero;

        public readonly static float longPressTimeThreshold = 0.8f;
        public readonly static float longPressDeltaThreshold = 10f; // 10个像素
    }

    public InteractionEvent pressEvent;
    public InteractionEvent longPressEvent;
    public InteractionEvent clickEvent;
    public InteractionEvent releaseEvent;

    private Context context;

    /// <summary>
    /// 没有触摸
    /// </summary>
    public bool waiting => context.state == State.Waiting;

    /// <summary>
    /// 触摸
    /// </summary>
    public bool touching => press || longPress;

    public bool press => context.state == State.Press;

    public bool longPress=> context.state == State.LongPress;


    public Vector2 position => context.pressCurrPosition;

    public Vector2 pressPosition => context.pressStartPosition;

    public Vector2 startPosition => context.pressStartPosition;

    public Vector2 delta => context.delta;

    // 点击后触发按下事件，状态从等待变为按住
    // 按住开始后一段时间，触碰点位置没有太大变化，则这段时间结束后，触发长按事件，然后变为长按
    // 按住开始后一段时间，一旦触碰点位置发生变化，状态立即变为长按，不触发长按事件
    // 按住后在一段事件内松开，触发松开事件，状态变为没按住
    // 松开时，如果按住时间小于某个阈值，触发点击事件，如果时间超出阈值，则也会触发长按事件，状态变为没按住。

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
            // 按下
            context.state = State.Press; // 状态从等待变为按下
            context.pressStartTime = Time.time; // 记录开始按下的时间
            context.pressStartPosition = screenPosition; // 记录开始按下的位置
            context.pressCurrPosition = context.pressStartPosition;
            pressEvent.Invoke(); // 处理按下事件
        }
        else if (moved)
        {
            context.pressCurrPosition = screenPosition;
            context.delta = screenDelta;

            if (context.state == State.Press)
            {
                if (Time.time - context.pressStartTime > Context.longPressTimeThreshold)
                {
                    // 按住状态下，按住时间达到长按阈值，立即触发长按事件并进入长按状态
                    context.state = State.LongPress;
                    longPressEvent.Invoke();
                }
                else if (Vector2.Distance(context.pressStartPosition, context.pressCurrPosition) > Context.longPressDeltaThreshold)
                {
                    // 按住状态下，一旦触摸位置变化，则立即进入长按状态
                    context.state = State.LongPress;
                }
            }
        }
        else if (released)
        {
            if (context.state == State.Press)
            {
                // 进入长按状态之前松开，则触发点击事件
                clickEvent.Invoke();
            }
            else
            {
                // 进入长按状态之后松开，触发松开事件
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
