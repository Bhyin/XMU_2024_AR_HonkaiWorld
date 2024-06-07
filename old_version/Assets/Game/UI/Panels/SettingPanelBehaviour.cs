using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    namespace UI
    {
        public class SettingPanelBehaviour : BaseUIPanelBehaviour
        {
            [Tooltip("ÍË³ö°´Å¥")]
            [SerializeField] Button exitBtn;

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
                exitBtn.onClick.AddListener(evtDict["ExitButton"]);
            }

            public override void RemoveAllListeners()
            {
                exitBtn.onClick.RemoveAllListeners();
            }

            public override void SetInteractable(bool interactable)
            {
                
            }

            public void ReplaceExitButtonCallback(UnityAction action)
            {
                exitBtn.onClick.RemoveAllListeners();
                exitBtn.onClick.AddListener(action);
            }
        }
    }
}

