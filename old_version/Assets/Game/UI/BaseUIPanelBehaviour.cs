using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.UI
{
    public class BaseUIPanelBehaviour : MonoBehaviour
    {
        public virtual Type type => Type.SCAN;

        /// <summary>
        /// 依据名称，为对应的UI可交互组件设置事件响应函数
        /// </summary>
        public virtual void AddListeners(Dictionary<string, UnityAction> evtDict)
        {
            
        }

        /// <summary>
        /// 移除交互组件中所有响应函数
        /// </summary>
        public virtual void RemoveAllListeners()
        {

        }

        public virtual void SetInteractable(bool interactable)
        {

        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

    }
}

