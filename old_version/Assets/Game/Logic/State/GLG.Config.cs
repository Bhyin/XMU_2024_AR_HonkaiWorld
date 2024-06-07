using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    namespace Logic
    {
        /// <summary>
        /// 配置地图和角色阶段逻辑
        /// </summary>
        public partial class GameLogic : MonoBehaviour
        {
            public class ConfigStateFunction : BaseStateFunction
            {
                public override State state => State.CONFIGURING;

                Vector2 __delta;
                public override void OnUpdate()
                {
                    // 缩放旋转地图的操作，将相关参数写入context
                    if (context.running)
                    {
                        if (input.Touching)
                        {
                            __delta = input.Delta; // 滑动向量
                            __delta.x /= context.screenWidth; // 水平滑动的比例
                            __delta.y /= context.screenHeight; // 上下滑动的比例

                            // 左右滑动旋转，从屏幕左侧划到右侧，刚好让地图旋转360度
                            map.MapRotate(-360 * __delta.x);

                            // 上下滑动缩放，从屏幕下方划到上方，刚好让地图缩放倍数乘2
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

