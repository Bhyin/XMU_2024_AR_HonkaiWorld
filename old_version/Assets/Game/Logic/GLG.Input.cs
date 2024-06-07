using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    namespace Logic
    {
        /// <summary>
        /// GameLogic���봦��
        /// </summary>
        public partial class GameLogic : MonoBehaviour
        {
            /// <summary>
            /// ��OnEnable�е���
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

