
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    namespace UI
    {

        /// <summary>
        /// UI管理器
        /// 用于控制UI面板的打开和关闭
        /// 其接口仅供GameLogic使用
        /// </summary>
        [AddComponentMenu("Game/UI/UI Manager")]
        public class UIManager : MonoBehaviour
        {
            /**
             * 接口
             * **/

            /// <summary>
            /// 根据传入的type打开对应的面板
            /// </summary>
            public void OpenPanel(Type type)
            {
                // 禁用顶层面板交互
                SetPeekInteractable(false);


                // 激活对应ID的面板
                BaseUIPanelBehaviour panel = GetBehaviour(type);
                panel.SetActive(true);
                panel.SetInteractable(true);

                // 入栈
                Push(panel);
            }

            /// <summary>
            /// 关闭当前顶部面板
            /// </summary>
            public void ClosePanel()
            {
                // 出栈
                BaseUIPanelBehaviour panel = Pop();

                // 禁用栈顶面板
                if (panel != null)
                {
                    panel.SetInteractable(false);
                    panel.SetActive(false);
                }

                // 启用顶层面板交互
                SetPeekInteractable(true);
            }

            private void SetPeekInteractable(bool interactable) => Top?.SetInteractable(interactable);


            private void Push(BaseUIPanelBehaviour panel) => m_PanelStack.Push(panel);


            private BaseUIPanelBehaviour Pop()
            {
                return m_PanelStack.Count > 0 ? m_PanelStack.Pop() : null;
            }

            private BaseUIPanelBehaviour GetBehaviour(Type type)
            {
                BaseUIPanelBehaviour behaviour;
                if(m_PanelsDict.TryGetValue(type, out behaviour))
                {
                    return behaviour;
                }
                return default;
            }

            public void Reset()
            {
                while (m_PanelStack.Count > 0) ClosePanel();
            }

            /**
             * 在Awake中调用
             * 
             * 实例化所有数据结构
             * **/
            public void Load()
            {
                // 创建所有UI面板实例
                foreach (Type type in m_Property.Keys)
                {
                    GameObject prefab = Instantiate(m_Property[type].panelPrefab, m_Canvas);
                    BaseUIPanelBehaviour panel = prefab.GetComponent<BaseUIPanelBehaviour>();
                    panel.SetActive(false);
                    panel.SetInteractable(false);

                    m_PanelsDict.Add(type, panel);
                }
            }

            /// <summary>
            /// 将GameLogic中定义的事件回调函数传递到各个Panel中
            /// </summary>
            public void AddListeners(Dictionary<Type, Dictionary<string, UnityAction>> callbacks)
            {
                foreach (Type type in callbacks.Keys.ToList())
                {
                    m_PanelsDict[type].RemoveAllListeners();
                    m_PanelsDict[type].AddListeners(callbacks[type]);
                }
            }

            /**
             * Attributes
             * **/
            [SerializeField] UIPanelProperty m_Property;
            [SerializeField] Transform m_Canvas;
            private Dictionary<Type, BaseUIPanelBehaviour> m_PanelsDict = new Dictionary<Type, BaseUIPanelBehaviour>();
            private Stack<BaseUIPanelBehaviour> m_PanelStack = new Stack<BaseUIPanelBehaviour>();

            public BaseUIPanelBehaviour Top
            {
                get
                {
                    if (m_PanelStack != null && m_PanelStack.Count > 0)
                    {
                        return m_PanelStack.Peek();
                    }
                    return null;
                }
            }

        }
    }

}

