using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    namespace Logic
    {
        /// <summary>
        /// GameLogic UI����
        /// 
        /// ������UI���**�л�**��صĴ��붼�ڸ��ļ����ж���
        /// </summary>
        public partial class GameLogic : MonoBehaviour
        {
            /// <summary>
            /// ��OnEnable�е���
            /// </summary>
            Dictionary<UI.Type, Dictionary<string, UnityAction>> GetUIListeners()
            {
                Dictionary<UI.Type, Dictionary<string, UnityAction>> listeners = new Dictionary<UI.Type, Dictionary<string, UnityAction>>();
                listeners.Add(UI.Type.SCAN, new Dictionary<string, UnityAction>() {
                    {"ConfirmButton", OnScanConfirmButtonClick },
                    {"ResetButton", OnResetButtonClick },
                    {"SettingButton", OnEnterSettingPanel},
                    {"PauseButton", OnPauseButtonClick },
                });
                listeners.Add(UI.Type.CONFIG, new Dictionary<string, UnityAction>() {
                    {"ConfirmButton", OnConfigConfirmButtonClick },
                    {"ResetButton", OnResetButtonClick },
                    {"BackButton", OnConfigBackButtonClick },
                    {"SettingButton", OnEnterSettingPanel },
                    {"PauseButton", OnPauseButtonClick },
                });
                listeners.Add(UI.Type.GAME, new Dictionary<string, UnityAction>() {
                    {"SettingButton", OnEnterSettingPanel },
                    {"PauseButton", OnPauseButtonClick },
                    {"AddButton", OnAddButtonClick },
                });
                listeners.Add(UI.Type.PAUSE, new Dictionary<string, UnityAction>() {
                    {"ContinueButton", OnContinueButtonClick },
                    {"RestartButton", Restart },
                    {"QuitButton", Quit },
                });
                listeners.Add(UI.Type.SETTING, new Dictionary<string, UnityAction>() {
                    {"ExitButton", OnExitSettingPanel },
                });

                return listeners;
            }

            public void OnAddButtonClick()
            {
                GameStateFunction stateFunc = ActiveStateFunction as GameStateFunction;
                stateFunc.AddCharacter();
            }


            /**
             * CONFIG����ص�
             * **/
            // �������ȷ�ϰ�ť
            void OnConfigConfirmButtonClick() => OnConfirmButtonClick(UI.Type.GAME, State.GAMING);
            void OnConfigBackButtonClick() => OnConfirmButtonClick(UI.Type.SCAN, State.SCANNING);

            /**
             * SCAN����ص�
             * **/
            // SCAN���ȷ�ϰ�ť
            void OnScanConfirmButtonClick() => OnConfirmButtonClick(UI.Type.CONFIG, State.CONFIGURING);
 
            /**
             * ͨ�÷���
             * **/
            // �����������
            void OnEnterSettingPanel()
            {
                __preType = uiManager.Top.type;
             
                // �رյ�ǰ��壬��������壬�滻���������˳��ص�������״̬��ͣ
                uiManager.ClosePanel();
                uiManager.OpenPanel(UI.Type.SETTING);
                
                // ��ͣ��ǰģʽ
                ActiveStateFunction.Pause();
            }

            UI.Type __preType;
            // �˳��������
            void OnExitSettingPanel()
            {
                // �ر�������壬��Ŀ����壬������ǰģʽ
                uiManager.ClosePanel();
                uiManager.OpenPanel(__preType);

                ActiveStateFunction.Continue();
            }

            // �����ͣ��ť
            void OnPauseButtonClick()
            {
                // ����ͣ��壬�滻��ͣ����˳��ص�����ͣ��ǰģʽ
                uiManager.OpenPanel(UI.Type.PAUSE);
                ActiveStateFunction.Pause();
            }

            // ���������ť
            void OnContinueButtonClick()
            {
                // �ر���ͣ��壬������ǰģʽ
                uiManager.ClosePanel();
                ActiveStateFunction.Continue();
            }

            // ���ȷ�ϰ�ť
            void OnConfirmButtonClick(UI.Type targetUI, State target)
            {
                // �رյ�ǰ���棬����Ŀ����壬����Ŀ��״̬
                uiManager.ClosePanel();
                uiManager.OpenPanel(targetUI);

                TransitionState(target);
            }

            // ������ð�ť
            void OnResetButtonClick()
            {
                ActiveStateFunction.Reset();
            }
        }
    }
}

