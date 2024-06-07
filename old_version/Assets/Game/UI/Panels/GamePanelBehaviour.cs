using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    namespace UI
    {
        public class GamePanelBehaviour : BaseUIPanelBehaviour
        {
            [Tooltip("��ͣ��ť")]
            [SerializeField] Button pauseBtn;
            [Tooltip("���ð�ť")]
            [SerializeField] Button settingBtn;
            [Tooltip("��Ӱ�ť")]
            [SerializeField] Button addBtn;

            public override void AddListeners(Dictionary<string, UnityAction> evtDict)
            {
                RemoveAllListeners();
                pauseBtn.onClick.AddListener(evtDict["PauseButton"]);
                settingBtn.onClick.AddListener(evtDict["SettingButton"]);
                addBtn.onClick.AddListener(evtDict["AddButton"]);
            }

            public override void RemoveAllListeners()
            {
                pauseBtn.onClick.RemoveAllListeners();
                settingBtn.onClick.RemoveAllListeners();
                addBtn.onClick.RemoveAllListeners();
            }

            public override void SetInteractable(bool interactable)
            {
                pauseBtn.interactable = interactable;
                settingBtn.interactable = interactable;
                addBtn.interactable = interactable;
            }
        }
    }
}
