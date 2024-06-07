using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;


namespace Game
{
    


    /// <summary>
    /// 配置项基类
    /// 
    /// 为类型TYPE定义配置项数据
    /// </summary>
    [Serializable]
    public class BasePropertyItem<TYPE>
    {
        public TYPE type;
    }

    /// <summary>
    /// 配置项资产文件类
    /// 
    /// 给定一个TYPE和数据项基类ITEM定义泛型
    /// 为不同的类型提供不同的配置
    /// 
    /// 在类型对应的**Manager脚本中读取配置数据，传递给其它脚本
    /// 配置项的数据不应被**Manager之外的脚本读取
    /// </summary>
    public class BaseProperty<TYPE, ITEM> : ScriptableObject
        where ITEM : BasePropertyItem<TYPE>
    {
        /// <summary>
        /// 所有配置项的列表
        /// 
        /// 在Inspector中定义
        /// </summary>
        [Tooltip("配置项列表")]
        [SerializeField] ITEM[] m_Items;

        /// <summary>
        /// 类型与配置项的字典
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
        /// 根据类型获取配置项
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

