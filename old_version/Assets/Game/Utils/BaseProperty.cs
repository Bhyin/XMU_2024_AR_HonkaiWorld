using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;


namespace Game
{
    


    /// <summary>
    /// ���������
    /// 
    /// Ϊ����TYPE��������������
    /// </summary>
    [Serializable]
    public class BasePropertyItem<TYPE>
    {
        public TYPE type;
    }

    /// <summary>
    /// �������ʲ��ļ���
    /// 
    /// ����һ��TYPE�����������ITEM���巺��
    /// Ϊ��ͬ�������ṩ��ͬ������
    /// 
    /// �����Ͷ�Ӧ��**Manager�ű��ж�ȡ�������ݣ����ݸ������ű�
    /// ����������ݲ�Ӧ��**Manager֮��Ľű���ȡ
    /// </summary>
    public class BaseProperty<TYPE, ITEM> : ScriptableObject
        where ITEM : BasePropertyItem<TYPE>
    {
        /// <summary>
        /// ������������б�
        /// 
        /// ��Inspector�ж���
        /// </summary>
        [Tooltip("�������б�")]
        [SerializeField] ITEM[] m_Items;

        /// <summary>
        /// ��������������ֵ�
        /// </summary>
        private Dictionary<TYPE, ITEM> m_Dict;

        private void CheckNull()
        {
            if (m_Dict == null)
            {
                m_Dict = new Dictionary<TYPE, ITEM>();
                foreach (var item in m_Items)
                {
                    m_Dict[item.type] = item;
                }
            }
        }

        public List<TYPE> Keys
        {
            get
            {
                CheckNull();
                return m_Dict.Keys.ToList();
            }
        }

        /// <summary>
        /// �������ͻ�ȡ������
        /// </summary>
        public ITEM this[TYPE type]
        {
            get
            {
                CheckNull();
                return m_Dict[type];
            }
        }

        
    }
}

