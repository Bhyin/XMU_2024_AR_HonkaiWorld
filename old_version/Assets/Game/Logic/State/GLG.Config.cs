using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    namespace Logic
    {
        /// <summary>
        /// ���õ�ͼ�ͽ�ɫ�׶��߼�
        /// </summary>
        public partial class GameLogic : MonoBehaviour
        {
            public class ConfigStateFunction : BaseStateFunction
            {
                public override State state => State.CONFIGURING;

                Vector2 __delta;
                public override void OnUpdate()
                {
                    // ������ת��ͼ�Ĳ���������ز���д��context
                    if (context.running)
                    {
                        if (input.Touching)
                        {
                            __delta = input.Delta; // ��������
                            __delta.x /= context.screenWidth; // ˮƽ�����ı���
                            __delta.y /= context.screenHeight; // ���»����ı���

                            // ���һ�����ת������Ļ��໮���Ҳ࣬�պ��õ�ͼ��ת360��
                            map.MapRotate(-360 * __delta.x);

                            // ���»������ţ�����Ļ�·������Ϸ����պ��õ�ͼ���ű�����2
                            map.MapScaleMul(1.0f + __delta.y);
                        }
                    }
                }

                public override void Enter()
                {
                    map.SetMapTransform(context.map_center, context.map_normal);
                    map.MapRotate(0f);
                    map.MapScale(1f);
                    map.SetActive(true);
                    Continue();
                }

                public override void Exit()
                {
                    map.SetActive(false);
                    Continue();
                }

            }
        }
    }
}

