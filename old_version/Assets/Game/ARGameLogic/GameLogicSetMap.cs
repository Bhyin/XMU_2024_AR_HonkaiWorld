using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameLogic : MonoBehaviour
{
    readonly float angleThreshold = Mathf.Cos(Mathf.PI / 8f);

    public void SetMapUpdate()
    {
        if (interactionManager.touching)
        {
            // 计算滑动方向
            Vector2 delta = interactionManager.delta;
            delta.Normalize();

            float angle = Vector2.Dot(delta, Vector2.up);
            if (Mathf.Abs(angle) > angleThreshold)
            {
                // 上下滑动缩放
                mapBehaviour.SetDeltaScale(delta.y * Time.deltaTime);
            }
            else if(Mathf.Abs(angle) < angleThreshold)
            {
                // 左右滑动旋转
                mapBehaviour.SetDeltaRotation(delta.x * Time.deltaTime);
            }
        }

    }

    public void SetMapStart()
    {
        mapBehaviour.SetActive(true);
        uiManager.OpenPanel(PanelItems.game_SetMapPanel);
    }

    public void SetMapFinish()
    {
        //mapBehaviour.SetActive(false);
        uiManager.ClosePanel();
    }
}
