using System;
using System.Collections.Generic;
using UnityEngine;
using URandom = UnityEngine.Random;

public class MapBehaviour : MonoBehaviour
{
    public InteractionManager interactionManager;
    Dictionary<Tuple<int, int>, MapBlockBehaviour> mapBlockDict;

    public AnchorBehaviour anchorBehaviour;
    public GameObject MapBlockPrefab;

    public MapBlockBehaviour activeBlockBehaviour;

    public float deltaScale = 10f;
    public float deltaAngle = -180f;

    public float defaultScale = 0.01f;
    [Min(10)]
    public int mapSize = 10;
    public float mapLength => mapSize;

    public bool placingChar = false;


    public Color blankBlockColor; // 空白格子颜色
    public Color placingCharBlockColor; // 未在选择状态的角色格子颜色
    public Color CharBlockColor; // 选择状态格子颜色
    public Color roadColor;
    public Color barrierBlockColor;

    public Texture2D barrierBlockTex;
    public Texture2D blankBlockTex;
    public Texture2D roadBlockTex;

    MapBlockBehaviour startBlock;
    MapBlockBehaviour endBlock;
    public List<Vector2Int> road;


    private void Awake()
    {
        road = new List<Vector2Int>();
        GetRoad();
        CreateMapBlocks();
        //SetStartAndEnd();
        //GetRoad();
        transform.localScale = transform.localScale * defaultScale;
    }

    private void OnEnable()
    {
        transform.position = anchorBehaviour.transform.position;
        transform.rotation = anchorBehaviour.transform.rotation;
    }

    public void OnDisable()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public MapBlockBehaviour GetBlock(int row, int col)
    {
        return mapBlockDict[new Tuple<int, int>(row, col)];
    }

    public void CreateMapBlocks()
    {
        mapBlockDict = new Dictionary<Tuple<int, int>, MapBlockBehaviour>();
        for (int row = 0; row < mapSize; ++row)
        {
            for (int col = 0; col < mapSize; ++col)
            {
                GameObject block = Instantiate(MapBlockPrefab, transform);
                MapBlockBehaviour behaviour = block.GetComponent<MapBlockBehaviour>();
                behaviour.Clear();
                behaviour.SetMapBehaviour(this);
                behaviour.SetTransform(row, col);

                behaviour.type = MapBlockBehaviour.BlockType.Blank;
                if (road.Contains(new Vector2Int(row, col)))
                {
                    behaviour.type = MapBlockBehaviour.BlockType.Road;
                }
                else
                {
                    // 随机生成障碍物
                    if (URandom.Range(1, 10) > 7 && (col != 0 && col != mapSize - 1))
                    {
                        behaviour.type = MapBlockBehaviour.BlockType.Barrier;
                    }
                }

                behaviour.gameObject.layer = gameObject.layer;
                mapBlockDict.Add(new Tuple<int, int>(row, col), behaviour);
            }
        }
    }

    public void SetStartAndEnd()
    {
        int r1 = URandom.Range(0, 5);
        int r2 = URandom.Range(5, 9);
        startBlock = GetBlock(0, r1);
        endBlock = GetBlock(mapSize - 1, r2);
    }

