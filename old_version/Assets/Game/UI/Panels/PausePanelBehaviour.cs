using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    namespace UI
    {
        public class PausePanelBehaviour : BaseUIPanelBehaviour
        {
            [Tooltip("������ť")]
            [SerializeField] Button continueBtn;
            [Tooltip("���¿�ʼ��ť")]
            [SerializeField] Button restartBtn;
            [Tooltip("�˳���ť")]
            [SerializeField] Button quitBtn;

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
                continueBtn.onClick.AddListener(evtDict["ContinueButton"]);
                restartBtn.onClick.AddListener(evtDict["RestartButton"]);
                quitBtn.onClick.AddListener(evtDict["QuitButton"]);
            }

            public override void RemoveAllListeners()
            {
                continueBtn.onClick.RemoveAllListeners();
                restartBtn.onClick.RemoveAllListeners();
                quitBtn.onClick.RemoveAllListeners();
            }

            public override void SetInteractable(bool interactable)
            {
                continueBtn.interactable = interactable;
                restartBtn.interactable = interactable;
                quitBtn.interactable = interactable;
            }
        }
    }
}

