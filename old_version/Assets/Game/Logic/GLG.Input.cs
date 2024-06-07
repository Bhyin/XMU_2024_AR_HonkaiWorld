using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    namespace Logic
    {
        /// <summary>
        /// GameLogic输入处理
        /// </summary>
        public partial class GameLogic : MonoBehaviour
        {
            /// <summary>
            /// 在OnEnable中调用
            /// </summary>
            public UnityAction[] GetInputListeners() => new UnityAction[4]
            {
                OnPress,
                OnLongPress,
                OnClick,
                OnRelease
            };

            public void OnPress() => ActiveStateFunction.OnPress();
            public void OnLongPress() => ActiveStateFunction.OnLongPress();
            public void OnClick() => ActiveStateFunction.OnClick();
            public void OnRelease() => ActiveStateFunction.OnRelease();
        }
    }
}