    public void GetRoad()
    {
        //// 深度优先搜索
        //found = false;
        //mark = new bool[mapSize, mapSize];
        //for (int i = 0; i < mapSize; ++i)
        //{
        //    for (int j = 0; j < mapSize; ++j)
        //    {
        //        mark[i, j] = GetBlock(i, j).type != MapBlockBehaviour.BlockType.Blank;
        //    }
        //}

        //DFS(startBlock.row, startBlock.col);

        //foreach (var roadBlock in road)
        //{
        //    GetBlock(roadBlock.x, roadBlock.y).type = MapBlockBehaviour.BlockType.Road;
        //}

        road = new List<Vector2Int>()
        {
            new Vector2Int(4, 0),
            new Vector2Int(4, 1),
            new Vector2Int(3, 1),
            new Vector2Int(2, 1),
            new Vector2Int(1, 1),
            new Vector2Int(1, 2),
            new Vector2Int(1, 3),
            new Vector2Int(1, 4),
            new Vector2Int(2, 4),
            new Vector2Int(3, 4),
            new Vector2Int(4, 4),
            new Vector2Int(5, 4),
            new Vector2Int(6, 4),
            new Vector2Int(7, 4),
            new Vector2Int(8, 4),
            new Vector2Int(8, 5),
            new Vector2Int(8, 6),
            new Vector2Int(8, 7),
            new Vector2Int(7, 7),
            new Vector2Int(6, 7),
            new Vector2Int(5, 7),
            new Vector2Int(4, 7),
            new Vector2Int(3, 7),
            new Vector2Int(2, 7),
            new Vector2Int(2, 8),
            new Vector2Int(2, 9),
            new Vector2Int(3, 9),
            new Vector2Int(4, 9),
            new Vector2Int(5, 9),
            new Vector2Int(6, 9),
            new Vector2Int(7, 9),
        };

        List<Vector2Int> road7 = new List<Vector2Int>()
        {
            new Vector2Int(0, 0)
        };

    }


    bool found = false;
    bool[,] mark = null;
    void DFS(int row, int col)
    {
        //if (found) return;
        //MapBlockBehaviour mb = GetBlock(row, col);

        //if (mb == endBlock)
        //{
        //    road.Push(new Vector2Int(row, col));
        //    found = true;
        //    return;
        //}
        //road.Push(new Vector2Int(row, col));
        //mark[row, col] = true;

        //List<Vector2Int> neighbours = GetDpsDirection(mb.row, mb.col);
        //foreach (Vector2Int v in neighbours)
        //{
        //    if (!mark[v.x, v.y] && !found && GetBlock(v.x, v.y).type == MapBlockBehaviour.BlockType.Blank) DPS(v.x, v.y);
        //}
        //if (!found) road.Pop();

    }

    public List<Vector2Int> GetDpsDirection(int row, int col)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>();
        int[,] direction = new int[4, 2] { { 1, 0 }, { 0, 1 }, { 0, -1 }, { -1, 0 } };
        for (int i = 0; i < 4; ++i)
        {
            int dx = row + direction[i, 0], dy = col + direction[i, 1];
            if (dx >= 0 && dx < mapSize && dy >= 0 && dy < mapSize)
            {
                neighbours.Add(new Vector2Int(dx, dy));
            }
        }

        return neighbours;
    }

    public void SetEnemyAtStartBlock(BaseEnemyBehaviour beb)
    {
        // 把敌人放在起点位置
    }

    public void PlacingCharStart()
    {
        // 把有效的格子的颜色变为蓝色，其它不变
        placingChar = true;
    }



    public void ResetMap()
    {
        for (int row = 0; row < mapSize; ++row)
        {
            for (int col = 0; col < mapSize; ++col)
            {
                Destroy(mapBlockDict[new Tuple<int, int>(row, col)].gameObject);
            }
        }
        road.Clear();
        mapBlockDict.Clear();

        GetRoad();
        CreateMapBlocks();
        //SetStartAndEnd();
        
    }

    public void PlacingCharFinish()
    {
        placingChar = false;
    }


    public void SetDeltaRotation(float ratio)
    {
        Vector3 originEuler = transform.localEulerAngles;
        originEuler.y += ratio * deltaAngle;
        transform.localEulerAngles = originEuler;
    }

    public void SetDeltaScale(float ratio)
    {
        Vector3 originScale = transform.localScale;
        originScale = originScale + ratio * deltaScale * Vector3.one;
        if (originScale.y < 0.01f) return;
        transform.localScale = originScale;
    }

    public void OnPressCallback()
    {

    }

    public void OnClickCallback()
    {

    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void SetAnchor(AnchorBehaviour anchor)
    {
        anchorBehaviour = anchor;
    }
}
