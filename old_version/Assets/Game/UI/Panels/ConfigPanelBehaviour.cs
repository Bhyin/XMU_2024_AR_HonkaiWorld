using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    namespace UI
    {
        public class ConfigPanelBehaviour : BaseUIPanelBehaviour
        {
            public override Type type => Type.CONFIG;

            [Tooltip("ȷ�ϰ�ť")]
            [SerializeField] Button confirmBtn;
            [Tooltip("���ð�ť")]
            [SerializeField] Button resetBtn;
            [Tooltip("���˰�ť")]
            [SerializeField] Button backBtn;
            [Tooltip("��ͣ��ť")]
            [SerializeField] Button pauseBtn;
            [Tooltip("���ð�ť")]
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
                resetBtn.onClick.AddListener(evtDict["ResetButton"]);
                backBtn.onClick.AddListener(evtDict["BackButton"]);
                pauseBtn.onClick.AddListener(evtDict["PauseButton"]);
                settingBtn.onClick.AddListener(evtDict["SettingButton"]);
            }

            public override void RemoveAllListeners()
            {
                confirmBtn.onClick.RemoveAllListeners();
                resetBtn.onClick.RemoveAllListeners();
                backBtn.onClick.RemoveAllListeners();
                pauseBtn.onClick.RemoveAllListeners();
                settingBtn.onClick.RemoveAllListeners();
            }

            public override void SetInteractable(bool interactable)
            {
                confirmBtn.interactable = interactable;
                resetBtn.interactable = interactable;
                backBtn.interactable = interactable;
                pauseBtn.interactable = interactable;
                settingBtn.interactable = interactable;
            }
        }
    }
}
