using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    namespace UI
    {
        public class ScanPanelBehaviour : BaseUIPanelBehaviour
        {
            [Tooltip("确认按钮")]
            [SerializeField] Button confirmBtn;
            [Tooltip("暂停按钮")]
            [SerializeField] Button pauseBtn;
            [Tooltip("设置按钮")]
            [SerializeField] Button settingBtn;

            // Start is called before the first frame update
            void Start()
            {

            }

            // Update is called once per frame
            void Update()
            {

            }

            public override void AddListeners(Dictionary<string, UnityAction> evtDict)
            {
                RemoveAllListeners();
                confirmBtn.onClick.AddListener(evtDict["ConfirmButton"]);
                settingBtn.onClick.AddListener(evtDict["SettingButton"]);
                pauseBtn.onClick.AddListener(evtDict["PauseButton"]);
            }

            public override void RemoveAllListeners()
            {
                confirmBtn.onClick.RemoveAllListeners();
                pauseBtn.onClick.RemoveAllListeners();
                settingBtn.onClick.RemoveAllListeners();
            }

            public override void SetInteractable(bool interactable)
            {
                confirmBtn.interactable = interactable;
                settingBtn.interactable = interactable;
                pauseBtn.interactable = interactable;
            }

        }
    }
}

