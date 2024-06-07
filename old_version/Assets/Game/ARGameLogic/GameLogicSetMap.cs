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
            // ���㻬������
            Vector2 delta = interactionManager.delta;
            delta.Normalize();

            float angle = Vector2.Dot(delta, Vector2.up);
            if (Mathf.Abs(angle) > angleThreshold)
            {
                // ���»�������
                mapBehaviour.SetDeltaScale(delta.y * Time.deltaTime);
            }
            else if(Mathf.Abs(angle) < angleThreshold)
            {
                // ���һ�����ת
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
