using System.Collections.Generic;
using System.Linq;
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
        /// <summary>
        /// GameLogic
        /// </summary>
        [AddComponentMenu("Game/Game Logic")]
        public partial class GameLogic : MonoBehaviour
        {
            private void Awake()
            {
                BaseStateFunction.logic = this;
                inputManager = GetComponent<InputManager>();
                inputManager.AddListeners(GetInputListeners());

                
                uiManager = GetComponent<UIManager>();
                uiManager.Load();
                uiManager.AddListeners(GetUIListeners());


                mapManager = GetComponent<MapManager>();
                mapManager.Load();

                characterManager = GetComponent<CharacterManager>();
                characterManager.Load();


                enemyManager = GetComponent<EnemyManager>();
                enemyManager.Load();


                arManager = GetComponent<ARManager>();
                arManager.Load();
            }

            private void OnDestroy()
            {
                BaseStateFunction.logic = null;
            }

            private void Start()
            {
                // TODO ��Ϸ״̬��ʼ��
                Restart();
            }

            // ����
            private void Update() => ActiveStateFunction.OnUpdate();
            private void LateUpdate() => ActiveStateFunction.OnLateUpdate();
            private void FixedUpdate() => ActiveStateFunction.OnFixedUpdate();

            
            BaseStateFunction GetStateFunction(State state)
            {
                BaseStateFunction stateFunc;
                return m_StateFunctions.TryGetValue(state, out stateFunc) ? stateFunc : new BaseStateFunction();
            }
            /// <summary>
            /// ״̬ת��
            /// </summary>
            void TransitionState(State target)
            {
                GetStateFunction(context.state).Exit(); // �뿪��ǰ״̬
                context.state = target; // �л�״̬
                GetStateFunction(context.state).Enter(); // ����Ŀ��״̬
            }

            /**
             * ���
             * **/

            /// <summary>
            /// �������״̬
            /// </summary>
            private void Restart()
            {
                uiManager.Reset();
                mapManager.Restart();
                characterManager.Restart();
                enemyManager.Restart();
                arManager.Restart();

                uiManager.OpenPanel(UI.Type.SCAN);
                TransitionState(State.SCANNING);
            }

            /// <summary>
            /// �˳����
            /// </summary>
            private void Quit() => Application.Quit();
        }
    }

}

