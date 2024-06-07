using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Map
{

    public partial class MapManager : MonoBehaviour
    {
        // 成员变量
        [SerializeField] Transform m_Map;
        [SerializeField] GameObject m_SquarePrefab; // 地图方块预制体
        [SerializeField] int m_Row; // 地图行数
        [SerializeField] int m_Col; // 地图列数

        // 地图使用余弦创建的预制体，不再运行时创建
        private Dictionary<Vector2Int, MapSquareBehaviour> m_SquareDict = new Dictionary<Vector2Int, MapSquareBehaviour>();

        // 从起点向终点的活动路径
        private List<Vector2Int> m_Path = new List<Vector2Int>();
        private List<Vector3> m_PathPositions = new List<Vector3>();
        private Vector2Int m_Start, m_End;

        private Utils.AStar m_AStarInstance;

        // 属性
        public int row => m_Row;
        public int col => m_Col;

        public float squareLength => Vector3.Distance(this[0, 0].Position, this[0, 1].Position);

        public float scaleFactor => GetMapScale() / Mathf.Min(m_Row, m_Col); // 用于设置角色和敌人的缩放

        public MapSquareBehaviour this[Vector2Int coord] => GetBehaviour(coord);

        public MapSquareBehaviour this[int row, int col] => GetBehaviour(new Vector2Int(row, col));

    }
}


