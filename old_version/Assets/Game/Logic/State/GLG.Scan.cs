using Game.AR;
using Game.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.OpenVR;
using UnityEngine;

namespace Game
{
    namespace Logic
    {
        /// <summary>
        /// 扫描阶段逻辑
        /// </summary>
        public partial class GameLogic : MonoBehaviour
        {
            public class ScanStateFunction : BaseStateFunction
            {
                public override State state => State.SCANNING;

                /// <summary>
                /// 更新
                /// 
                /// 扫描画面，检测其中的平面
                /// </summary>
                public override void OnUpdate()
                {
                    ar.DetectPlane(out context.ar_anchorPosition, out context.ar_planeNormal);
                }

                public override void Enter()
                {
                    ar.SetActive(true);
                    Continue();
                }

                public override void Exit()
                {
                    Continue();

                    // 将地图平面的位置和法向信息写入context
                    context.map_center = context.ar_anchorPosition;
                    context.map_normal = context.ar_planeNormal;
                    

                    ar.SetActive(false);
                }

                public override void Pause()
                {
                    base.Pause();
                    ar.Pause();
                }

                public override void Continue()
                {
                    base.Continue();
                    ar.Continue();
                }
            }
        }
    }
}

