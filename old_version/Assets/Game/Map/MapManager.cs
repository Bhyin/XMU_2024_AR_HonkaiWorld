using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.XR.OpenVR;
using UnityEditor.Hardware;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI.Table;

namespace Game.Map
{
    /// <summary>
    /// 地图管理器
    /// 实现地图的加载，控制地图方块的状态，计算坐标、路径、区域。
    /// 其接口仅供GameLogic使用
    /// </summary>
    [AddComponentMenu("Game/Map/Map Manager")]
    public partial class MapManager : MonoBehaviour
    {
        public void SetMapTransform(Vector3 position, Vector3 up)
        {
            m_Map.position = position;
            m_Map.up = up;
        }
        public void MapRotate(float angle) => m_Map.Rotate(0f, angle, 0f, Space.Self);
        public void MapScale(float scale) => m_Map.localScale = new Vector3(scale, 1f, scale);
        public void MapScaleMul(float factor) => MapScale(GetMapScale() * factor);
        public float GetMapScale() => m_Map.localScale.x;

        /// <summary>
        /// 剪枝函数
        /// 判断坐标是否越界且可以作为通路
        /// </summary>
        public bool PrunningFunc(Vector2Int coord)
        {
            if (0 <= coord.x && coord.x < m_Row && 0 <= coord.y && coord.y < m_Col)
            {
                Type t = this[coord].type;
                return t == Type.SPACE || t == Type.PLACINGCHAR || t == Type.START || t == Type.END;
            }
            return false;
        }


        /**
         * 给定坐标和类型，将对应坐标的类型设置为给定类型
         * **/
        public void SetSquareType(Vector2Int coord, Type type)
        {
            MapSquareBehaviour s = this[coord];
            if (s != null) s.SetType(type);
        }

        public void SetSquareType(List<Vector2Int> coords, Type type)
        {
            foreach (var coord in coords)
            {
                SetSquareType(coord, type);
            }
        }

        public void SetSquareType(Vector2Int[] coords, Type type)
        {
            foreach (var coord in coords)
            {
                SetSquareType(coord, type);
            }
        }

        public void SetStart(Vector2Int coord)
        {
            SetSquareType(coord, Type.START);
            m_Start = coord;
        }

        public void SetEnd(Vector2Int coord)
        {
            SetSquareType(coord, Type.END);
            m_End = coord;
        }


        public MapSquareBehaviour GetBehaviour(Vector2Int coord)
        {
            MapSquareBehaviour behaviour;
            if (m_SquareDict.TryGetValue(coord, out behaviour))
            {
                return behaviour;
            }
            return default;
        }


        public void SetActive(bool active)
        {
            foreach (var square in m_SquareDict.Values.ToList())
            {
                square.SetActive(active);
            }
            m_Map.gameObject.SetActive(active);
        }

        public void Restart()
        {
            foreach(var square in m_SquareDict.Values.ToList())
            {
                square.type = Type.SPACE;
            }

            SetActive(false);
        }

        /**
         * 在Awake中调用
         * **/

        public void Load()
        {
            float squareScale = 1.0f / Mathf.Min(m_Row, m_Col);

            for (int i = 0; i < m_Row; ++i)
            {
                for (int j = 0; j < m_Col; ++j)
                {
                    // 创建地图
                    GameObject square = Instantiate(m_SquarePrefab, m_Map);
                    square.SetActive(true);
                    square.name = $"{i}_{j}";
                    // 设置局部坐标和缩放
                    square.transform.localScale = new Vector3(squareScale, 1f, squareScale);
                    square.transform.localPosition = new Vector3((2.0f * i + 1.0f) * squareScale - 1f, 0f, (2.0f * j + 1.0f) * squareScale - 1f);

                    // 传递方块属性
                    MapSquareBehaviour msb = square.GetComponent<MapSquareBehaviour>();
                    Vector2Int coord = new Vector2Int(i, j);
                    msb.type = Type.SPACE;
                    msb.SetCoordinate(coord);
                    m_SquareDict.Add(coord, msb);
                }
            }

            m_AStarInstance = new Utils.AStar(m_SquareDict.Keys.ToList(), PrunningFunc);
        }
    }
}

