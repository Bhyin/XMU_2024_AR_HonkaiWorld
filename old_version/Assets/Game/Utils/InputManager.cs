using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

#if UNITY_EDITOR

#else
using ITouch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using ITouchPhase = UnityEngine.InputSystem.TouchPhase;
#endif

namespace Game
{
    [AddComponentMenu("Game/Input Manager")]
    public class InputManager : MonoBehaviour
    {
        /**
         * Methods
         * **/
        private void Awake()
        {
            EnhancedTouchSupport.Enable();
        }

        private void OnDestroy()
        {
            EnhancedTouchSupport.Disable();
        }

        // Update is called once per frame
        private void Update()
        {
            if (!OK) return;

            if (Began) // 按下
            {
                // 更新Context
                m_Context.state = State.PRESS; // 状态为按下
                m_Context.startTime = Time.time; // 记录按下事件
                m_Context.startPos = screenPosition; // 记录按下位置
                m_Context.currPos = m_Context.startPos; // 当前按下位置
                m_PressEvt.Invoke(); // 触发按下事件                                                                        
            }
            else if (Moved)
            {
                m_Context.currPos = screenPosition;
                m_Context.delta = screenDelta;

                if (m_Context.state == State.PRESS)
                {
                    if (Time.time - m_Context.startTime > Context.longPressTimeThreshold)
                    {
                        m_Context.state = State.LONGPRESS;
                        m_LongPressEvt.Invoke();
                    }
                }
            }
            else if (Released)
            {
                m_Context.currPos = screenPosition;
                if (m_Context.state == State.PRESS) m_ClickEvt.Invoke();
                else m_ReleaseEvt.Invoke();
                m_Context.state = State.WAITING;
            }
            else
            {
                m_Context.state = State.WAITING;
            }
        }


        private bool OK
        {
            get
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

        private bool Began
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

        private bool Moved
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

        private bool Released
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

        public bool Waiting => m_Context.state == State.WAITING;
        public bool Pressing => m_Context.state == State.PRESS;
        public bool LongPressing => m_Context.state == State.LONGPRESS;
        public bool Touching => Pressing || LongPressing;

        public Vector2 Position => m_Context.currPos;
        public Vector2 StartPos => m_Context.startPos;
        public Vector2 Delta => m_Context.delta;

        public void AddListeners(UnityAction[] listenrs)
        {
            Assert.IsTrue(listenrs.Length == 4);
            m_PressEvt.AddListener(listenrs[0]);
            m_LongPressEvt.AddListener(listenrs[1]);
            m_ClickEvt.AddListener(listenrs[2]);
            m_ReleaseEvt.AddListener(listenrs[3]); 
        }


#if UNITY_EDITOR
        private Mouse mouse;
#else
        private ITouch touch;
#endif

        private Context m_Context = new Context();

        private InputEvent m_PressEvt = new InputEvent();
        private InputEvent m_LongPressEvt = new InputEvent();
        private InputEvent m_ClickEvt = new InputEvent();
        private InputEvent m_ReleaseEvt = new InputEvent();

        /**
         * Classes
         * **/

        [Serializable]
        public class InputEvent : UnityEvent
        {
            public static InputEvent operator +(InputEvent ent, UnityAction callback)
            {
                ent.AddListener(callback);
                return ent;
            }

            public static InputEvent operator -(InputEvent ent, UnityAction callback)
            {
                ent.RemoveListener(callback);
                return ent;
            }
        }

        enum State
        {
            WAITING, // 等待输入
            PRESS, // 按住
            LONGPRESS // 长按
        }

        class Context
        {
            public State state = State.WAITING;

            public float startTime = 0f;

            public Vector2 startPos = Vector2.zero;
            public Vector2 currPos = Vector2.zero;
            public Vector2 delta = Vector2.zero;

            public readonly static float longPressTimeThreshold = 0.8f;
        }
    }
}

