using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBlockBehaviour : MonoBehaviour
{
    MapBehaviour mapBehaviour;

    public int row, col;

    public GameObject character = null;
    public GameObject barrier = null;

    public enum BlockType
    {
        Blank, // 空白区域
        Barrier, // 障碍区域
        Road, // 道路区域
        Character // 角色区域
    }

    public BlockType type = BlockType.Character;

    public bool selected = false;

    public Color activeColor;



    public MeshRenderer blockDesignRenderer;

    Material blockDesignMat;
    public Shader blockDesignShader;


    // Start is called before the first frame update
    void Start()
    {

        blockDesignMat = new Material(blockDesignShader);

        blockDesignRenderer.material = blockDesignMat;
    }

    private void OnDisable()
    {
        Clear();
    }

    public void Clear()
    {
        if (character)
        {
            Destroy(character);
            character = null;
        }

        if (barrier)
        {
            Destroy(barrier);
            barrier = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (type == BlockType.Blank)
        {
            if (mapBehaviour.placingChar)
            {
                activeColor = mapBehaviour.placingCharBlockColor;
            }
            else
            {
                activeColor = mapBehaviour.blankBlockColor;
            }
            
        }
        else if (type == BlockType.Barrier)
        {
            activeColor = mapBehaviour.barrierBlockColor;
        }
        else if (type == BlockType.Road)
        {
            activeColor = mapBehaviour.roadColor;
        }
        else
        {
            activeColor = mapBehaviour.CharBlockColor;
        }
        blockDesignMat.SetColor("_Color", activeColor);
    }

    public void SetTransform(int row, int col)
    {
        this.row = row; this.col = col;
        gameObject.name = $"{row}x{col}";
        transform.localPosition = new Vector3(row, 0, col) - 0.5f * new Vector3(mapBehaviour.mapLength, 0, mapBehaviour.mapLength);
        //transform.rotation = Quaternion.LookRotation(mapBehaviour.transform.forward, mapBehaviour.transform.up);
        transform.localRotation = Quaternion.identity;
    }

    public void SetMapBehaviour(MapBehaviour mapBehaviour)
    {
        this.mapBehaviour = mapBehaviour;
    }

    public void Select()
    {
        selected = true;
    }

    public void UnSelect()
    {
        selected = false;
    }


}
