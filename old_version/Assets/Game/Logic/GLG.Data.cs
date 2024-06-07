using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Game.Character;
using Game.Enemy;
using Game.Map;
using Game.UI;
using Game.AR;

namespace Game
{
    namespace Logic
    {
        // 游戏状态
        public enum State
        {
            SCANNING, // 扫描
            CONFIGURING, // 配置地图选择角色
            GAMING, // 游戏进行中
        }

        /// <summary>
        /// Context组件
        /// 
        /// 用于维护游戏当前的各种数据属性
        /// </summary>
        [Serializable]
        public class Context
        {
            public bool running = true;
            public State state = State.SCANNING;


            /**
             * Map
             * **/
            public Vector3 map_center = Vector3.zero; // 地图平面中心
            public Vector3 map_normal = Vector3.up; // 地图平面法向
            public float map_rotationAngle = 0.0f; // 地图旋转角度
            public float map_scale = 1.0f; // 地图缩放倍数

            /**
             * Character
             * **/
            // 场上所有角色的ID
            public bool addingCharacters = false;
            public bool movingCharacters = false;
            public BaseCharacterBehaviour activeCharacter;
            public MapSquareBehaviour activeCharMapSquare;


            /**
             * AR
             * **/
            // 平面
            public bool ar_detPlane = false; // 是否检测到平面
            public Vector3 ar_anchorPosition = Vector3.zero; // 锚点位置
            public Vector3 ar_planeNormal = Vector3.zero; // 平面法向

            // 手部
            public bool ar_detHand = false; // 是否检测到手部
            public Vector3[] ar_handLandmarks = null; // 手部关键点坐标

            // 障碍物
            public bool ar_detObjects = false; // 是否检测到物体
            public List<Vector4> ar_objectBoxes = new List<Vector4>(); // 屏幕空间包围盒（minX，minY，maxX，maxY）


            /**
             * 相机以及屏幕参数
             * **/
            public float screenWidth => Screen.width;
            public float screenHeight => Screen.height;
        }

        /// <summary>
        /// GameLogic数据成员以及属性
        /// </summary>
        public partial class GameLogic : MonoBehaviour
        {
            private InputManager inputManager;
            private UIManager uiManager;
            private MapManager mapManager;
            private CharacterManager characterManager;
            private EnemyManager enemyManager;
            private ARManager arManager;

            private Context context = new Context();

            /// <summary>
            /// 各阶段回调函数库
            /// 
            /// 每个游戏阶段的具体更新方法与交互逻辑都在BaseStateFunction的基类中分别实现
            /// </summary>
            private Dictionary<State, BaseStateFunction> m_StateFunctions = new Dictionary<State, BaseStateFunction>()
            {
                { State.SCANNING, new ScanStateFunction() },
                { State.CONFIGURING, new ConfigStateFunction() },
                { State.GAMING, new GameStateFunction() },
            };

            BaseStateFunction ActiveStateFunction => GetStateFunction(context.state);


            /// <summary>
            /// 各个阶段需实现的基础回调，默认空函数
            /// </summary>
            public class BaseStateFunction
            {
                public static GameLogic logic = null;

                protected ARManager ar => logic.arManager;
                protected UIManager ui => logic.uiManager;
                protected InputManager input => logic.inputManager;
                protected MapManager map => logic.mapManager;
                protected CharacterManager characters => logic.characterManager;
                protected EnemyManager enemies => logic.enemyManager;
                protected Context context => logic.context;
                protected Camera camera => Camera.main;

                public virtual State state => State.SCANNING;
                public virtual void OnUpdate() 
                {
                    if (context.running) Debug.Log("Running: " + state.ToString());
                    else Debug.Log("Pausing: " + state.ToString());
                } // 更新
                public virtual void OnLateUpdate() { } // 更新
                public virtual void OnFixedUpdate() { } // 固定时间间隔更新
                public virtual void OnPress() { } // 按下
                public virtual void OnLongPress() { } // 长按
                public virtual void OnClick() { } // 点击
                public virtual void OnRelease() { } // 松开

                /// <summary>
                /// 进入状态时调用
                /// </summary>
                public virtual void Enter() { }
                /// <summary>
                /// 退出状态时调用
                /// </summary>
                public virtual void Exit() { }
                
                /// <summary>
                /// 暂停状态
                /// </summary>
                public virtual void Pause()
                {
                    context.running = false;
                }

                /// <summary>
                /// 继续运行
                /// </summary>
                public virtual void Continue()
                {
                    context.running = true;
                }

                /// <summary>
                /// 重置状态
                /// </summary>
                public void Reset()
                {
                    Exit();
                    Enter();
                }
            }
        }
    }
}

