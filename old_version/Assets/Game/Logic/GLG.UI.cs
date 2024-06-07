using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    namespace Logic
    {
        /// <summary>
        /// GameLogic UI处理
        /// 
        /// 所有与UI面板**切换**相关的代码都在该文件集中定义
        /// </summary>
        public partial class GameLogic : MonoBehaviour
        {
            /// <summary>
            /// 在OnEnable中调用
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
             * CONFIG界面回调
             * **/
            // 配置面板确认按钮
            void OnConfigConfirmButtonClick() => OnConfirmButtonClick(UI.Type.GAME, State.GAMING);
            void OnConfigBackButtonClick() => OnConfirmButtonClick(UI.Type.SCAN, State.SCANNING);

            /**
             * SCAN界面回调
             * **/
            // SCAN面板确认按钮
            void OnScanConfirmButtonClick() => OnConfirmButtonClick(UI.Type.CONFIG, State.CONFIGURING);
 
            /**
             * 通用方法
             * **/
            // 进入设置面板
            void OnEnterSettingPanel()
            {
                __preType = uiManager.Top.type;
             
                // 关闭当前面板，打开设置面板，替换设置面板的退出回调，设置状态暂停
                uiManager.ClosePanel();
                uiManager.OpenPanel(UI.Type.SETTING);
                
                // 暂停当前模式
                ActiveStateFunction.Pause();
            }

            UI.Type __preType;
            // 退出设置面板
            void OnExitSettingPanel()
            {
                // 关闭设置面板，打开目标面板，继续当前模式
                uiManager.ClosePanel();
                uiManager.OpenPanel(__preType);

                ActiveStateFunction.Continue();
            }

            // 点击暂停按钮
            void OnPauseButtonClick()
            {
                // 打开暂停面板，替换暂停面板退出回调，暂停当前模式
                uiManager.OpenPanel(UI.Type.PAUSE);
                ActiveStateFunction.Pause();
            }

            // 点击继续按钮
            void OnContinueButtonClick()
            {
                // 关闭暂停面板，继续当前模式
                uiManager.ClosePanel();
                ActiveStateFunction.Continue();
            }

            // 点击确认按钮
            void OnConfirmButtonClick(UI.Type targetUI, State target)
            {
                // 关闭当前界面，进入目标面板，进入目标状态
                uiManager.ClosePanel();
                uiManager.OpenPanel(targetUI);

                TransitionState(target);
            }

            // 点击重置按钮
            void OnResetButtonClick()
            {
                ActiveStateFunction.Reset();
            }
        }
    }
}

