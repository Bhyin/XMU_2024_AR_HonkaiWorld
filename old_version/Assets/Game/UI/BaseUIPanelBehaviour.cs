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
        /// �������ƣ�Ϊ��Ӧ��UI�ɽ�����������¼���Ӧ����
        /// </summary>
        public virtual void AddListeners(Dictionary<string, UnityAction> evtDict)
        {
            
        }

        /// <summary>
        /// �Ƴ����������������Ӧ����
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

