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
        /// ɨ��׶��߼�
        /// </summary>
        public partial class GameLogic : MonoBehaviour
        {
            public class ScanStateFunction : BaseStateFunction
            {
                public override State state => State.SCANNING;

                /// <summary>
                /// ����
                /// 
                /// ɨ�軭�棬������е�ƽ��
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

                    // ����ͼƽ���λ�úͷ�����Ϣд��context
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

