using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class GameLogic : MonoBehaviour
{
    public void PlacingCharUpdate()
    {
        
    }


    

    public void PlacingCharStart()
    {
        process = GameProcess.PlacingCharacter;
        mapBehaviour.PlacingCharStart();
    }

    public void PlacingCharFinish()
    {
        // 有待放置的角色
        process = GameProcess.Gaming;
        mapBehaviour.PlacingCharFinish();
    }

    public void PlacingCharPressCallback()
    {
        // 如果点击到按钮，就不触发

        Vector2 screenPositioin = interactionManager.position;

        Ray ray = mainCamera.ScreenPointToRay(screenPositioin);
        if (Physics.Raycast(ray, out hit, 100f, 1 << LayerMask.NameToLayer("Map")))
        {
            MapBlockBehaviour mb = hit.collider.gameObject.GetComponent<MapBlockBehaviour>();
            if (mb != null && mb.type == MapBlockBehaviour.BlockType.Blank)
            {
                mb.type = MapBlockBehaviour.BlockType.Character;
                GameObject character = Instantiate(characterPrefab, mb.transform);
                BaseCharacterBehaviour bcp = character.GetComponent<BaseCharacterBehaviour>();
                bcp.row = mb.row;
                bcp.col = mb.col;
                bcp.SetGameLogic(this);
                character.SetActive(true);
                character.transform.localPosition = Vector3.zero;
            }
        }

        PlacingCharFinish();
    }

    public void PlacingCharClickCallback()
    {
        
    }

}
